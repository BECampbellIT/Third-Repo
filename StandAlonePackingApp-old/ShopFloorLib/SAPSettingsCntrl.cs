using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopFloorLib
{
    public partial class SAPSettingsCntrl : UserControl
    {
        public SAPSettingsCntrl()
        {
            InitializeComponent();

            var settings = (SAPSettings)XMLConfigHandler.ReadConfig(typeof(SAPSettings));

            if (settings != null)
                UpdateComponents(settings);
        }
        private void UpdateComponents(SAPSettings s)
        {
            txtSAPHost.Text = s.hostname;
            txtSAPUser.Text = s.user;
            txtSAPPassword.Text = CryptoSystem.Decrypt(s.password);
            txtSAPClient.Text = s.client;
            txtDevice.Text = s.device;
        }
        public bool SaveSettings()
        {
            var s= new SAPSettings();

            s.hostname = txtSAPHost.Text;
            s.user = txtSAPUser.Text;
            s.password = CryptoSystem.Encrypt(txtSAPPassword.Text);
            s.client = txtSAPClient.Text;
            s.device = txtDevice.Text;

            return XMLConfigHandler.SaveConfig(s);
        }
    }
}
