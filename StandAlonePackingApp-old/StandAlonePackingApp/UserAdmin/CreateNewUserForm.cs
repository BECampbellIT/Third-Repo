using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopFloorLib;
using StandAlonePackingLib;

namespace StandAlonePackingApp.UserAdmin
{
    public partial class CreateNewUserForm : Form
    {
        public CreateNewUserForm()
        {
            InitializeComponent();

            txtUserId.Click += TxtUserId_Click;
            txtPassword.Click += TxtPassword_Click;
            txtRepeatPassword.Click += TxtRepeatPassword_Click;
        }

        private void TxtRepeatPassword_Click(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Repeat Password", txtRepeatPassword);
            dia.ShowDialog();
        }

        private void TxtPassword_Click(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Enter Initial Password", txtPassword);
            dia.ShowDialog();
        }

        private void TxtUserId_Click(object sender, EventArgs e)
        {
            var dia = new NumericKeypadDialog("Enter User Id", txtUserId);
            dia.ShowDialog();
        }

        private void CreateNewUser_Load(object sender, EventArgs e)
        {
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtRepeatPassword.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if(txtUserId.Text.Length <3)
            {
                MessageBox.Show("User Id must be at least 3 digits long.", "Invalid User Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // See if user id is already used
            if(ThisApp.userList.Find(u => u.userId == txtUserId.Text) != null)
            {
                MessageBox.Show(string.Format("User {0} already exists.", txtUserId.Text), "Invalid User Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            if (txtUserName.Text.Length < 3)
            {
                MessageBox.Show("Enter a User Name.", "Invalid User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User newUser = new User(txtUserId.Text, txtUserName.Text, txtPassword.Text);
            newUser.accessUserAdmin = chkUserAdmin.Checked;
            newUser.accessMaintainImage = chkSendImage.Checked;
            newUser.accessSendImage = chkSendImage.Checked;
            newUser.accessBlockImage = chkBlockImage.Checked;
            newUser.accessPackCarton = chkPackCarton.Checked;

            // Check that the user has at least one authorisation
            if (!(newUser.accessUserAdmin || newUser.accessSendImage || newUser.accessMaintainImage || newUser.accessBlockImage || newUser.accessPackCarton))
            {
                MessageBox.Show("Please specify at least one authorisation for the user.", "Invalid User Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newUser.CreateInDB())
            {
                MessageBox.Show(string.Format("User {0} created", newUser.userId), "New User Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ThisApp.userList.Add(newUser);
                this.Close();
            }                
            else
            {
                MessageBox.Show("Error creating new user - see log for details", "Error Creating New User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
