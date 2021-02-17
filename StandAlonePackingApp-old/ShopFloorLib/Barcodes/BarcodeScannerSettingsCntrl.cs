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
    public partial class BarcodeScannerSettingsCntrl : UserControl
    {
        public BarcodeScannerSettingsCntrl()
        {
            InitializeComponent();

            GetStopBitsValues();
            GetParityValues();

            var settings = (BarcodeScannerSettings)XMLConfigHandler.ReadConfig(typeof(BarcodeScannerSettings));
            if (settings != null)
                UpdateComponents(settings);
        }
        private void UpdateComponents(BarcodeScannerSettings s)
        {
            if (s.prefix ==  'A')
                cmboBxPrefix.Text = "AIM Code";
            else
                cmboBxPrefix.Text = new string(s.prefix, 1);

            if (s.suffix == 'E')
                cmboBxSuffix.Text = "Enter";
            else
                cmboBxSuffix.Text = new string(s.suffix, 1);

            cmboBxInput.Text = s.readFrom;
            cmboBxBaudRate.Text = s.baudRate.ToString();
            cmboBxDataBits.Text = s.dataBits.ToString();
            cmboBxParity.Text = s.parity.ToString();
            cmboBxStopBits.Text = s.stopBits.ToString();
        }
        public bool SaveSettings()
        {
            var settings = new BarcodeScannerSettings();

            if (cmboBxPrefix.Text.Length > 0)
                settings.prefix = cmboBxPrefix.Text[0];

            if (cmboBxSuffix.Text.Length > 0)
                settings.suffix = cmboBxSuffix.Text[0];

            settings.readFrom = cmboBxInput.Text;
            settings.baudRate = int.Parse(cmboBxBaudRate.Text);
            settings.dataBits = int.Parse(cmboBxDataBits.Text);
            settings.parity = (Parity)Enum.Parse(typeof(Parity), cmboBxParity.Text);
            settings.stopBits = (StopBits)Enum.Parse(typeof(StopBits), cmboBxStopBits.Text);

            return XMLConfigHandler.SaveConfig(settings);
        }
        private void GetStopBitsValues()
        {
            foreach (var sb in Enum.GetValues(typeof(StopBits)))
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

        private void cmboBxInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            grpBxCOMPort.Enabled = !cmboBxInput.Text.Equals("Keyboard");
        }
    }
}
