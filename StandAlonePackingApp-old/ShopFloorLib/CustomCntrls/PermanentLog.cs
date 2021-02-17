using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ShopFloorLib
{
    public static class PermanentLog
    {
        public struct LogRec
        {
            public DateTime created;
            public string msg;
        }
        public static bool Add(string msg)
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "INSERT INTO pck.log (msg) VALUES (@msg)";

                cmd.Parameters.AddWithValue("@msg", msg);

                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add("Error inserting record into PCK.LOG - see log for details", MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        public static List<LogRec> GetLogEntries(DateTime from, DateTime to)
        {
            var l = new List<LogRec>();

            DBOperations.OpenDBConnection();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM pck.log WHERE created >= @from AND created <= @to ORDER BY id DESC";

            MySqlDataReader rdr = null;

            try
            {
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var rec = new LogRec();
                    rec.created = rdr.GetDateTime(1);
                    rec.msg = rdr.GetString(2);
                    l.Add(rec);
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading log messages from DB " + ex.ToString(), MessageLogger.MsgLevel.critical);
                return null;
            }
            finally
            {
                if (rdr != null)
                    rdr.Close();
            }

            return l;
        }
    }
}
