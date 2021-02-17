using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScaleSimulator;
using System.ServiceModel;

namespace ShopFloorLib
{
    class SimulatorReader : ScaleReader
    {
        private IPCScaleSimContract channel;

        public SimulatorReader()
            : base()
        {
        }
        public override void Open()
        {
            base.Open();

            string address = "net.pipe://localhost.becampbell.com.au/shopfloorlib/ScaleSim";

            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            channel = ChannelFactory<IPCScaleSimContract>.CreateChannel(binding, ep);
        }
        public override void Close()
        {
            base.Close();   
        }
        public override string PollScaleWeight()
        {
            return "";
        }
        public override void InterpretResult(string res, out decimal gross, out decimal net, out decimal tare, out Stability stab)
        {
            try
            {
                int s = channel.getWeight(out gross);
                tare = 1;
                net = gross - tare;
                if (s == 0)
                    stab = Stability.stableWeight;
                else
                    stab = Stability.unstableWeight;
            }
            catch(Exception)
            {
                gross = tare = net = 0;
                stab = Stability.unknown;
            }
        }
        public override void SetTare(decimal newTare)
        {
            //Only supports local tare
        }
        public override ScaleInfo GetScaleTypeInfo()
        {
            var i = new ScaleInfo();
            i.scaleType = "Simulator";
            i.commsType = ScaleCommsType.none;
            i.description = "Simulated Scale application";
            return i;
        }
    }
}
