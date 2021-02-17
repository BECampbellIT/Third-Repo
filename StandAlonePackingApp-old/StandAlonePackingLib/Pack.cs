using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopFloorLib;
using System.ServiceModel;
using StandAlonePackingLib.SAPFuncs;

namespace StandAlonePackingLib
{
    public class Pack
    {
        #region Public Attributes
        public ulong serial { get; private set; }
        public string barcode { get; private set; }
        public string orderNum { get; private set; }
        public string materialNum { get; private set; }
        public string plant { get; private set; }
        public string storage_lctn { get; private set; }
        public decimal qty { get; private set; }
        public string uom { get; private set; }
        public decimal netWeight { get; private set; }
        public decimal tareWeight { get; private set; }
        public decimal actualWeight { get; private set; }
        public decimal plannedWeight { get; private set; }
        public DateTime packedOn { get; private set; }
        public DateTime manuDate { get; private set; }
        public DateTime expiryDate { get; private set; }
        public string batch { get; private set; }
        public bool partCarton { get; private set; }
        public ulong origSerial { get; private set; }
        public string terminal { get; private set; }
        public string user { get; private set; }
        public bool useFreezer { get; private set; }
        public string slaughterDates { get; private set; }
        public bool cancelled { get; private set; }
        public DateTime sentToSAP { get; private set; }
        #endregion

        #region Private Attributes
        private static MySqlCommand insertCommand;
        private static MySqlCommand getSerialCommand;
        #endregion

        #region Constructors
        public Pack(ulong _serial)
        {
            serial = _serial;
        }
        public Pack(Order.IncOrder incOrd, Material mat,
            decimal _qty, string _uom,
            decimal _netWeight, decimal _tareWeight,
            decimal _actualWeight, 
            string _batch,
            string _terminal, string _user,
            DateTime _manuDate,
            string _slaughterDates,
            bool _partCarton = false,
            ulong _origSerial = 0)
        {
            orderNum = incOrd.orderNum;
            materialNum = mat.matNumber;
            qty = _qty;
            uom = _uom;
            plant = incOrd.plant;
            storage_lctn = incOrd.storageLctn;
            netWeight = _netWeight;
            tareWeight = _tareWeight;
            actualWeight = _actualWeight;
            plannedWeight = mat.nomWeight;
            packedOn = DateTime.Now;
            manuDate = _manuDate;
            expiryDate = manuDate.AddDays((double)mat.shelfLife);
            batch = _batch;
            partCarton = _partCarton;
            origSerial = _origSerial;
            terminal = _terminal;
            user = _user;
            useFreezer = incOrd.useFreezer;
            slaughterDates = _slaughterDates;
        }
        public Pack(MySqlDataReader rdr)
        {
            serial          = rdr.GetUInt64(0);
            orderNum        = rdr.GetString(1);
            materialNum     = rdr.GetString(2);
            plant           = rdr.GetString(3);
            storage_lctn    = rdr.GetString(4);
            qty             = rdr.GetDecimal(5);
            uom             = rdr.GetString(6);
            netWeight       = rdr.GetDecimal(7);
            tareWeight      = rdr.GetDecimal(8);
            actualWeight    = rdr.GetDecimal(9);
            plannedWeight   = rdr.GetDecimal(10);
            packedOn        = rdr.GetDateTime(11);
            manuDate        = rdr.GetDateTime(12);
            expiryDate      = rdr.GetDateTime(13);
            batch           = rdr.GetString(14);
            partCarton      = rdr.GetBoolean(15);
            origSerial      = rdr.GetUInt64(16);
            terminal        = rdr.GetString(17);
            user            = rdr.GetString(18);
            useFreezer      = rdr.GetBoolean(19);
            slaughterDates  = rdr.GetString(20);
            cancelled       = rdr.GetBoolean(21);
        }
        #endregion

        #region Initialise
        public static void Initialise()
        {
            DBOperations.OpenDBConnection();

            // Prepare an insert command to create a new row in the pack table
            insertCommand = DBOperations.myConn.CreateCommand();
            insertCommand.CommandText = @"INSERT INTO pck.pack 
(order_num,material_num,plant,storage_lctn,qty,uom,net_weight,tare_weight,actual_weight,planned_weight,packed_on,manu_date,expiry_date,batch,part_carton,orig_serial,terminal,user,use_freezer,slaughter_dates,cancelled)
VALUES (@ord,@mat,@plant,@sloc,@qty,@uom,@net,@tare,@act,@plan,@packedOn,@manu,@exp,@batch,@part,@orig,@term,@user,@freeze,@sl_dates,@canc)";

            insertCommand.Parameters.AddWithValue("@ord","");
            insertCommand.Parameters.AddWithValue("@mat", "");
            insertCommand.Parameters.AddWithValue("@plant", "");
            insertCommand.Parameters.AddWithValue("@sloc", "");
            insertCommand.Parameters.AddWithValue("@qty", 0M);
            insertCommand.Parameters.AddWithValue("@uom", "");
            insertCommand.Parameters.AddWithValue("@net", 0M);
            insertCommand.Parameters.AddWithValue("@tare", 0M);
            insertCommand.Parameters.AddWithValue("@act", 0M);
            insertCommand.Parameters.AddWithValue("@plan", 0M);
            insertCommand.Parameters.AddWithValue("@packedOn", DateTime.Now);
            insertCommand.Parameters.AddWithValue("@manu", DateTime.Now);
            insertCommand.Parameters.AddWithValue("@exp", DateTime.Now);
            insertCommand.Parameters.AddWithValue("@batch", "");
            insertCommand.Parameters.AddWithValue("@part", 0);
            insertCommand.Parameters.AddWithValue("@orig", 0);
            insertCommand.Parameters.AddWithValue("@term", "");
            insertCommand.Parameters.AddWithValue("@user", "");
            insertCommand.Parameters.AddWithValue("@freeze", 0);
            insertCommand.Parameters.AddWithValue("@sl_dates", "");
            insertCommand.Parameters.AddWithValue("@canc", 0);
            insertCommand.Prepare();

            getSerialCommand = DBOperations.myConn.CreateCommand();
            getSerialCommand.CommandText = "select last_insert_id();";
            getSerialCommand.Prepare();
        }
        #endregion

        #region InsertSingle
        public void InsertSingle()
        {
            insertCommand.Parameters[0].Value = orderNum;
            insertCommand.Parameters[1].Value = materialNum;
            insertCommand.Parameters[2].Value = plant;
            insertCommand.Parameters[3].Value = storage_lctn;
            insertCommand.Parameters[4].Value = qty;
            insertCommand.Parameters[5].Value = uom;
            insertCommand.Parameters[6].Value = netWeight;
            insertCommand.Parameters[7].Value = tareWeight;
            insertCommand.Parameters[8].Value = actualWeight;
            insertCommand.Parameters[9].Value = plannedWeight;
            insertCommand.Parameters[10].Value = packedOn;
            insertCommand.Parameters[11].Value = manuDate;
            insertCommand.Parameters[12].Value = expiryDate;
            insertCommand.Parameters[13].Value = batch;
            insertCommand.Parameters[14].Value = partCarton;
            insertCommand.Parameters[15].Value = origSerial;
            insertCommand.Parameters[16].Value = terminal;
            insertCommand.Parameters[17].Value = user;
            insertCommand.Parameters[18].Value = useFreezer;
            insertCommand.Parameters[19].Value = slaughterDates;
            insertCommand.Parameters[20].Value = cancelled;

            insertCommand.ExecuteNonQuery();

            serial = ulong.Parse(getSerialCommand.ExecuteScalar().ToString());
        }
        #endregion

        #region SendPacks
        public static bool SendPacks()
        {
            DBOperations.BeginTransaction();

            var packs = ReadUnsentPacks();
            if (packs.Count == 0)
            {
                DBOperations.CommitTransaction();
                return true;  //Okay - no cartons need to be sent
            }

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);

            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.MaxReceivedMessageSize = 1000000;
            var address = new EndpointAddress(CommonData.webServiceEndPoint);
            ZStandalonePackingClient client = new ZStandalonePackingClient(binding, address);

            var toSAP = new ZReceiptCartons();
            toSAP.Cartons = new ZsapaCarton[packs.Count];

            int i = 0;
            foreach(var p in packs)
            {
                toSAP.Cartons[i] = new ZsapaCarton();

                toSAP.Cartons[i].Serial = p.serial.ToString();
                toSAP.Cartons[i].MaterialNum = p.materialNum; 
                toSAP.Cartons[i].Batch = p.batch; 
                toSAP.Cartons[i].OrderNum = p.orderNum;
                toSAP.Cartons[i].Plant = p.plant; 
                toSAP.Cartons[i].StorageLctn = p.storage_lctn; 
                toSAP.Cartons[i].PackedQty = p.qty; 
                toSAP.Cartons[i].PackedUom = p.uom; 
                toSAP.Cartons[i].Weight = p.netWeight; 
                toSAP.Cartons[i].PlannedWeight = p.plannedWeight;
                toSAP.Cartons[i].ActualWeight = p.actualWeight; 
                toSAP.Cartons[i].TareWeight = p.tareWeight; 
                toSAP.Cartons[i].PackedOn = p.packedOn.ToString("yyyy-MM-dd");
                toSAP.Cartons[i].PackedAt = p.packedOn.ToString("HHmmss");
                toSAP.Cartons[i].ManuDate = p.manuDate.ToString("yyyy-MM-dd");
                toSAP.Cartons[i].ExpiryDate = p.expiryDate.ToString("yyyy-MM-dd");
                toSAP.Cartons[i].SlaughterDates = p.slaughterDates;
                toSAP.Cartons[i].Device = p.terminal; 
                toSAP.Cartons[i].UseFreezer = p.useFreezer ? "X" : ""; 
                toSAP.Cartons[i].DgaUser = p.user;
                toSAP.Cartons[i].GrReversal = p.cancelled ? "X" : "";
                i++;
            }         

            client.ClientCredentials.UserName.UserName = CommonData.sapSettings.user;
            client.ClientCredentials.UserName.Password = CryptoSystem.Decrypt(CommonData.sapSettings.password);

            ZReceiptCartonsResponse fromSAP;
            try
            {
                fromSAP = client.ZReceiptCartons(toSAP);
            }
            catch (Exception e)
            {
                MessageLogger.Add("Error sending packs to SAP " + e.ToString(), MessageLogger.MsgLevel.error);
                DBOperations.RollbackTransaction();
                return false;
            }
            
            foreach(var c in fromSAP.ReturnMsgs)
            {
                MessageLogger.Add("Flagging carton " + c.Serial + " " + c.Msg, MessageLogger.MsgLevel.info);

                FlagAsSent(ulong.Parse(c.Serial), c.Msg);
            }
            DBOperations.CommitTransaction();
            return true;
        }
        #endregion

        #region ReadUnsentPacks
        private static List<Pack> ReadUnsentPacks()
        {
            var packs = new List<Pack>();

            DBOperations.OpenDBConnection();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pck.pack WHERE sent_to_SAP IS NULL FOR UPDATE";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pack p = new Pack(rdr);
                    packs.Add(p);
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading packs to send to SAP", MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }
            return packs;
        }
        #endregion

        #region ReadSingle
        public static Pack ReadSingle(ulong serial)
        {
            DBOperations.BeginTransaction();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pck.pack WHERE serial=@serial";
            cmd.Parameters.AddWithValue("serial", serial);
            Pack p = null;

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    p = new Pack(rdr);
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add(string.Format("Error reading single pack {0}", serial), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }
            DBOperations.CommitTransaction();
            return p;
        }
        #endregion

        #region SetCancelled
        public bool SetCancelled()
        {
            try
            {
                DBOperations.OpenDBConnection();
                DBOperations.BeginTransaction();
                MySqlCommand cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "UPDATE pck.pack SET sent_to_SAP = null, cancelled = 1 where serial = @serial";
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add(string.Format("Error setting cancelled flag for pack {0}", serial), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                DBOperations.RollbackTransaction();
                return false;
            }
            this.cancelled = true;
            return true;
        }
        #endregion

        #region FlagAsSent
        private static void FlagAsSent(ulong serial, string msg)
        {
            try
            {
                DBOperations.OpenDBConnection();
                MySqlCommand cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "UPDATE pck.pack SET sent_to_SAP = now(), msg = @msg where serial = @serial";
                cmd.Parameters.AddWithValue("@msg", msg);
                cmd.Parameters.AddWithValue("@serial", serial);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add(string.Format("Error setting sent to SAP flag for pack {0}", serial), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
            }
        }
        #endregion
    }
}
