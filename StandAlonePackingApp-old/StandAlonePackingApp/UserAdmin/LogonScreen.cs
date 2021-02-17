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
    public partial class LogonScreen : Form
    {
        public LogonScreen()
        {
            InitializeComponent();
        }

        private void LogonScreen_Load(object sender, EventArgs e)
        {
            // Check if another instance of this application is already running and close this instance if one is.
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("Application is already running in another window.", "Application Already Running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
                        List<User> users = User.ReadUsersFromDB();

            matrixUserList.Setup(8,1, users.Cast<ButtonMatrix.MatrixObject>().ToList());
            matrixUserList.ButtonSelected += HandleButtonSelected;

            this.Text += " - " + CommonData.sapSettings.device;

            txtPassword.Enter += TxtPassword_Enter;
            SetFieldsVisibility();
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Enter Password", txtPassword);
            dia.ShowDialog();
            SetFieldsVisibility();
        }

        private void SetFieldsVisibility()
        {
            if(ThisApp.user == null)
            {
                lblPassword.Visible = false;
                txtPassword.Visible = false;
                btnLogon.Visible = false;
            }
            else
            { 
                lblPassword.Visible = true;
                txtPassword.Visible = true;
                btnLogon.Visible = txtPassword.Text.Length > 0;
            }
        }
        private void HandleButtonSelected(object sender, ButtonMatrix.ButtonSelectedEventArgs e)
        {
            ThisApp.user = (User)e.obj;

            SetFieldsVisibility();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ThisApp.user = null;
            this.Close();
        }


        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (ThisApp.user.CheckPassword(txtPassword.Text))
            {
                this.Close();
            }
            else
            {
                SetFieldsVisibility();
                MessageBox.Show("The password you entered is incorrect.", "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
