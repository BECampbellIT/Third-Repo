using System;
using System.Windows.Forms;
using ShopFloorLib;


namespace StandAlonePackingApp
{
    public partial class EditImageDialog : Form
    {
        PrinterImage myImage;

        public EditImageDialog(PrinterImage img)
        {
            InitializeComponent();
            myImage = img;
        }

        private void EditPrinterImageDialog_Load(object sender, EventArgs e)
        {
            if (myImage != null)
            {
                txtDescription.Text = myImage.description;
                txtFileName.Text = myImage.fullPath;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtDescription.Text.Length < 3)
            {
                MessageBox.Show("Please enter a decription for the image.", "Desription Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtFileName.Text.Length < 3)
            {
                MessageBox.Show("Please enter a File Name.", "File Name Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool ok;
            string opr;

            if (myImage == null)
            {
                // Creating a new image definition
                myImage = new PrinterImage(txtDescription.Text, txtFileName.Text);
                ok = myImage.CreateInDB();
                opr = "created";
            }
            else
            {
                // Updating an existing image definition
                ok = myImage.UpdateOnDB(txtDescription.Text, txtFileName.Text);
                opr = "updated";
            }

            if (ok)
            {
                MessageLogger.Add(string.Format("Image definition {0} filename {1} {2} on DB by {3}", myImage.description, myImage.fullPath, opr, ThisApp.user.name), 
                                        MessageLogger.MsgLevel.permanent);

                MessageBox.Show(string.Format("Image Definition '{0}' saved to DB",myImage.description), "New Image Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Error saving image definition to DB", "New Image Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }
    }
}
