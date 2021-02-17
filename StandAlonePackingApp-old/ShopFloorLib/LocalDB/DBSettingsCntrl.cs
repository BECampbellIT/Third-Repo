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
    public partial class DBSettingsCntrl : UserControl
    {
        public DBSettingsCntrl()
        {
            InitializeComponent();

            DBSettings s = (DBSettings)XMLConfigHandler.ReadConfig(typeof(DBSettings));
            if (s != null)
            {
                txtDBHost.Text = s.DBServer;
                txtDBUser.Text = s.DBUser;
                txtDBPassword.Text = CryptoSystem.Decrypt(s.DBPassword);
            }
        }
        public bool SaveSettings()
        {
            var settings = new DBSettings();

            settings.DBServer = txtDBHost.Text;
            settings.DBUser = txtDBUser.Text;
            settings.DBPassword = CryptoSystem.Encrypt(txtDBPassword.Text);

            return XMLConfigHandler.SaveConfig(settings);
        }
    }
}
