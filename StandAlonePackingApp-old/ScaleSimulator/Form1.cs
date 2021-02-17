using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScaleSimulator
{
    public partial class Form1 : Form
    {
        public static NumericUpDown myNumWeight;
        public static RadioButton myStable;

        public Form1()
        {
            InitializeComponent();

            numWeight.Value = 15;
            radBtnStable.Checked = true;
            radBtnUnstable.Checked = false;

            myNumWeight = numWeight;
            myStable = radBtnStable;

            string address = "net.pipe://local.becampbell.com.au/shopfloorlib/ScaleSim";

            ServiceHost serviceHost = new ServiceHost(typeof(IPCScaleSimServer));
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            serviceHost.AddServiceEndpoint(typeof(IPCScaleSimContract), binding, address);
            serviceHost.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        class IPCScaleSimServer : IPCScaleSimContract
        {
            public int getWeight(out decimal gross)
            {
                gross = Form1.myNumWeight.Value;

                if (myStable.Checked)
                    return 0;
                else
                    return 1;
            }
        }
   }
}
