using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandAlonePackingLib;
using ShopFloorLib;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace StandAlonePackingApp
{
    class InterStationComms
    {
        const byte startChar = 2;
        const byte endChar = 3;

        public class ReceiptInfo
        {
            public string orderNum;
            public string materialNum;
            public decimal qtyPacked;
        }

        public static void ListenToOtherStation(ButtonMatrix bt)
        {
            // Get local IP V4 address
            var addr = GetLocalIPv4();
            if (addr == null)
            {
                MessageLogger.Add("Unable to determine local IP address for inter-station comms", MessageLogger.MsgLevel.warning);
                return;
            }

            MessageLogger.Add(string.Format("Listening to address {0}, port {1} for inter-station comms", 
                                                addr, CommonData.localSettings.ThisPackingStationPort), 
                              MessageLogger.MsgLevel.info);

            var listener = new TcpListener(addr, CommonData.localSettings.ThisPackingStationPort);
            
            listener.Start();

            while(true)
            {
                Byte[] response = new Byte[80];
                var client = listener.AcceptTcpClient();
                var stream = client.GetStream();

                Int32 bytes = stream.Read(response, 0, response.Length);
                bool finished = false;
                bool inRes = true;
                string res = "";

                for (int i = 0; i < bytes && !finished; i++)
                {
                    switch (response[i])
                    {
                        case startChar:
                            inRes = true;
                            break;

                        case endChar:
                            if (inRes)
                            {
                                finished = true;
                            }
                            break;

                        default:
                            if (inRes)
                            {
                                res += (char)response[i];
                            }
                            break;
                    }
                }
                var words = res.Split(',');
                if (words.Length == 3)
                {
                    string orderNum = words[0];
                    string materialNum = words[1];
                    decimal qtyPacked = decimal.Parse(words[2]);

                    var ord = CommonData.normalOrders.Find(o => o.materialNum.Equals(materialNum));
                    if (ord == null)
                        ord = CommonData.reworkOrders.Find(o => o.materialNum.Equals(materialNum));
                    if (ord != null)
                    {
                        var incOrd = ord.incOrders.Find(i => i.orderNum.Equals(orderNum));
                        if (incOrd != null)
                        {
                            ord.IncreaseDeliveredQty(qtyPacked, incOrd);
                            bt.UpdateButton(materialNum);
                        }
                    }
                }
            }
        }
        private static IPAddress GetLocalIPv4()
        {
            // Bastardised from: http://stackoverflow.com/a/28621250/2685650.

            return NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(ni => ni.OperationalStatus == OperationalStatus.Up
                    && ni.GetIPProperties().GatewayAddresses.FirstOrDefault() != null
                    && ni.GetIPProperties().UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork) != null
                )
                ?.GetIPProperties()
                .UnicastAddresses
                .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork)
                ?.Address
                ?? null;
        }
        public static void SendToOtherStation(object state)
        {
            ReceiptInfo ri = (ReceiptInfo)state;

            if (CommonData.localSettings.OtherPackingStationPort == 0)
                return;

            try
            {
                Byte[] request = Encoding.ASCII.GetBytes(string.Format("\x02{0},{1},{2}\x03", ri.orderNum, ri.materialNum, ri.qtyPacked));
                var client = new TcpClient(CommonData.localSettings.OtherPackingStationAddr, CommonData.localSettings.OtherPackingStationPort);
                var stream = client.GetStream();
                stream.Write(request, 0, request.Length);
            }
            catch (IOException)
            {
            }
            catch (SocketException)
            {
            }
        }
    }
}
