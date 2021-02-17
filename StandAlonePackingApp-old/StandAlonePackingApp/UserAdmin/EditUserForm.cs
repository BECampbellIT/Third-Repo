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

namespace StandAlonePackingApp.UserAdmin
{
    public partial class EditUserForm : Form
    {
        private User myUser;

        public EditUserForm(User _user)
        {
            InitializeComponent();
            myUser = _user;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            this.Text = "Edit User - " + myUser.userId;
            txtUserName.Text = myUser.name;

            chkUserAdmin.Checked = myUser.accessUserAdmin;
            chkSendImage.Checked = myUser.accessSendImage;
            chkMaintainImage.Checked = myUser.accessMaintainImage;
            chkBlockImage.Checked = myUser.accessBlockImage;
            chkPackCarton.Checked = myUser.accessPackCarton;
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length < 3)
            {
                MessageBox.Show("Enter a User Name.", "Invalid User Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            myUser.accessUserAdmin = chkUserAdmin.Checked;
            myUser.accessSendImage = chkSendImage.Checked;
            myUser.accessMaintainImage = chkMaintainImage.Checked;
            myUser.accessBlockImage = chkBlockImage.Checked;
            myUser.accessPackCarton = chkPackCarton.Checked;
            myUser.name = txtUserName.Text;

            // Check that the user has at least one authorisation
            if (!(myUser.accessUserAdmin || myUser.accessSendImage || myUser.accessMaintainImage || myUser.accessBlockImage || myUser.accessPackCarton))
            {
                MessageBox.Show("Please specify at least one authorisation for the user.", "Invalid User Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (myUser.UpdateOnDB())
            {
                MessageBox.Show(string.Format("User {0} updated", myUser.userId), "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating user - see log for details", "Error Updating User", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
