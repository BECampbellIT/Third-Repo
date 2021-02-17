using MySql.Data.MySqlClient;

namespace ShopFloorLib
{
    public static class DBOperations
    {
        public static MySqlConnection myConn;
        public static DBSettings settings;

        private static MySqlTransaction trans = null;

        public static bool Initialise()
        {
            settings = (DBSettings)XMLConfigHandler.ReadConfig(typeof(DBSettings));
            if (settings == null)
            {
                MessageLogger.Add("Error ready DBSettings.XML", MessageLogger.MsgLevel.error);
                return false;
            }

            return true;
        }

        public static bool OpenDBConnection()
        {
            if (myConn != null)
                return true;

            if (myConn == null)
            {
                string cs = string.Format("server={0};userid={1};password={2};database=pck",
                                       settings.DBServer,
                                       settings.DBUser,
                                       CryptoSystem.Decrypt(settings.DBPassword));
                try
                {
                    myConn = new MySqlConnection(cs);
                    myConn.Open();

                    //MySqlCommand cmd = myConn.CreateCommand();
                    //cmd.CommandText = "SET autocommit = 0";
                    //cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageLogger.Add("Error opening DB connection " + ex.ToString(), MessageLogger.MsgLevel.error);
                    return false;
                }
            }
            return true;
        }
        public static void BeginTransaction()
        {
            OpenDBConnection();
            if (trans == null)
            {
                trans = myConn.BeginTransaction();
            }
        }
        public static void CommitTransaction()
        {
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
                trans = null;
            }
        }
        public static void RollbackTransaction()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
                trans = null;
            }
        }

    }
}
