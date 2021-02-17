using System;
using System.IO;

namespace ShopFloorLib
{
    public class MessageLogger
    {
        public enum MsgLevel { additional, info, warning, error, critical, permanent };

        private static MessageLogCntrl msgLogCntrl = null;

        public static void SetLogControl(MessageLogCntrl _msglog)
        {
            msgLogCntrl = _msglog;
        }

        public static void Add(String msg, MsgLevel lvl)
        {
            string filename = "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            string l;

            switch (lvl)
            {
                case MsgLevel.critical: l = "CRITICAL"; break;
                case MsgLevel.error: l = "E"; break;
                case MsgLevel.warning: l = "W"; break;
                case MsgLevel.info: l = "I"; break;
                case MsgLevel.permanent: l = "Permanent log"; break;
                default: l = "?"; break;
            }

            try
            {
                StreamWriter logFile = File.AppendText(filename);
                logFile.WriteLine("{0:MM/dd/yy H:mm:ss.fff} : {1} {2}", DateTime.Now, l, msg);
                logFile.Close();
            }
            catch(Exception)
            {
                
            }

            if (msgLogCntrl != null && lvl != MsgLevel.additional)
                msgLogCntrl.Add(msg, lvl);

            if (lvl == MsgLevel.permanent)
                PermanentLog.Add(msg);
        }
    }
}
