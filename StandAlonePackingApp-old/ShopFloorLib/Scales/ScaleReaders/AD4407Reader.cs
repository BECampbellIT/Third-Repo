using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    class AD4407Reader : ScaleReader
    {
        SerialPort mySerialPort;

        public AD4407Reader()
            : base()
        {
        }
        public override void Open()
        {
            base.Open();
            mySerialPort = new SerialPort(settings.comPort, settings.baudRate, settings.parity, settings.dataBits, settings.stopBits);
            SetTare(0M);
        }
        public override void Close()
        {
            base.Close();

            if (mySerialPort.IsOpen)
                mySerialPort.Close();
        }
        public override string PollScaleWeight()
        {
            if (!mySerialPort.IsOpen)
            {
                return "Serial port not open";
            }
            try 
            {
                mySerialPort.Write("RW,1\r\n");
                return mySerialPort.ReadLine();
            }
            catch (Exception ex)
            {
                MessageLogger.Add("Error Reading Serial Port " + ex.ToString(), MessageLogger.MsgLevel.error);
            }
            return null;
        }
        public override void InterpretResult(string res, out decimal gross, out decimal net, out decimal tare, out Stability stab)
        {
            string[] words = res.Split(',');
            if (words.Length != 5)
            {
                gross = net = tare = 0;
                stab = Stability.unknown;
                return;
            }
            if (words[0].Equals("ST"))
                stab = Stability.stableWeight;
            else if (words[0].Equals("US"))
                stab = Stability.unstableWeight;
            else
                stab = Stability.unknown;

            gross = decimal.Parse(words[1]);
            tare = decimal.Parse(words[3]);
            net = decimal.Parse(words[2]);
        }
        public override void SetTare(decimal newTare)
        {
            try
            {
                decimal decigrams = decimal.Round(newTare*100,0);
                string cmd = String.Format("PT,{0}\r\n", decigrams);
                if (!mySerialPort.IsOpen)
                {
                    mySerialPort.Open();
                }
                mySerialPort.Write(cmd);
            }
            catch (Exception ex)
            {
               MessageLogger.Add("Error sending Tare to Serial Port " + ex.ToString(), MessageLogger.MsgLevel.error);
            }
        }
        public override ScaleInfo GetScaleTypeInfo()
        {
            var i = new ScaleInfo();
            i.scaleType = "AD4407";
            i.commsType = ScaleCommsType.serial;
            i.description = "A&D 4407 via serial RS232";
            return i;
        }

    }
}
