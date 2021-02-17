using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ShopFloorLib
{
    public partial class ScaleSettingsCntrl : UserControl
    {
        List<ScaleReader.ScaleInfo> scaleTypes;

        public ScaleSettingsCntrl()
        {
            InitializeComponent();

            scaleTypes = ScaleReader.GetScaleTypes();
            GetScaleTypeValues();
            GetStopBitsValues();
            GetParityValues();

            ScaleSettings settings = (ScaleSettings)XMLConfigHandler.ReadConfig(typeof(ScaleSettings));

            if (settings != null)
                UpdateComponents(settings);
        }
        private void GetScaleTypeValues()
        {
            var scaleTypes = ScaleReader.GetScaleTypes();
            foreach(var st in scaleTypes)
            {
                cmboBxScaleType.Items.Add(st.scaleType);
            }
        }
        private void GetStopBitsValues()
        {
            foreach(var sb in Enum.GetValues(typeof(StopBits)))
            {
                cmboBxStopBits.Items.Add(sb.ToString());
            }
        }
        private void GetParityValues()
        {
            foreach (var sb in Enum.GetValues(typeof(Parity)))
            {
                cmboBxParity.Items.Add(sb.ToString());
            }
        }
        private void UpdateComponents(ScaleSettings settings)
        {
            cmboBxScaleType.SelectedIndex = scaleTypes.FindIndex(st => st.scaleType.Equals(settings.scaleType));
            chkBxLocalTare.Checked = settings.localTare;
            txtBxHostHame.Text = settings.hostname;
            txtBxPort.Text = settings.tcpPort.ToString();
            cmboBxCOMPort.Text = settings.comPort;
            cmboBxBaudRate.Text = settings.baudRate.ToString();
            cmboBxDataBits.Text = settings.dataBits.ToString();
            cmboBxParity.Text = settings.parity.ToString();
            cmboBxStopBits.Text = settings.stopBits.ToString();
        }
        private void cmboBxScaleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var commsType = scaleTypes[cmboBxScaleType.SelectedIndex].commsType;

            grpBxCOMPort.Enabled = commsType == ScaleReader.ScaleCommsType.serial;
            grpBxNetwork.Enabled = commsType == ScaleReader.ScaleCommsType.tcp;
        }
        public bool SaveSettings()
        {
            var settings = new ScaleSettings();

            settings.scaleType = scaleTypes[cmboBxScaleType.SelectedIndex].scaleType;
            settings.localTare = chkBxLocalTare.Checked;
            settings.hostname = txtBxHostHame.Text;
            try
            {
                settings.tcpPort = int.Parse(txtBxPort.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show("Invalid port for scale");
                return false;
            }
            settings.comPort = cmboBxCOMPort.Text;
            settings.baudRate = int.Parse(cmboBxBaudRate.Text);
            settings.dataBits = int.Parse(cmboBxDataBits.Text);
            settings.parity = (Parity) Enum.Parse(typeof(Parity), cmboBxParity.Text);
            settings.stopBits = (StopBits) Enum.Parse(typeof(StopBits), cmboBxStopBits.Text);

            return XMLConfigHandler.SaveConfig(settings);
        }
    }
}
