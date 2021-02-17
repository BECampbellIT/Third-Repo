using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShopFloorLib;
using System.Drawing;

namespace StandAlonePackingLib
{
    public class User : ButtonMatrix.MatrixObject
    {
        #region Public Attributes
        public string userId { get; private set; }
        public string name { get; set; }
        public string password { get; private set; }
        public bool accessUserAdmin { get; set; }
        public bool accessMaintainImage { get; set; }
        public bool accessSendImage { get; set; }
        public bool accessBlockImage { get; set; }
        public bool accessPackCarton { get; set; }
        #endregion

        #region Private Attributes
        private const short UserAdmin = 1;
        private const short MaintainImage = 2;
        private const short SendImage = 4;
        private const short BlockImage = 8;
        private const short PackCarton = 16;
        #endregion

        #region Constructors
        public User(string _userId)
        {
            userId = _userId;
        }
        public User(string _userId, string _userName, string _password)
        {
            userId = _userId;
            name = _userName;
            password = CryptoSystem.Encrypt(_password);
        }
        public User(MySqlDataReader rdr)
        {
            userId = rdr.GetString(0);
            name = rdr.GetString(1);
            password = rdr.GetString(2);
            setPermissions(rdr.GetInt16(3));
        }
        #endregion

        #region set/get Permissions
        private void setPermissions(short access)
        {
            accessUserAdmin = (access & User.UserAdmin) != 0;
            accessMaintainImage = (access & User.MaintainImage) != 0;
            accessSendImage = (access & User.SendImage) != 0;
            accessBlockImage = (access & User.BlockImage) != 0;
            accessPackCarton = (access & User.PackCarton) != 0;
        }

        short getPermissions()
        {
            short access = 0;

            if (accessUserAdmin) access |= User.UserAdmin;
            if (accessMaintainImage) access |= User.MaintainImage;
            if (accessSendImage) access |= User.SendImage;
            if (accessBlockImage) access |= User.BlockImage;
            if (accessPackCarton) access |= User.PackCarton;

            return access;
        }
        #endregion

        #region ReadUsersFromDB
        public static List<User> ReadUsersFromDB()
        {
            var users = new List<User>();

            DBOperations.OpenDBConnection();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM user";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var user = new User(rdr);
                    users.Add(user);
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading users from DB " + ex.ToString(), MessageLogger.MsgLevel.critical);
                return null;
            }
            if (users.Count == 0)
            {
                var user = new User("001");
                user.name = "Administrator";
                user.password = CryptoSystem.Encrypt("12345");
                user.accessUserAdmin = true;
                users.Add(user);

                user = new User("002");
                user.name = "Packer 1";
                user.password = CryptoSystem.Encrypt("1234");
                user.accessPackCarton = true;
                users.Add(user);

            }
            return users;
        }
        #endregion

        #region CreateInDB
        public bool CreateInDB()
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                short access = getPermissions();

                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "INSERT INTO pck.user VALUES (@id,@name,@password,@access)";

                cmd.Parameters.AddWithValue("@id", userId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@access", access);


                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error creating user {0} in DB - see log for details", userId), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region UpdateOnDB
        public bool UpdateOnDB()
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                short access = getPermissions();

                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "UPDATE pck.user SET name=@name, password=@password, access_level=@access WHERE user_id=@id";

                cmd.Parameters.AddWithValue("@id", userId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@access", access);

                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error updating user {0} in DB - see log for details", userId), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region UpdatePassword
        public bool UpdatePassword(string newPassword)
        {
            password = CryptoSystem.Encrypt(newPassword);
            return UpdateOnDB();
        }
        #endregion

        #region DeleteFromDB
        public bool DeleteFromDB()
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "DELETE FROM pck.user WHERE user_id=@id";

                cmd.Parameters.AddWithValue("@id", userId);


                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error deleting user {0} from DB - see log for details", userId), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region CheckPassword
        public bool CheckPassword(string passwordEntered)
        {
            return password.Equals(CryptoSystem.Encrypt(passwordEntered));
        }
        #endregion

        #region methods from MatrixObject interface
        /************************************
         * Implement methods from MatrixObject interface 
         ************************************/
        override public string ToString()
        {
            return string.Format("{0} - {1}", userId, name);
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
            return userId;
        }
        #endregion
    }
}
