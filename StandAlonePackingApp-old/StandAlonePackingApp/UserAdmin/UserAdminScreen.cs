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

namespace StandAlonePackingApp
{
    public partial class UserAdminScreen : Form
    {
        public UserAdminScreen()
        {
            InitializeComponent();
        }

        private void UserAdminScreen_Load(object sender, EventArgs e)
        {
            //Add column headers
            listView1.Columns.Add("User Id", 80);
            listView1.Columns.Add("User Name", 200);
            listView1.Columns.Add("Access Level", 500);

            UpdateUserList();
        }
        private void UpdateUserList()
        {
            ThisApp.userList = User.ReadUsersFromDB();

            listView1.Items.Clear();
            foreach (var user in ThisApp.userList)
            {
                string[] arr = new string[4];
                ListViewItem itm;
                //add items to ListView
                arr[0] = user.userId;
                arr[1] = user.name;

                if (user.accessPackCarton)
                    arr[2] = "Pack Cartons; ";

                if (user.accessUserAdmin)
                    arr[2] += "User Admin; ";

                if (user.accessSendImage)
                    arr[2] += "Send images to printer; ";

                if (user.accessBlockImage)
                    arr[2] += "Block images; ";

                if (user.accessMaintainImage)
                    arr[2] += "Define images; ";

                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            var createDia = new UserAdmin.CreateNewUserForm();
            createDia.ShowDialog();
            UpdateUserList();
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null)
                return;

            var dia = new UserAdmin.EditUserForm(user);
            dia.ShowDialog();
            UpdateUserList();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            var user = GetSelectedUser();
            if (user == null)
                return;

            var dia = new UserAdmin.ChangePasswordForm(user);
            dia.ShowDialog();
            UpdateUserList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            User user = GetSelectedUser();
            if (user == null)
                return;

            if ( MessageBox.Show(string.Format("Do you want to delete user {0}", user), "Confirm User Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                        == DialogResult.Yes )
            {
                if (user.DeleteFromDB())
                {
                    MessageBox.Show(string.Format("User {0} deleted", user), "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateUserList();
                }
                else
                {
                    MessageBox.Show(string.Format("Error deleting User {0} - see log", user), "Error Deleting User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private User GetSelectedUser()
        {
            var selItms = listView1.SelectedItems;
            if (selItms.Count != 1)
            {
                MessageBox.Show("Please select a user");
                return null;
            }
            return ThisApp.userList[selItms[0].Index];
        }
    }
}
