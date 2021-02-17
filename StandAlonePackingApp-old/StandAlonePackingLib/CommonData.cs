using ShopFloorLib;
using System;
using System.Collections.Generic;

namespace StandAlonePackingLib
{
    public static class CommonData
    {
        public static List<Material> mats;
        public static List<Order> normalOrders;
        public static List<Order> reworkOrders;
        public static List<Order.PackedOn> slDates;

        public static LocalSettings localSettings;
        public static string filter = "";
        public static SAPSettings sapSettings;

        public static bool Initialise()
        {
            localSettings = (LocalSettings)XMLConfigHandler.ReadConfig(typeof(LocalSettings));
            if (localSettings==null)
            {
                MessageLogger.Add("Error ready LocalSettings.XML", MessageLogger.MsgLevel.error);
                return false;
            }

            sapSettings = (SAPSettings)XMLConfigHandler.ReadConfig(typeof(SAPSettings));
            if (localSettings == null)
            {
                MessageLogger.Add("Error ready SAPSettings.XML", MessageLogger.MsgLevel.error);
                return false;
            }

            if (!DBOperations.Initialise())
                return false;

            MessageLogger.Add(String.Format("Settings read. Web Service Binding: {0} SAP user: {1}", webServiceEndPoint, sapSettings.user),
                            MessageLogger.MsgLevel.info);
            return true;
        }

        public static string webServiceEndPoint
        {
            set {}
            get { return "http://" + sapSettings.hostname + ":8000/sap/bc/srt/rfc/sap/zstandalonepacking/" + sapSettings.client + "/zsapa/b1"; }
        }
    }
}
