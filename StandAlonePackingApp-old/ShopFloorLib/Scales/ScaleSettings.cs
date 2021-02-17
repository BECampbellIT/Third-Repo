using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ShopFloorLib
{
    [XmlRootAttribute("ScaleSettings")]
    public class ScaleSettings
    {
        public string scaleType { set; get; }
        public bool localTare { set; get; }

        //Serial settings
        public string comPort { set; get; }
        public int baudRate { set; get; }
        public Parity parity { set; get; }
        public int dataBits { set; get; }
        public StopBits stopBits { set; get; }

        //Network settings
        public string hostname { set; get; }
        public int tcpPort { set; get; }
    }
}
