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

namespace StandAlonePackingApp.UserAdmin
{
    public partial class ChangePasswordForm : Form
    {
        private User myUser;

        public ChangePasswordForm(User _user)
        {
            InitializeComponent();
            myUser = _user;

            txtPassword.Enter += TxtPassword_Enter;
            txtRepeatPassword.Enter += TxtRepeatPassword_Enter;
        }
        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Enter new Password", txtPassword);
            dia.ShowDialog();
        }

        private void TxtRepeatPassword_Enter(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Repeat Password", txtRepeatPassword);
            dia.ShowDialog();
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtRepeatPassword.Text = "";
            this.Text = "Reset Password for " + myUser.ToString();
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length < 3 || txtRepeatPassword.Text.Length < 3)
            {
                MessageBox.Show("Password must be a least 3 digits long.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtPassword.Text != txtRepeatPassword.Text)
            {
                MessageBox.Show("Password and Repeat Password are different.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (myUser.UpdatePassword(txtPassword.Text))
            {
                MessageBox.Show(string.Format("Password changed for user {0}", myUser), "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error resetting password for user - see log", "Error Resetting Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
