using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    class Marel2200Reader : ScaleReader
    {
        TcpClient myClient;
        NetworkStream myStream;

        TcpClient myClient1;
        NetworkStream myStream1;

        public Marel2200Reader()
            : base()
        {
         
        }
        public override void Open()
        {
            myClient = new TcpClient(settings.hostname, settings.tcpPort);
            myStream = myClient.GetStream();

            myClient1 = new TcpClient(settings.hostname, settings.tcpPort - 2);
            myStream1 = myClient1.GetStream();

            base.Open();
        }
        public override void Close()
        {
            base.Close();
            myClient.Close();
        }
        public override string PollScaleWeight()
        {
            try
            {
                Byte[] request = new Byte[9];
                Byte[] response = new Byte[1024];

                const byte startChar = 2;
                const byte endChar = 3;
                const byte tabChar = 9;

                request[0] = startChar;
                request[1] = (byte)'(';
                request[2] = (byte)'8';
                request[3] = (byte)'7';
                request[4] = tabChar;
                request[5] = (byte)'1';
                request[6] = tabChar;
                request[7] = (byte)'1';
                request[8] = endChar;

                while (running)
                {
                    myStream.Write(request, 0, 9);

                    bool inRes = false;
                    bool finished = false;
                    string res = "";

                    Int32 bytes = myStream.Read(response, 0, response.Length);
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
                    return res;
                }
            }
            catch (IOException)
            {

            }
            return null;
        }
        public override void InterpretResult(string res, out decimal gross, out decimal net, out decimal tare, out Stability stab)
        {
            string[] words = res.Split('\t');
            if (words.Length != 11)
            {
                stab = Stability.unknown;
                net = gross = tare = 0;
                return;
            }
            if (words[6].StartsWith("s"))
                stab = Stability.stableWeight;
            else if (words[6].Equals("mnt"))
                stab = Stability.unstableWeight;
            else
                stab = Stability.unknown;

            gross = decimal.Parse(words[2]);
            tare = decimal.Parse(words[8]);
            net = gross - tare;
        }
        public override void SetTare(decimal newTare)
        {
            //.w.113.2:    0.390<0d><0a>
            Byte[] request = Encoding.ASCII.GetBytes(string.Format(".w.113.2:    {0}\r\n",newTare));

            try
            {
                myStream1.Write(request, 0, request.Length);
               
            }
            catch (IOException)
            {

            }                
        }
        public override ScaleInfo GetScaleTypeInfo()
        {
            var i = new ScaleInfo();
            i.scaleType = "Marel2200";
            i.commsType = ScaleCommsType.tcp;
            i.description = "Marel M2200 via Ethernet";
            return i;
        }
    }
}
