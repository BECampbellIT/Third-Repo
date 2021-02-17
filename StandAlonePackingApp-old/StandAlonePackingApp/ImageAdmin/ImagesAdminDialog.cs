using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ShopFloorLib;

namespace StandAlonePackingApp
{
    public partial class PrinterImagesDialog : Form
    {
        private List<PrinterImage> imageList;

        #region Constructors
        public PrinterImagesDialog()
        {
            InitializeComponent();

            //Add column headers
            listView1.Columns.Add("Id", 60);
            listView1.Columns.Add("Image Description", 250);
            listView1.Columns.Add("File Name", 300);
            listView1.Columns.Add("Status", 300);

            btnAddImage.Visible = btnDeleteImage.Visible = btnEditImage.Visible = ThisApp.user.accessMaintainImage;
            btnSendImage.Visible = btnRemoveImage.Visible = ThisApp.user.accessSendImage;
            btnLockImage.Visible = btnUnlockImage.Visible = ThisApp.user.accessBlockImage;

            UpdateImagesList();
        }
        #endregion

        #region UpdateImagesList
        private void UpdateImagesList()
        {
            imageList = PrinterImage.ReadPrinterImagesFromDB();

            listView1.Items.Clear();
            foreach (var image in imageList)
            {
                string[] arr = new string[4];
                ListViewItem itm;

                //add items to ListView
                arr[0] = image.id.ToString();
                arr[1] = image.description;
                arr[2] = image.fullPath;

                switch (image.onPrinter)
                {
                    case PrinterImage.ImageStatus.Loaded:
                        arr[3] = "On Printer; ";
                        break;
                    case PrinterImage.ImageStatus.NotLoaded:
                        arr[3] = "Not loaded; ";
                        break;
                    case PrinterImage.ImageStatus.Unknown:
                        arr[3] = "Unable to query printer; ";
                        break;
                }
                if (image.locked)
                    arr[3] += "Image Locked!";

                itm = new ListViewItem(arr);
                listView1.Items.Add(itm);
                
                if (image.locked)
                    listView1.Items[listView1.Items.Count-1].ForeColor = Color.Red;
            }
        }
        #endregion

        #region Image Maintenance buttons (Create, Edit, Delete) 
        private void btnAddImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessMaintainImage)
            {
                MessageBox.Show("You are not authorised to create image definitions", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            new EditImageDialog(null).ShowDialog();
            UpdateImagesList();
        }

        private void btnEditImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessMaintainImage)
            {
                MessageBox.Show("You are not authorised to edit image definitions", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var img = GetSelectedImage();
            if (img != null)
            {
                new EditImageDialog(img).ShowDialog();
                UpdateImagesList();
            }
        }

        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessMaintainImage)
            {
                MessageBox.Show("You are not authorised to delete image definitions", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var img = GetSelectedImage();
            if (img != null)
            {
                if( MessageBox.Show(string.Format("Do you want to delete Image Definition '{0}'", img.description), "Confirm Deletion", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                if (img.DeleteFromDB())
                {
                    MessageBox.Show(string.Format("Printer Image '{0}'", img.description), "Image Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageLogger.Add(string.Format("Image definition {0} deleted by {1}", img.description, ThisApp.user.name), MessageLogger.MsgLevel.permanent);
                    UpdateImagesList();
                }
            }
        }
        #endregion

        #region Send to Printer / Remove from Printer buttons
        private void btnSendImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessSendImage)
            {
                MessageBox.Show("You are not authorised to enable images", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var img = GetSelectedImage();
            if (img != null)
            {
                if (img.locked)
                    MessageBox.Show("Image is locked", "Enabling Image not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (img.SendToPrinter())
                {
                    UpdateImagesList();
                    MessageLogger.Add(string.Format("Image {0} sent to printer by {1}", img.description, ThisApp.user.name), MessageLogger.MsgLevel.permanent);
                }
             }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessSendImage)
            {
                MessageBox.Show("You are not authorised to disable images", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var img = GetSelectedImage();
            if (img != null)
            {
                if (img.RemoveFromPrinter())
                {
                    MessageLogger.Add(string.Format("Image {0} removed from printer by {1}", img.description, ThisApp.user.name), MessageLogger.MsgLevel.permanent);
                    UpdateImagesList();
                }
            }
        }
        #endregion

        #region Lock and Unlock image buttons
        private void btnLockImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessBlockImage)
            {
                MessageBox.Show("You are not authorised to lock images", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var img = GetSelectedImage();
            if (img != null)
            {
                if (img.onPrinter == PrinterImage.ImageStatus.Loaded)
                    img.RemoveFromPrinter();

                MessageLogger.Add(string.Format("Image {0} locked by {1}", img.description, ThisApp.user.name), MessageLogger.MsgLevel.permanent);

                img.SetLockedOnDB(true);
                UpdateImagesList();
            }
        }

        private void btnUnlockImage_Click(object sender, EventArgs e)
        {
            if (!ThisApp.user.accessBlockImage)
            {
                MessageBox.Show("You are not authorised to unlock images", "No Authorisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var img = GetSelectedImage();
            if (img != null)
            {
                MessageLogger.Add(string.Format("Image {0} unlocked by {1}", img.description, ThisApp.user.name), MessageLogger.MsgLevel.permanent);

                img.SetLockedOnDB(false);
                UpdateImagesList();
            }
        }
        #endregion

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private PrinterImage GetSelectedImage()
        {
            var selItms = listView1.SelectedItems;
            if (selItms.Count != 1)
            {
                MessageBox.Show("Please select an image");
                return null;
            }
            return imageList[selItms[0].Index];
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            new PermanentLogDialog().ShowDialog();
        }
    }
}
