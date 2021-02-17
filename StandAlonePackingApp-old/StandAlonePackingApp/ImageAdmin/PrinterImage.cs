using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ShopFloorLib;
using StandAlonePackingLib;
using System.Management;
using System.Net;
using System.IO;

namespace StandAlonePackingApp
{
    public class PrinterImage
    {
        #region Public attributes
        public enum ImageStatus { Loaded, NotLoaded, Unknown }

        public int id { private set; get; }
        public string description { private set; get; }
        public string fullPath { private set; get; }
        public ImageStatus onPrinter { private set; get; }
        public bool locked { private set; get; }

        public static string printerHost { get { if (_printerHost == null) { _printerHost = getPrinterIPAddress(); } return _printerHost; } }
        #endregion

        #region Public attributes
        private static string _printerHost = null;
        #endregion

        #region Constructors
        public PrinterImage(string _description, string _filename)
        {
            description = _description;
            fullPath = _filename;
            onPrinter = ImageStatus.Unknown;
        }

        public PrinterImage(MySqlDataReader rdr)
        {
            id = rdr.GetInt16(0);
            description = rdr.GetString(1);
            fullPath = rdr.GetString(2);
            locked = rdr.GetInt16(3) != 0;
            onPrinter = ImageStatus.Unknown;
        }
        #endregion

        /************************************ 
         * Database operations 
         * ************************************/
        #region ReadPrinterImagesFromDB
        public static List<PrinterImage> ReadPrinterImagesFromDB()
        {
            var images = new List<PrinterImage>();

            DBOperations.OpenDBConnection();
            MySqlCommand cmd = DBOperations.myConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM printer_image";

            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    images.Add(new PrinterImage(rdr));
                }
                rdr.Close();
            }
            catch (MySqlException ex)
            {
                MessageLogger.Add("Error reading printer images from DB - see log for details", MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return null;
            }
            // Look to see if the images are currently loaded on the printer
            var files = GetDirectoryListing();
            foreach (var img in images)
            {
                if (files == null)
                    img.onPrinter = ImageStatus.Unknown;
                else
                {
                    img.onPrinter = ImageStatus.NotLoaded;
                    string fileName = Path.GetFileName(img.fullPath);
                    if (files.Find(f => f.Equals(fileName)) != null)
                        img.onPrinter = ImageStatus.Loaded;
                }
            }
            return images;
        }
        #endregion

        #region CreateInDB
        public bool CreateInDB()
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "INSERT INTO pck.printer_image (description, file_name, locked) VALUES (@desc,@file,@locked)";

                cmd.Parameters.AddWithValue("@desc", description);
                cmd.Parameters.AddWithValue("@file", fullPath);
                cmd.Parameters.AddWithValue("@locked", locked);

                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error creating print image {0} in DB - see log for details", description), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region UpdateOnDB    
        public bool UpdateOnDB(string _description, string _filename)
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "UPDATE pck.printer_image SET description=@desc, file_name=@file WHERE id=@id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@desc", _description);
                cmd.Parameters.AddWithValue("@file", _filename);

                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error updating printer image '{0}' in DB - see log for details", description), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            description = _description;
            fullPath = _filename;
            return true;
        }
        #endregion

        #region SetLockedOnDB    
        public bool SetLockedOnDB(bool _locked)
        {
            DBOperations.OpenDBConnection();
            DBOperations.BeginTransaction();

            try
            {
                var cmd = DBOperations.myConn.CreateCommand();
                cmd.CommandText = "UPDATE pck.printer_image SET locked=@locked WHERE id=@id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@locked", _locked);

                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error updating lock flag for printer image '{0}' in DB - see log for details", description), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            locked = _locked;
            return true;
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
                cmd.CommandText = "DELETE FROM pck.printer_image WHERE id = @id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                DBOperations.CommitTransaction();
            }
            catch (MySqlException ex)
            {
                DBOperations.RollbackTransaction();
                MessageLogger.Add(string.Format("Error deleting printer image '{0}' from DB - see log for details", description), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        /*************************************
         * FTP operations 
         * ************************************/

        #region SendToPrinter
        public bool SendToPrinter()
        {
            if (printerHost == null)
                return false;

            try
            {
                var request = GetFtpRequest(string.Format("ftp://{0}/c/{1}", printerHost, Path.GetFileName(fullPath)));
                request.Method = WebRequestMethods.Ftp.UploadFile;

                // Copy the contents of the file to the request stream.  
                StreamReader sourceStream = new StreamReader(fullPath);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageLogger.Add(string.Format("Error uploading file {1} via FTP to printer host {0} - see log for details", printerHost, fullPath), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region RemoveFromPrinter
        public bool RemoveFromPrinter()
        {
            if (printerHost == null)
                return false;

            try
            {
                var request = GetFtpRequest(string.Format("ftp://{0}/c/{1}", printerHost, Path.GetFileName(fullPath)));
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {
                MessageLogger.Add(string.Format("Error deleting file {1} via FTP from printer host {0} - see log for details", printerHost, fullPath), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return false;
            }
            return true;
        }
        #endregion

        #region GetDirectoryListing
        private static List<string> GetDirectoryListing()
        {
            var files = new List<string>();

            if (printerHost == null)
                return null;

            try
            {
                FtpWebRequest request = GetFtpRequest(string.Format("ftp://{0}/c/", printerHost));
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                string fileName;
                while ((fileName = reader.ReadLine()) != null)
                {
                    files.Add(fileName);
                }
                reader.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                MessageLogger.Add(string.Format("Error getting file list via FTP for printer host {0} - see log for details", printerHost), MessageLogger.MsgLevel.error);
                MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                return null;
            }
            return files;
        }
        #endregion
        
        #region GetFTPReqest
        private static FtpWebRequest GetFtpRequest(string uri)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;
            request.Timeout = -1;
            request.Credentials = new NetworkCredential("admin", "pass");

            return request;
        }
        #endregion

        #region getPrinterIPAddress
        private static string getPrinterIPAddress()
        {
            if (ThisApp.printer == null)
                ThisApp.printer = new LabelPrinter();

            if (ThisApp.printer?.settings?.printerName == null)
                return null;

            string addr = null;
            string printerPort = null;

            // Get the Printer Port used by the given Printer
            using (var s1 = new ManagementObjectSearcher(string.Format("SELECT PortName from Win32_Printer WHERE Name LIKE '%{0}'", ThisApp.printer.settings.printerName)))
            {
                try
                {
                    foreach (var printer in s1.Get())
                    {
                        printerPort = printer["PortName"].ToString();
                        break;
                    }
                }
                catch (ManagementException ex)
                {
                    MessageLogger.Add(string.Format("Error getting Port for Printer {0} - see log for details", ThisApp.printer?.settings?.printerName), MessageLogger.MsgLevel.error);
                    MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                }
            }

            if (printerPort != null)
            {
                // Get the IP Address of the Printer Port
                using (var s2 = new ManagementObjectSearcher(string.Format("SELECT HostAddress from Win32_TCPIPPrinterPort WHERE Name LIKE '%{0}'", printerPort)))
                {
                    try
                    {
                        foreach (var port in s2.Get())
                        {
                            addr = port["HostAddress"].ToString();
                            break;
                        }
                    }
                    catch (ManagementException ex)
                    {
                        MessageLogger.Add(string.Format("Error getting Host Addreee for Printer Port {0} - see log for details", printerPort), MessageLogger.MsgLevel.error);
                        MessageLogger.Add(ex.ToString(), MessageLogger.MsgLevel.additional);
                    }
                }
            }
            return addr;
        }
        #endregion
    }
}
