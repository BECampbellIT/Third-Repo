using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandAlonePackingLib.SAPFuncs;
using System.ServiceModel;
using MySql.Data.MySqlClient;
using ShopFloorLib;
using System.Drawing;

namespace StandAlonePackingLib
{
    public class Order : ButtonMatrix.MatrixObject
    {
        #region Public Attributes
        public string materialNum { get; private set; }
        public Material material { get; private set; }
        public decimal targetQty { get; private set; }
        public decimal packedQty { get; private set; }
        public decimal maxQty { get; private set; }
        public string Uom { get; private set; }
        public List<IncOrder> incOrders;
        #endregion

        #region Private Attributes
        private static MySqlCommand insertCommand;
        private static MySqlCommand updateAllFldsCommand;
        private static MySqlCommand selectSingleCommand;
        private static MySqlCommand incDeliveredQtyCommand;
        #endregion

        #region Constructors
        public Order(IncOrder incOrder)
        {
            this.materialNum = incOrder.materialNum;
            try
            {
                this.material = CommonData.mats.Find(m => m.matNumber.Equals(materialNum));
            }
            catch (ArgumentNullException)
            {
                MessageLogger.Add(string.Format("Material master data missing for order {0} material {1}", incOrder.orderNum, incOrder.materialNum)
                    , MessageLogger.MsgLevel.warning);
            }
            this.Uom = incOrder.Uom;
            this.incOrders = new List<IncOrder>();
        }
        #endregion

        #region SortIncOrders
        public void SortIncOrders()
        {
            this.incOrders.Sort(delegate(IncOrder a, IncOrder b)
            {
                bool aOverDel = a.packedQty >= a.targetQty;
                bool bOverDel = b.packedQty >= b.targetQty;

                // Use orders that aren't fully delivered in preference to those that are.
                if (aOverDel && !bOverDel) return 1;
                if (!aOverDel && bOverDel) return -1;

                if (aOverDel && bOverDel)
                {
                    //Both orders fully delivered, use a default order in preference to a scheduled order
                    if (a.seqNum == 99999999999999M)
                        return -1;

                    if (b.seqNum == 99999999999999M)
                        return 1;
                }

                // Use orders with a lower sequence order over ones with higher seq nums.
                if (a.seqNum > b.seqNum) return 1;
                if (a.seqNum < b.seqNum) return -1;

                // Can't decide based on being fully delivered or different seq nums, use the one with a lower order number
                return a.orderNum.CompareTo(b.orderNum);
            });
        }
        #endregion

        #region AddIncOrder
        public void AddIncOrder(IncOrder incOrd)
        {
            if (incOrd.relInd)
            {
                this.packedQty += incOrd.packedQty;
                this.targetQty += incOrd.targetQty;
                if (!material.unlimOverdel)
                {
                    this.maxQty = decimal.Floor(this.targetQty * (1 + material.overdelPerc / 100));
                }
            }
            this.incOrders.Add(incOrd);
            SortIncOrders();
        }
        #endregion

        #region IncreaseDeliveredQty
        public void IncreaseDeliveredQty(decimal qtyPacked, IncOrder incOrd = null)
        {
            packedQty += qtyPacked;
            if (incOrd == null)
                this.incOrders[0].IncreaseDeliveredQty(qtyPacked);
            else
                incOrd.IncreaseDeliveredQty(qtyPacked);

            SortIncOrders();
        }
        #endregion

        #region Initialise
        public static bool Initialise()
        {
            if (!DBOperations.OpenDBConnection())
                return false;

            // Prepare an insert command to create a new row in the order tab
            insertCommand = DBOperations.myConn.CreateCommand();
            insertCommand.CommandText = "INSERT INTO pck.order VALUES (@ord_num,@strt_date,@mat_number,@tq,@pq,@uom,@tw,@ot,@plant,@sloc,@unlim,@overdel,@sdoc,@cst,@cst_name,@freez,@st,@seq,@rel,@bq,@buom,@strt_time,@sl_dates,@del_date)";

            updateAllFldsCommand = DBOperations.myConn.CreateCommand();
            updateAllFldsCommand.CommandText = @"UPDATE pck.order SET
start_date=@strt_date,
material_num=@mat_number,
target_qty=@tq,
packed_qty= @pq,
uom=@uom,
tare_weight=@tw,
order_type=@ot,
plant=@plant,
storage_lctn=@sloc,
unlim_overdel=@unlim,
overdel_perc=@overdel,
sales_doc=@sdoc,
customer=@cst,
cust_name=@cst_name,
use_freezer=@freez,
stk_type=@st,
seq_num=@seq,
rel_ind=@rel,
bom_base_qty=@bq,
bom_base_uom=@buom,
start_time=@strt_time,
slaughter_dates=@sl_dates,
deliv_date=@del_date
WHERE order_num = @ord_num";

            selectSingleCommand = DBOperations.myConn.CreateCommand();
            selectSingleCommand.CommandText = "SELECT order_num FROM pck.order WHERE order_num=@ord_num";
            selectSingleCommand.Parameters.AddWithValue("@ord_num", "");
            selectSingleCommand.Prepare();

            incDeliveredQtyCommand = DBOperations.myConn.CreateCommand();
            incDeliveredQtyCommand.CommandText = "UPDATE pck.order SET packed_qty = packed_qty + @inc WHERE order_num=@ord_num";
            incDeliveredQtyCommand.Parameters.AddWithValue("ord_num", typeof(string));
            incDeliveredQtyCommand.Parameters.AddWithValue("inc", typeof(decimal));
            incDeliveredQtyCommand.Prepare();

            return true;
        }
        #endregion

        #region ReadOrdersFromSAP
        public static bool ReadOrdersFromSAP(out List<Order> normalOrders, out List<Order> reworkOrders, out List<Order.PackedOn> slDates)
        {
            normalOrders = new List<Order>();
            reworkOrders = new List<Order>();
            slDates = new List<PackedOn>();

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.MaxReceivedMessageSize = 1000000;

            var address = new EndpointAddress(CommonData.webServiceEndPoint);
            var client = new ZStandalonePackingClient(binding, address);
            var toSAP = new ZGetProductionOrders();
            toSAP.Device = CommonData.sapSettings.device;

            client.ClientCredentials.UserName.UserName = CommonData.sapSettings.user;
            client.ClientCredentials.UserName.Password = CryptoSystem.Decrypt(CommonData.sapSettings.password);
            ZGetProductionOrdersResponse fromSAP;

            try
            {
                fromSAP = client.ZGetProductionOrders(toSAP);
            }
            catch (CommunicationException ex)
            {
                MessageLogger.Add("Error reading orders from SAP", MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            string prevMatNumber = "~";

            Order normalOrder = null;
            Order reworkOrder = null;
            bool missingMaterialData = false;

            foreach (ZsapaProdOrder sapOrd in fromSAP.Orders)
            {
                var newIncOrd = new IncOrder(sapOrd);
                bool rework = sapOrd.OrderType.Equals("YREW");

                if (!sapOrd.MaterialNum.Equals(prevMatNumber))
                {
                    normalOrder = reworkOrder = null;
                    missingMaterialData = false;

                    var ord = new Order(newIncOrd);
                    if (ord.material == null)
                    {
                        missingMaterialData = true;
                        MessageLogger.Add("No master data for material " + ord.materialNum, MessageLogger.MsgLevel.warning);
                    }
                    else if (rework)
                    {
                        reworkOrder = ord;
                        reworkOrders.Add(ord);
                    }
                    else
                    {
                        normalOrder = ord;
                        normalOrders.Add(ord);
                    }
                }
                if (missingMaterialData)
                    continue;

                if (rework)
                {
                    if (reworkOrder == null)
                    {
                        reworkOrder = new Order(newIncOrd);
                        reworkOrders.Add(reworkOrder);
                    }
                    reworkOrder.AddIncOrder(newIncOrd);
                }
                else
                {
                    if (normalOrder == null)
                    {
                        normalOrder = new Order(newIncOrd);
                        normalOrders.Add(normalOrder);
                    }
                    normalOrder.AddIncOrder(newIncOrd);
                }
                prevMatNumber = sapOrd.MaterialNum;
            }
            foreach (var sl in fromSAP.ProdDates)
            {
                slDates.Add(new Order.PackedOn(sl));
            }
            return true;
        }
        #endregion

        #region SaveOrdersToDB
        public static string SaveOrdersToDB(List<Order> ords)
        {
            DBOperations.BeginTransaction();
            foreach (Order ord in ords)
            {
                foreach (IncOrder incOrd in ord.incOrders)
                {
                    selectSingleCommand.Parameters[0].Value = incOrd.orderNum;

                    try
                    {
                        if (selectSingleCommand.ExecuteScalar() == null)
                        {
                            // Order doesn't exist in DB - insert record
                            incOrd.PopulateParameters(insertCommand);
                            insertCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            // Update existing info for this order
                            incOrd.PopulateParameters(updateAllFldsCommand);
                            updateAllFldsCommand.ExecuteNonQuery();
                        }

                    }
                    catch (MySqlException ex)
                    {
                        DBOperations.RollbackTransaction();
                        MessageLogger.Add(string.Format("Error saving order {0} to DB - see log file for details",incOrd.orderNum), MessageLogger.MsgLevel.error);
                        MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                        return null;
                    }
                }
                // Remove orders that aren't released
                ord.incOrders.RemoveAll(inc => !inc.relInd);
            }
            // Remove materials without any released orders.
            ords.RemoveAll(o => o.incOrders.Count == 0);

            DBOperations.CommitTransaction();
            return null;
        }
        #endregion

        #region SavePackDatesToDB
        public static void SavePackDatesToDB(List<Order.PackedOn> slDates)
        {
            DBOperations.BeginTransaction();

            try
            {
                DBOperations.OpenDBConnection();
                MySqlCommand cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "delete from pck.pack_date";
                cmd.ExecuteNonQuery();


                MySqlCommand ins = DBOperations.myConn.CreateCommand();
                ins.CommandText = "insert pck.pack_date values (@packed, @sldates, @def)";
                ins.Parameters.Add("packed", MySqlDbType.DateTime);
                ins.Parameters.Add("sldates", MySqlDbType.String);
                ins.Parameters.AddWithValue("def", true);

                foreach (var s in slDates)
                {
                    ins.Parameters["packed"].Value = s.packedOn;
                    ins.Parameters["sldates"].Value = s.slaughterDates;
                    ins.Parameters["def"].Value = s.defaultVal;
                    ins.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add("Error saving packed on dates to DB - see log file for more details", MessageLogger.MsgLevel.critical);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }
            DBOperations.CommitTransaction(); ;
        }
        #endregion

        #region ReadOrdersFromDB
        public static void ReadOrdersFromDB(out List<Order> normalOrders, out List<Order> reworkOrders, out List<Order.PackedOn> slDates)
        {
            normalOrders = new List<Order>();
            reworkOrders = new List<Order>();

            string prevMatNumber = "~";

            Order normalOrder = null;
            Order reworkOrder = null;
            bool missingMaterialData = false;

            DBOperations.BeginTransaction();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "select * from pck.order where start_date = current_date() and rel_ind = 1 order by material_num";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var newIncOrd = new IncOrder(rdr);
                    bool rework = newIncOrd.orderType.Equals("YREW");

                    if (!newIncOrd.materialNum.Equals(prevMatNumber))
                    {
                        normalOrder = reworkOrder = null;
                        missingMaterialData = false;

                        var ord = new Order(newIncOrd);
                        if (ord.material == null)
                        {
                            missingMaterialData = true;
                            MessageLogger.Add("No master data for material " + ord.materialNum, MessageLogger.MsgLevel.warning);
                        }
                        else if (rework)
                        {
                            reworkOrder = ord;
                            reworkOrders.Add(ord);
                        }
                        else
                        {
                            normalOrder = ord;
                            normalOrders.Add(ord);
                        }
                    }
                    if (missingMaterialData)
                        continue;

                    if (rework)
                    {
                        if (reworkOrder == null)
                        {
                            reworkOrder = new Order(newIncOrd);
                            reworkOrders.Add(reworkOrder);
                        }
                        reworkOrder.AddIncOrder(newIncOrd);
                    }
                    else
                    {
                        if (normalOrder == null)
                        {
                            normalOrder = new Order(newIncOrd);
                            normalOrders.Add(normalOrder);
                        }
                        normalOrder.AddIncOrder(newIncOrd);
                    }
                    prevMatNumber = newIncOrd.materialNum;
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading materials from DB - see log file for more details", MessageLogger.MsgLevel.critical);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }

            slDates = ReadPackedOnDatesFromDB();
            DBOperations.CommitTransaction();
        }
        #endregion

        #region ReadPackedOnDatesFromDB
        private static List<Order.PackedOn> ReadPackedOnDatesFromDB()
        {
            var slDates = new List<Order.PackedOn>();

            DBOperations.OpenDBConnection();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "select * from pck.pack_date";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    slDates.Add(new PackedOn(rdr));
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading Packed On dates from DB - see log file for more details", MessageLogger.MsgLevel.critical);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }

            return slDates;
        }
        #endregion

        #region  methods from MatrixObject interface
        /*
         * Implement methods from MatrixObject interface 
         */
        override public string ToString()
        {
            return string.Format("{0}\n{1}\n{2} / {3} {4}",
                                   this.materialNum,
                                   this.material.description,
                                   this.packedQty.ToString("#0"),
                                   this.targetQty.ToString("#"),
                                   this.Uom);
        }
        public bool MatchesFilter()
        {
            return this.materialNum.Contains(CommonData.filter);
        }
        public Color GetNormalColor()
        {
            if (packedQty >= targetQty)
                // Already fully delivered
                if (material.woolGrp.Length > 0)
                    return Color.Orange;
                else
                    return Color.MistyRose;
            else
                // Not fully delivered
                if (material.woolGrp.Length > 0)
                    return Color.LimeGreen;
                else
                    return Color.MintCream;
        }
        public Color GetSelectedColor()
        {
            if (packedQty >= targetQty)
                // Already fully delivered
                return Color.Crimson;
            else
                // Not fully delivered
                if (material.woolGrp.Length > 0)
                    return Color.Lime;
                else
                    return Color.Cyan;
        }
        public string GetKey()
        {
            return materialNum;
        }
        #endregion

        /************************************
         * IncOrder nested class
         ************************************/
        public class IncOrder : ButtonMatrix.MatrixObject
        {
            #region Public Attributes
            public string orderNum { get; private set; }
            public string materialNum { get; private set; }
            public decimal targetQty { get; private set; }
            public decimal packedQty { get; private set; }
            public string Uom { get; private set; }
            public decimal tareWeight { get; private set; }
            public string orderType { get; private set; }
            public string plant { get; private set; }
            public string storageLctn { get; private set; }
            public bool unlimOverdel { get; private set; }
            public decimal overdelPerc { get; private set; }
            public string salesDoc { get; private set; }
            public string customer { get; private set; }
            public string custName { get; private set; }
            public DateTime startDateTime { get; private set; }
            public bool useFreezer { get; private set; }
            public string stkType { get; private set; }
            public decimal seqNum { get; private set; }
            public bool relInd { get; private set; }
            public decimal bomBaseQty { get; private set; }
            public string bomBaseUom { get; private set; }
            public string slaughterDates { get; private set; }
            public DateTime delivDate { get; private set; }
            #endregion

            #region Constructors
            public IncOrder(ZsapaProdOrder sapOrd)
            {
                this.orderNum = sapOrd.OrderNum;
                this.materialNum = sapOrd.MaterialNum;
                this.targetQty = sapOrd.TargetQty;
                this.packedQty = sapOrd.PackedQty;
                this.Uom = sapOrd.Uom;
                this.tareWeight = sapOrd.TareWeight;
                this.orderType = sapOrd.OrderType;
                this.plant = sapOrd.Plant;
                this.storageLctn = sapOrd.StorageLctn;
                this.unlimOverdel = sapOrd.UnlimOverdel.Equals("X");
                this.relInd = sapOrd.RelInd.Equals("X");
                this.overdelPerc = sapOrd.OverdelPerc;
                this.salesDoc = sapOrd.SalesDoc;
                this.customer = sapOrd.Customer;
                this.custName = sapOrd.CustName;
                this.startDateTime = new DateTime(Int32.Parse(sapOrd.StartDate.Substring(0, 4)),  //Year
                                                    Int32.Parse(sapOrd.StartDate.Substring(5, 2)),  //Month 
                                                    Int32.Parse(sapOrd.StartDate.Substring(8, 2)),  //Day
                                                    sapOrd.StartTime.Hour,
                                                    sapOrd.StartTime.Minute,
                                                    sapOrd.StartTime.Second);
                this.useFreezer = sapOrd.UseFreezer.Equals("X");
                this.stkType = sapOrd.StkType;
                this.seqNum = decimal.Parse(sapOrd.SeqNum);
                this.bomBaseQty = sapOrd.BomBaseQty;
                this.bomBaseUom = sapOrd.BomBaseUom;
                this.slaughterDates = sapOrd.SlaughterDates;

                if (Int32.Parse(sapOrd.DelivDate.Substring(0, 4)) != 0)
                {
                    this.delivDate = new DateTime(Int32.Parse(sapOrd.DelivDate.Substring(0, 4)),  //Year
                                                    Int32.Parse(sapOrd.DelivDate.Substring(5, 2)),  //Month 
                                                    Int32.Parse(sapOrd.DelivDate.Substring(8, 2)));  //Day
                }
            }
            public IncOrder(MySqlDataReader rdr)
            {
                orderNum = rdr.GetString("order_num");
                materialNum = rdr.GetString("material_num");
                targetQty = rdr.GetDecimal("target_qty");
                packedQty = rdr.GetDecimal("packed_qty");
                Uom = rdr.GetString("uom");
                tareWeight = rdr.GetDecimal("tare_weight");
                orderType = rdr.GetString("order_type");
                plant = rdr.GetString("plant");
                storageLctn = rdr.GetString("storage_lctn");
                unlimOverdel = rdr.GetBoolean("unlim_overdel");
                overdelPerc = rdr.GetDecimal("overdel_perc");
                salesDoc = rdr.GetString("sales_doc");
                customer = rdr.GetString("customer");
                custName = rdr.GetString("cust_name");
                startDateTime = rdr.GetDateTime("start_date");
                useFreezer = rdr.GetBoolean("use_freezer");
                stkType = rdr.GetString("stk_type");
                seqNum = rdr.GetDecimal("seq_num");
                relInd = rdr.GetBoolean("rel_ind");
                bomBaseQty = rdr.GetDecimal("bom_base_qty");
                bomBaseUom = rdr.GetString("bom_base_uom");
                slaughterDates = rdr.GetString("slaughter_dates");
                delivDate = rdr.GetDateTime("deliv_date");
            }
            public IncOrder()
            {

            }
            #endregion

            #region IncreaseDeliveredQty
            public void IncreaseDeliveredQty(decimal qtyPacked)
            {
                packedQty += qtyPacked;

                incDeliveredQtyCommand.Parameters[0].Value = this.orderNum;
                incDeliveredQtyCommand.Parameters[1].Value = qtyPacked;
                incDeliveredQtyCommand.ExecuteNonQuery();
            }
            #endregion

            #region PopulateParameters
            public void PopulateParameters(MySqlCommand c)
            {
                if (c.Parameters.Count == 0)
                {
                    c.Parameters.AddWithValue("@ord_num", orderNum);
                    c.Parameters.AddWithValue("@strt_date", startDateTime);
                    c.Parameters.AddWithValue("@mat_number", materialNum);
                    c.Parameters.AddWithValue("@tq", targetQty);
                    c.Parameters.AddWithValue("@pq", packedQty);
                    c.Parameters.AddWithValue("@uom", Uom);
                    c.Parameters.AddWithValue("@tw", tareWeight);
                    c.Parameters.AddWithValue("@ot", orderType);
                    c.Parameters.AddWithValue("@plant", plant);
                    c.Parameters.AddWithValue("@sloc", storageLctn);
                    c.Parameters.AddWithValue("@unlim", unlimOverdel);
                    c.Parameters.AddWithValue("@overdel", overdelPerc);
                    c.Parameters.AddWithValue("@sdoc", salesDoc);
                    c.Parameters.AddWithValue("@cst", customer);
                    c.Parameters.AddWithValue("@cst_name", custName);
                    c.Parameters.AddWithValue("@freez", useFreezer);
                    c.Parameters.AddWithValue("@st", stkType);
                    c.Parameters.AddWithValue("@seq", seqNum);
                    c.Parameters.AddWithValue("@rel", relInd);
                    c.Parameters.AddWithValue("@bq", bomBaseQty);
                    c.Parameters.AddWithValue("@buom", bomBaseUom);
                    c.Parameters.AddWithValue("@strt_time", startDateTime);
                    c.Parameters.AddWithValue("@sl_dates", slaughterDates);
                    c.Parameters.AddWithValue("@del_date", delivDate);
                    c.Prepare();
                }
                else
                {
                    c.Parameters[0].Value = orderNum;
                    c.Parameters[1].Value = startDateTime;
                    c.Parameters[2].Value = materialNum;
                    c.Parameters[3].Value = targetQty;
                    c.Parameters[4].Value = packedQty;
                    c.Parameters[5].Value = Uom;
                    c.Parameters[6].Value = tareWeight;
                    c.Parameters[7].Value = orderType;
                    c.Parameters[8].Value = plant;
                    c.Parameters[9].Value = storageLctn;
                    c.Parameters[10].Value = unlimOverdel;
                    c.Parameters[11].Value = overdelPerc;
                    c.Parameters[12].Value = salesDoc;
                    c.Parameters[13].Value = customer;
                    c.Parameters[14].Value = custName;
                    c.Parameters[15].Value = useFreezer;
                    c.Parameters[16].Value = stkType;
                    c.Parameters[17].Value = seqNum;
                    c.Parameters[18].Value = relInd;
                    c.Parameters[19].Value = bomBaseQty;
                    c.Parameters[20].Value = bomBaseUom;
                    c.Parameters[21].Value = startDateTime;
                    c.Parameters[22].Value = slaughterDates;
                    c.Parameters[23].Value = delivDate;
                }
            }
            #endregion

            #region methods from MatrixObject interface 
            /*
             * Implement methods from MatrixObject interface 
             */
            override public string ToString()
            {
                string custName;

                if (this.customer == null || this.customer.Length == 0)
                    custName = "Arndell Park";
                else
                    custName = string.Format("{0} - {1}", this.customer, this.custName);

                return string.Format("{0}\n{1} / {2} {3}\nOrder: {4}\nDeliv Date: {5}",
                                    custName,
                                    this.packedQty.ToString("#0"),
                                    this.targetQty.ToString("#"),
                                    this.Uom,
                                    this.salesDoc,
                                    this.delivDate.ToString("dd.MM.yyyy"));
            }
            public bool MatchesFilter()
            {
                return true;
            }
            public Color GetNormalColor()
            {
                if (packedQty >= targetQty)
                    return Color.MistyRose;
                else
                    return Color.MintCream;
            }
            public Color GetSelectedColor()
            {
                if (packedQty >= targetQty)
                    return Color.Crimson;
                else
                    return Color.Cyan;
            }
            public string GetKey()
            {
                return orderNum;
            }
            #endregion
        }

        /*************************************
         * PackedOn nested class
         *************************************/
        public class PackedOn : ButtonMatrix.MatrixObject
        {
            #region Public Attributes
            public DateTime packedOn { get; private set; }
            public string slaughterDates { get; private set; }
            public bool defaultVal { get; private set; }
            #endregion

            #region Contstructors
            public PackedOn(MySqlDataReader rdr)
            {
                packedOn = rdr.GetDateTime("packed_on");
                slaughterDates = rdr.GetString("slaughter_dates");
                defaultVal = rdr.GetBoolean("default_value");
            }
            public PackedOn(ZsapaProdDate s)
            {
                packedOn = new DateTime(Int32.Parse(s.ManuDate.Substring(0, 4)),  //Year
                                        Int32.Parse(s.ManuDate.Substring(5, 2)),  //Month 
                                        Int32.Parse(s.ManuDate.Substring(8, 2)));  //Day
                slaughterDates = s.SlaughterDates;
                defaultVal = s.DefaultVal.Equals("X");
            }
            #endregion

            #region methods from MatrixObject interface 
            /************************************
             * Implement methods from MatrixObject interface 
             ************************************/
            override public string ToString()
            {
                return string.Format("Pack Date: {0}\r\nSlaughter Dates: {1}", packedOn.ToShortDateString(), slaughterDates);
            }
            public bool MatchesFilter()
            {
                return true;
            }
            public Color GetNormalColor()
            {
                return Color.MintCream;
            }
            public Color GetSelectedColor()
            {
                return Color.Cyan;
            }
            public string GetKey()
            {
                return packedOn.ToString();
            }
            #endregion
        }
    }
}
