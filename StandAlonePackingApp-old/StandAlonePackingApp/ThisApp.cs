using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Drawing;
using ShopFloorLib;
using StandAlonePackingLib;
using System.ComponentModel;
using System.Threading;

namespace StandAlonePackingApp
{
    public static class ThisApp
    {
        public enum Mode { NormalPacking, Rework, PartCarton, Deletion };

        public static bool weightToleranceDisabled = false;
        public static User user = null;
        public static List<User> userList = null;

        public static ScaleIndicator scale;
        public static LabelPrinter printer;

        private static Order ordLastPrint = null;
        private static Order.IncOrder incOrdLastPrint = null;
        private static Pack packLastPrint = null;

        #region Initialise
        public static void Initialise(BusyDialog busyDialog)
        {
            if (!Order.Initialise())
                MessageLogger.Add("Error connecting to DB - see log file for details", MessageLogger.MsgLevel.critical);

            Pack.Initialise();
            printer = new LabelPrinter();

            if (CommonData.localSettings.PullMaterialsAtStartup)
                ReadMaterialsFromSAP(busyDialog);
            else
                ReadMaterialsFromDB(busyDialog);

            if (CommonData.localSettings.PullOrdersAtStartup)
                ReadOrdersFromSAP(busyDialog);
            else
                ReadOrdersFromDB(busyDialog);       
        }
        #endregion

        #region ReadMaterials
        public static void ReadMaterialsFromSAP(BusyDialog bd)
        {
            bd.SetTitle("Reading Material Master Data from SAP");

            if (Material.readMaterialsFromSAP(out CommonData.mats))
            {
                MessageLogger.Add(CommonData.mats.Count + " materials read from SAP", MessageLogger.MsgLevel.info);

                bd.SetTitle("Saving Material Master Data to Local DB");
                Material.saveMaterialsToDB(CommonData.mats, true);
            }
            else
            {
                // Error pulling updated material list from SAP - use local material master data instead.
                ReadMaterialsFromDB(bd);
            }
        }

        public static void ReadMaterialsFromDB(BusyDialog bd)
        {
            bd.SetTitle("Reading Material Master Data from DB");
            CommonData.mats = Material.readMaterialsFromDB();
            MessageLogger.Add(CommonData.mats.Count + " materials read from DB", MessageLogger.MsgLevel.info);
        }
        #endregion

        #region ReadOrders
        public static void ReadOrdersFromSAP(BusyDialog bd)
        {
            bd.SetTitle("Sending unsent cartons to SAP");
            if (Pack.SendPacks())
            {
                bd.SetTitle("Reading Production Orders from SAP");
                if (Order.ReadOrdersFromSAP(out CommonData.normalOrders, out CommonData.reworkOrders, out CommonData.slDates))
                {
                    MessageLogger.Add(CommonData.normalOrders.Count + " normal orders read from SAP", MessageLogger.MsgLevel.info);
                    MessageLogger.Add(CommonData.reworkOrders.Count + " re-work orders read from SAP", MessageLogger.MsgLevel.info);

                    bd.SetTitle("Saving Production Orders to Local DB");
                    Order.SaveOrdersToDB(CommonData.normalOrders);
                    Order.SaveOrdersToDB(CommonData.reworkOrders);
                    Order.SavePackDatesToDB(CommonData.slDates);
                    return;
                }
            }
            // Didn't successfully read orders from SAP - read local copy of order data instead
            ReadOrdersFromDB(bd);
        }
        public static void ReadOrdersFromDB(BusyDialog bd)
        {
            bd.SetTitle("Reading Production Orders from DB");
            Order.ReadOrdersFromDB(out CommonData.normalOrders, out CommonData.reworkOrders, out CommonData.slDates);
            MessageLogger.Add(CommonData.normalOrders.Count + " normal orders read from DB", MessageLogger.MsgLevel.info);
            MessageLogger.Add(CommonData.reworkOrders.Count + " re-work orders read from DB", MessageLogger.MsgLevel.info);
        }
        #endregion

        #region PostReceipt
        public static bool PostReceipt(Order ord, Mode mode, Order.PackedOn reworkDateSel = null, Order.IncOrder incOrderSel = null)
        {
            // Get weight from scale
            if (scale.ReadScale(out decimal gross, out decimal net, out decimal tare) != ScaleReader.Stability.stableWeight)
            {
                MessageLogger.Add("Stable weight not returned from scale", MessageLogger.MsgLevel.error);
                return false;
            }

            if (tare <= 0)
            {
                MessageLogger.Add("No tare weight returned from scale", MessageLogger.MsgLevel.error);
                return false;
            }

            Order.IncOrder incOrd;
            if (incOrderSel == null)
                // No specific order selected by user - use the order at the top of the list, which is always sorted to
                // put the highest priority, not-yet-completely-delivered order at the top.
                incOrd = ord.incOrders[0];
            else
                // User has selected a specific order i.e. we're in bin receipting mode
                incOrd = incOrderSel;

            if (net < 0)
            {
                MessageLogger.Add("Negative net weight returned from scale.", MessageLogger.MsgLevel.error);
                return false;
            }

            if (!ThisApp.weightToleranceDisabled)
            {
                // Check maximum and minimum carton weights
                if (ord.material.minWeight != 0M && net < ord.material.minWeight)
                {
                    MessageLogger.Add(string.Format("Weight ({1:0.00} kg) is below the minimum weight of {0:0.00}", ord.material.minWeight, net),
                                        MessageLogger.MsgLevel.error);
                    return false;
                }

                if (ord.material.maxWeight != 0M && net > ord.material.maxWeight)
                {
                    MessageLogger.Add(string.Format("Weight ({1:0.00} kg) is above the maximum weight of {0:0.00}", ord.material.maxWeight, net),
                                            MessageLogger.MsgLevel.error);
                    return false;
                }
            }

            decimal qtyToPack;
            if (ord.material.baseUom.Equals("KG"))
                qtyToPack = net;  // Receipting a bin
            else  
                qtyToPack = 1;    // Packing a single carton

            //Check over delivery tolerance
            if (ord.maxQty != 0M && (ord.packedQty + qtyToPack) > ord.maxQty)
            {
                MessageLogger.Add(string.Format("Maximum qty of {0} {1} already packed for this product", ord.maxQty, ord.Uom), MessageLogger.MsgLevel.error);
                return false;
            }

            if (mode == Mode.Rework && reworkDateSel == null)
            {
                MessageLogger.Add("Please select a production date for re-work,", MessageLogger.MsgLevel.error);
                return false;
            }
            decimal actWeight = net;

            DateTime manuDate;
            string slaughterDates;
            if (mode == Mode.Rework)
            {
                manuDate = reworkDateSel.packedOn;
                slaughterDates = reworkDateSel.slaughterDates;
            }
            else
            {
                manuDate = DateTime.Now;
                slaughterDates = incOrd.slaughterDates;
            }

            if (ord.material.fixedWeight)
                net = ord.material.nomWeight; //Fixed weight carton, always pack at nominal weight

            Pack pack = new Pack(incOrd : incOrd, 
                                mat : ord.material,
                                _qty : qtyToPack,
                                _uom : ord.material.baseUom,
                                _netWeight : net,
                                _tareWeight : tare,
                                _actualWeight : actWeight,
                                _manuDate : manuDate,
                                _slaughterDates : slaughterDates,
                                _terminal: CommonData.sapSettings.device,
                                _batch : manuDate.ToString("yyyyMMdd"),
                                _user : ThisApp.user?.userId);

            DBOperations.BeginTransaction();
            pack.InsertSingle();
            ord.IncreaseDeliveredQty(qtyToPack, incOrd);
            DBOperations.CommitTransaction();

            var barcode = PrintLabel(ord, incOrd, pack);
            if (CommonData.localSettings.OtherPackingStationPort != 0)
            {
                // Start new thread to send carton receipt info to the other packing station
                var ri = new InterStationComms.ReceiptInfo();
                ri.materialNum = incOrd.materialNum;
                ri.orderNum = incOrd.orderNum;
                ri.qtyPacked = qtyToPack;

                ThreadPool.QueueUserWorkItem(new WaitCallback(InterStationComms.SendToOtherStation), ri);
            }

            MessageLogger.Add(string.Format("Packed material {0} barcode {1} weight {2}", pack.materialNum, pack.serial, net), MessageLogger.MsgLevel.info);

            return true;
        }
        #endregion

        #region PrintLabel
        public static string PrintLabel(Order ord, Order.IncOrder incOrd, Pack pack)
        {
            string comment;
            string barcode;
            Material labelMat;
            decimal gross = pack.tareWeight + pack.netWeight;

            if (ord.material.madeToOrd && incOrd.customer == null && ord.material.targetMatNumber != null)
            {
                //This is a bin going to AP that has an automatic material-to-material done as part of the GR process. It should be 
                //labelled up as if it were the target material of the mat-2-mat.
                comment = "Converted from: " + ord.materialNum;
                // Use target material's details on label
                labelMat = CommonData.mats.Find(m => m.matNumber.Equals(ord.material.targetMatNumber));
            }
            else
            {
                comment = "";
                labelMat = ord.material;
            }

            barcode = string.Format("000000000{0:0000000000}91{1}", pack.serial, pack.useFreezer ? "1" : "0");
            
            var dict = new Dictionary<string, string>();

            dict.Add("&MATNR&", labelMat.matNumber);
            dict.Add("&MAKTX_1&", labelMat.labelLine1);
            dict.Add("&MAKTX_2&", labelMat.labelLine2);
            dict.Add("&MAKTX_3&", labelMat.labelLine3);
            dict.Add("&MAKTX_4&", labelMat.labelLine4);
            dict.Add("&DEVCE&", labelMat.Ean);
            dict.Add("&NTGEW_2DPLS&", pack.netWeight.ToString("0.00"));
            dict.Add("&BRGEW_2DPLS&", gross.ToString("0.00"));
            dict.Add("&TAGEW&", pack.tareWeight.ToString());
            dict.Add("&EXIDV&", pack.serial.ToString("0000000000"));
            dict.Add("&BARCODE&", barcode);
            dict.Add("&ERNAM&", "BCP:" + pack.user);
            dict.Add("&HSDAT&", pack.manuDate.ToString("dd.MM.yyyy"));
            dict.Add("&HSDAT_S&", pack.manuDate.ToString("yyMMdd"));
            dict.Add("&HSDAT_DDMMMYY&", pack.manuDate.ToString("dd MMM yy"));
            dict.Add("&ERZET&", pack.packedOn.ToString("HH:mm:ss"));
            dict.Add("&VFDAT&", pack.expiryDate.ToString("dd.MM.yyyy"));
            dict.Add("&VFDAT_DDMMMYY&", pack.expiryDate.ToString("dd MMM yy"));
            dict.Add("&MBLNR&", labelMat.oldMatNumber);
            dict.Add("&WRK_NAME&", labelMat.custMatNumber);
            dict.Add("&WRK_STR&", comment);
            dict.Add("&TERMI&", CommonData.sapSettings.device);
            dict.Add("&SLDATES&", pack.slaughterDates);

            decimal netNoDec = decimal.Round(pack.netWeight * 100, 0);
            dict.Add("&NTGEW_OPUNKT&", netNoDec.ToString("000000"));
            dict.Add("&AAPOS&", netNoDec.ToString("0000"));

            printer.Print(labelMat.labelFile, dict);

            // Save away info of last label printed, needed for Re-print Last
            ordLastPrint = ord;
            incOrdLastPrint = incOrd;
            packLastPrint = pack;

            return barcode;
        }
        #endregion

        #region ReprintLast
        public static void ReprintLast()
        {
            if (ordLastPrint != null)
            {
                PrintLabel(ordLastPrint, incOrdLastPrint, packLastPrint);
                MessageLogger.Add(string.Format("Re-printed label for material {0} serial {1}", ordLastPrint.materialNum, packLastPrint.serial), 
                                        MessageLogger.MsgLevel.info);
            }
            else
            {
                MessageLogger.Add("No last carton to re-print", MessageLogger.MsgLevel.error);
            }
        }
        #endregion

        #region CancelLast
        public static void CancelLast()
        {
            if (packLastPrint == null)
            {
                MessageLogger.Add("No last carton to cancel", MessageLogger.MsgLevel.error);
                return;   //Nothing receipted yet, or has already been cancelled
            }

            if (packLastPrint.SetCancelled())
            {
                MessageLogger.Add(string.Format("Barcode {0} Material {1} Weight {2} cancelled", packLastPrint.barcode, packLastPrint.materialNum, packLastPrint.netWeight), MessageLogger.MsgLevel.info);
                ordLastPrint.IncreaseDeliveredQty(-packLastPrint.qty, incOrdLastPrint);

                if (CommonData.localSettings.OtherPackingStationPort != 0)
                {
                    // Start new thread to send carton receipt info to the other packing station
                    var ri = new InterStationComms.ReceiptInfo();
                    ri.materialNum = incOrdLastPrint.materialNum;
                    ri.orderNum = incOrdLastPrint.orderNum;
                    ri.qtyPacked = -packLastPrint.qty;

                    ThreadPool.QueueUserWorkItem(new WaitCallback(InterStationComms.SendToOtherStation), ri);
                }

                ordLastPrint = null;
                incOrdLastPrint = null;
                packLastPrint = null;
            }
        }
        #endregion

        #region PrintTestLabel
        public static bool PrintTestLabel()
        {
            ThisApp.scale.SetTare(0);

            // Get weight from scale
            if (scale.ReadScale(out decimal gross, out decimal net, out decimal tare) != ScaleReader.Stability.stableWeight)
            {
                MessageLogger.Add("Stable weight not returned from scale", MessageLogger.MsgLevel.error);
                return false;
            }

            var dict = new Dictionary<string, string>();
            var def = CommonData.slDates.Find(sd => sd.defaultVal);
            if (def == null)
            {
                MessageLogger.Add("Default slaughter dates not maintained", MessageLogger.MsgLevel.error);
                return false;
            }

            dict.Add("&MATNR&", "~TEST LABEL~");
            dict.Add("&MAKTX_EN&", "Test label only");
            dict.Add("&NTGEW&", gross.ToString("0.00"));
            dict.Add("&TAGEW&", "0.00");
            dict.Add("&ERDAT&", DateTime.Now.ToString("dd.MM.yyyy"));
            dict.Add("&ERZET&", DateTime.Now.ToString("HH:mm:ss"));
            dict.Add("&TERMI&", CommonData.sapSettings.device);
            dict.Add("&SLDATES&", def.slaughterDates);

            return printer.Print("TEST_LABEL_WP", dict);
        }
        #endregion

        #region CancelCartonReceipt
        public static bool CancelCartonReceipt(string bc)
        {
            if (bc.Length < 8)
            {
                MessageLogger.Add(string.Format("Funny looking barcode {0} - cannot process", bc), MessageLogger.MsgLevel.error);
                return false;
            }
            ulong serial;
            if (!ulong.TryParse(bc.Substring(0, bc.Length - 3), out serial))
            {
                MessageLogger.Add(string.Format("Non-numeric barcode {0} - cannot process", bc), MessageLogger.MsgLevel.error);
                return false;
            }
            Pack p = Pack.ReadSingle(serial);
            if (p==null)
            {
                MessageLogger.Add(string.Format("Unrecognised barcode {0}", bc), MessageLogger.MsgLevel.error);
                return false;
            }
            if (p.cancelled)
            {
                MessageLogger.Add(string.Format("Barcode {0} - already cancelled", bc), MessageLogger.MsgLevel.error);
                return false;
            }
            if (p.SetCancelled())
            {
                MessageLogger.Add(string.Format("Barcode {0} Material {1} Weight {2} cancelled", bc, p.materialNum, p.netWeight), MessageLogger.MsgLevel.info);
            }
            var ord = CommonData.normalOrders.Find(o => o.materialNum.Equals(p.materialNum));

            if (ord==null)
                ord = CommonData.reworkOrders.Find(o => o.materialNum.Equals(p.materialNum));

            if(ord!=null)
            {
                var incOrd = ord.incOrders.Find(i=>i.orderNum.Equals(p.orderNum));
                if (incOrd!=null)
                {
                    ord.IncreaseDeliveredQty(-p.qty, incOrd);

                    if (CommonData.localSettings.OtherPackingStationPort != 0)
                    {
                        // Start new thread to send carton receipt info to the other packing station
                        var ri = new InterStationComms.ReceiptInfo();
                        ri.materialNum = incOrd.materialNum;
                        ri.orderNum = incOrd.orderNum;
                        ri.qtyPacked = -p.qty;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(InterStationComms.SendToOtherStation), ri);
                    }

                    if (packLastPrint?.serial == p.serial)
                    {
                        // Have just cancelled the last pack receipted, clear out "Last Pack" fields
                        ordLastPrint = null;
                        incOrdLastPrint = null;
                        packLastPrint = null;
                    }

                    return true;
                }
            }
            return false;
        }
        #endregion  
    }
}
