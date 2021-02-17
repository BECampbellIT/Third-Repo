using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ShopFloorLib
{
    [XmlRootAttribute("BarcodeScannerSettings")]
    public class BarcodeScannerSettings
    {
        public string readFrom { set; get; }
        public char prefix { set; get; }
        public char suffix { set; get; }

        //Serial settings
        public int baudRate { set; get; }
        public Parity parity { set; get; }
        public int dataBits { set; get; }
        public StopBits stopBits { set; get; }

        public override string ToString()
        {
            return string.Format("Read from={0} prefix={1} suffix={2} baud={3} partity={4} data bits={5} stop bits={6}",
                readFrom, prefix, suffix, baudRate, parity, dataBits, stopBits);
        }
    }
}
