using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StandAlonePackingLib;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class SettingsScreen : Form
    {
        public SettingsScreen()
        {
            InitializeComponent();

            LocalSettings s =   (LocalSettings)XMLConfigHandler.ReadConfig(typeof(LocalSettings));
            if (s != null)
            {
                chkBxReadMats.Checked = s.PullMaterialsAtStartup;
                chkBxReadOrders.Checked = s.PullOrdersAtStartup;
                txtOtherStationAddr.Text = s.OtherPackingStationAddr;
                txtOtherStationPort.Text = s.OtherPackingStationPort.ToString();
                txtThisStationPort.Text = s.ThisPackingStationPort.ToString();

                if (s.CartonSendInterval >= numCartonSendInterval.Minimum && s.CartonSendInterval <= numCartonSendInterval.Maximum)
                    numCartonSendInterval.Value = s.CartonSendInterval;

                if (s.OrderReadInterval >= numOrderReadInt.Minimum && s.OrderReadInterval <= numOrderReadInt.Maximum)
                    numOrderReadInt.Value = s.OrderReadInterval;

                if (s.MaterialReadInterval >= numMaterialReadInt.Minimum && s.MaterialReadInterval <= numMaterialReadInt.Maximum)
                    numMaterialReadInt.Value = s.MaterialReadInterval;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var s = new LocalSettings();

            s.PullMaterialsAtStartup = chkBxReadMats.Checked;
            s.PullOrdersAtStartup = chkBxReadOrders.Checked;
            s.CartonSendInterval = (int)numCartonSendInterval.Value;
            s.OrderReadInterval = (int)numOrderReadInt.Value;
            s.MaterialReadInterval = (int)numMaterialReadInt.Value;
            s.OtherPackingStationAddr = txtOtherStationAddr.Text;

            try
            {
                s.OtherPackingStationPort = int.Parse(txtOtherStationPort.Text);
                s.ThisPackingStationPort = int.Parse(txtThisStationPort.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Invalid port number. Use 0 for no inter-station comms", "Invalid port number");
                return;
            }
            XMLConfigHandler.SaveConfig(s);
            if (!scaleSettingsCntrl1.SaveSettings())
                return;

            if (!sapSettingsCntrl1.SaveSettings())
                return;

            if (!labelPrinterSettingsCntrl1.SaveSettings())
                return;

            if (!barcodeScannerSettingsCntrl1.SaveSettings())
                return;

            if (!dbSettingsCntrl1.SaveSettings())
                return;

            Application.Exit();
        }
    }
}
