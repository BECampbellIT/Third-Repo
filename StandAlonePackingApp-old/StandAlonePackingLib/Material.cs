using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandAlonePackingLib.SAPFuncs;
using System.ServiceModel;
using MySql.Data.MySqlClient;
using ShopFloorLib;

namespace StandAlonePackingLib
{
    public class Material
    {
        #region Class Attributes
        public string matNumber { get; private set; }
        public string description { get; private set; }
        public string labelLine1 { get; private set; }
        public string labelLine2 { get; private set; }
        public string labelLine3 { get; private set; }
        public string labelLine4 { get; private set; }
        public bool fixedWeight { get; private set; }
        public decimal maxWeight { get; private set; }
        public decimal minWeight { get; private set; }
        public decimal nomWeight { get; private set; }
        public string baseUom { get; private set; }
        public string parallelUom { get; private set; }
        public bool unlimOverdel { get; private set; }
        public decimal overdelPerc { get; private set; }
        public string Ean { get; private set; }
        public bool madeToOrd { get; private set; }
        public string labelFile { get; private set; }
        public decimal shelfLife { get; private set; }
        public string custMatNumber { get; private set; }
        public string oldMatNumber { get; private set; }
        public string targetMatNumber { get; private set; }
        public string woolGrp { get; private set; }
        #endregion

        #region Constructors
        public Material(string _matNumber)
        {
            this.matNumber = _matNumber;        
        }
        #endregion

        #region readMaterialsFromSAP
        public static bool readMaterialsFromSAP(out List<Material> mats)
        {
            mats = new List<Material>();
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            binding.MaxReceivedMessageSize = 5000000;

            var address = new EndpointAddress(CommonData.webServiceEndPoint);

            ZStandalonePackingClient client = new ZStandalonePackingClient(binding, address);
            ZGetFinishedGoodMaterials toSAP = new ZGetFinishedGoodMaterials();
            toSAP.Device = CommonData.sapSettings.device;

            client.ClientCredentials.UserName.UserName = CommonData.sapSettings.user;
            client.ClientCredentials.UserName.Password = CryptoSystem.Decrypt(CommonData.sapSettings.password);
            ZGetFinishedGoodMaterialsResponse fromSAP;

            try
            {
                fromSAP = client.ZGetFinishedGoodMaterials(toSAP);
            }
            catch (CommunicationException ex)
            {
                MessageLogger.Add("Error reading materials from SAP", MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }

            foreach(ZsapaFinGoodMat finGood in fromSAP.FinGoods)
            {
                Material newMat = new Material(finGood.MatNumber);
                newMat.description = finGood.Description;
                newMat.labelLine1 = finGood.LabelLine1;
                newMat.labelLine2 = finGood.LabelLine2;
                newMat.labelLine3 = finGood.LabelLine3;
                newMat.labelLine4 = finGood.LabelLine4;
                newMat.maxWeight = limitWeight(finGood.MaxWeight);
                newMat.minWeight = limitWeight(finGood.MinWeight);
                newMat.nomWeight = limitWeight(finGood.NomWeight);
                newMat.baseUom = finGood.BaseUom;
                newMat.parallelUom = finGood.ParallelUom;
                newMat.overdelPerc = finGood.OverdelPerc;
                newMat.Ean = finGood.Ean;
                newMat.labelFile = finGood.LabelFile;
                newMat.shelfLife = finGood.ShelfLife;
                newMat.madeToOrd = finGood.MadeToOrd.Equals("X");
                newMat.fixedWeight = finGood.FixedWeight.Equals("X");
                newMat.unlimOverdel = finGood.UnlimOverdel.Equals("X");
                newMat.custMatNumber = finGood.CustMatNum;
                newMat.oldMatNumber = finGood.OldMatNum;
                newMat.targetMatNumber = finGood.TargetMatNum;
                newMat.woolGrp = finGood.WoolGrp;

                mats.Add(newMat);
            }
            return true;
        }
        #endregion

        #region limitWeight
        private static decimal limitWeight(decimal w)
        {
            return w < 999 ? w : 999;
        }
        #endregion

        #region saveMaterialsToDB
        public static string saveMaterialsToDB(List<Material> mats, bool refreshDB)
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "DELETE FROM pck.material";
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "INSERT INTO pck.material VALUES (@mat_number,@desc,@l1,@l2,@l3,@l4,@fw,@max,@min,@nom,@buom,@puom,@unlim,@overdel,@EAN,@m2o,@label,@shelf,@cust,@old,@target,@wool)";
            bool first = true;

            foreach(Material mat in mats)
            {
                if (first)
                {
                    cmd.Parameters.AddWithValue("@mat_number", mat.matNumber);
                    cmd.Parameters.AddWithValue("@desc", mat.description);
                    cmd.Parameters.AddWithValue("@l1", mat.labelLine1);
                    cmd.Parameters.AddWithValue("@l2", mat.labelLine2);
                    cmd.Parameters.AddWithValue("@l3", mat.labelLine3);
                    cmd.Parameters.AddWithValue("@l4", mat.labelLine4);
                    cmd.Parameters.AddWithValue("@fw", mat.fixedWeight);
                    cmd.Parameters.AddWithValue("@max", mat.maxWeight);
                    cmd.Parameters.AddWithValue("@min", mat.minWeight);
                    cmd.Parameters.AddWithValue("@nom", mat.nomWeight);
                    cmd.Parameters.AddWithValue("@buom", mat.baseUom);
                    cmd.Parameters.AddWithValue("@puom", mat.parallelUom);
                    cmd.Parameters.AddWithValue("@unlim", mat.unlimOverdel);
                    cmd.Parameters.AddWithValue("@overdel", mat.overdelPerc);
                    cmd.Parameters.AddWithValue("@EAN", mat.Ean);
                    cmd.Parameters.AddWithValue("@m2o", mat.madeToOrd);
                    cmd.Parameters.AddWithValue("@label", mat.labelFile);
                    cmd.Parameters.AddWithValue("@shelf", mat.shelfLife);
                    cmd.Parameters.AddWithValue("@cust", mat.custMatNumber);
                    cmd.Parameters.AddWithValue("@old", mat.oldMatNumber);
                    cmd.Parameters.AddWithValue("@target", mat.targetMatNumber);
                    cmd.Parameters.AddWithValue("@wool", mat.woolGrp);
                    cmd.Prepare();
                    first = false;
                }
                else
                {
                    cmd.Parameters[0].Value = mat.matNumber;
                    cmd.Parameters[1].Value = mat.description;
                    cmd.Parameters[2].Value = mat.labelLine1;
                    cmd.Parameters[3].Value = mat.labelLine2;
                    cmd.Parameters[4].Value = mat.labelLine3;
                    cmd.Parameters[5].Value = mat.labelLine4;
                    cmd.Parameters[6].Value = mat.fixedWeight;
                    cmd.Parameters[7].Value = mat.maxWeight;
                    cmd.Parameters[8].Value = mat.minWeight;
                    cmd.Parameters[9].Value = mat.nomWeight;
                    cmd.Parameters[10].Value = mat.baseUom;
                    cmd.Parameters[11].Value = mat.parallelUom;
                    cmd.Parameters[12].Value = mat.unlimOverdel;
                    cmd.Parameters[13].Value = mat.overdelPerc;
                    cmd.Parameters[14].Value = mat.Ean;
                    cmd.Parameters[15].Value = mat.madeToOrd;
                    cmd.Parameters[16].Value = mat.labelFile;
                    cmd.Parameters[17].Value = mat.shelfLife;
                    cmd.Parameters[18].Value = mat.custMatNumber;
                    cmd.Parameters[19].Value = mat.oldMatNumber;
                    cmd.Parameters[20].Value = mat.targetMatNumber;
                    cmd.Parameters[21].Value = mat.woolGrp;
                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    DBOperations.RollbackTransaction();
                    MessageLogger.Add(string.Format("Error saving material {0} to DB - see log for details", mat.matNumber), MessageLogger.MsgLevel.error);
                    MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                    return null;
                }

            }

            DBOperations.CommitTransaction();
            return null;
        }
        #endregion

        #region readMaterialsFromDB
        public static List<Material> readMaterialsFromDB()
        {
            var mats = new List<Material>();

            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM material";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Material mat = new Material(rdr.GetString("mat_number"));

                    mat.description = rdr.GetString("description");
                    mat.labelLine1 = rdr.GetString("label_line_1");
                    mat.labelLine2 = rdr.GetString("label_line_2");
                    mat.labelLine3 = rdr.GetString("label_line_3");
                    mat.labelLine4 = rdr.GetString("label_line_4");
                    mat.fixedWeight = rdr.GetBoolean("fixed_weight");
                    mat.maxWeight = rdr.GetDecimal("max_weight");
                    mat.minWeight = rdr.GetDecimal("min_weight");
                    mat.nomWeight = rdr.GetDecimal("nom_weight");
                    mat.baseUom = rdr.GetString("base_uom");
                    mat.parallelUom = rdr.GetString("parallel_uom");
                    mat.unlimOverdel = rdr.GetBoolean("unlim_overdel");
                    mat.overdelPerc = rdr.GetDecimal("overdel_perc");
                    mat.Ean = rdr.GetString("EAN");
                    mat.madeToOrd = rdr.GetBoolean("made_to_ord");
                    mat.labelFile = rdr.GetString("label_file");
                    mat.shelfLife = rdr.GetDecimal("shelf_life");
                    mat.custMatNumber = rdr.GetString("cust_mat_number");
                    mat.oldMatNumber = rdr.GetString("old_mat_number");
                    mat.targetMatNumber = rdr.GetString("target_mat_number");
                    mat.woolGrp = rdr.GetString("wool_grp");

                    mats.Add(mat);
                }
                rdr.Close();
            }
            catch(MySqlException ex)
            {
                MessageLogger.Add("Error reading materials from DB " + ex.ToString(), MessageLogger.MsgLevel.critical);
                DBOperations.RollbackTransaction();
                return null;
            }
            DBOperations.CommitTransaction();
            return mats;
        }
        #endregion
    }
}
