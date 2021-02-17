namespace StandAlonePackingApp.UserAdmin
{
    partial class EditUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnSaveUser = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.chkBlockImage = new System.Windows.Forms.CheckBox();
            this.chkSendImage = new System.Windows.Forms.CheckBox();
            this.chkUserAdmin = new System.Windows.Forms.CheckBox();
            this.chkMaintainImage = new System.Windows.Forms.CheckBox();
            this.chkPackCarton = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(188, 37);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(275, 22);
            this.txtUserName.TabIndex = 9;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(12, 37);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(86, 16);
            this.lblUserName.TabIndex = 8;
            this.lblUserName.Text = "User Name";
            // 
            // btnSaveUser
            // 
            this.btnSaveUser.BackColor = System.Drawing.Color.Green;
            this.btnSaveUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveUser.Location = new System.Drawing.Point(24, 297);
            this.btnSaveUser.Name = "btnSaveUser";
            this.btnSaveUser.Size = new System.Drawing.Size(150, 39);
            this.btnSaveUser.TabIndex = 13;
            this.btnSaveUser.Text = "Save User";
            this.btnSaveUser.UseVisualStyleBackColor = false;
            this.btnSaveUser.Click += new System.EventHandler(this.btnSaveUser_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Crimson;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(367, 297);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(96, 39);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chkBlockImage
            // 
            this.chkBlockImage.AutoSize = true;
            this.chkBlockImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBlockImage.Location = new System.Drawing.Point(12, 240);
            this.chkBlockImage.Name = "chkBlockImage";
            this.chkBlockImage.Size = new System.Drawing.Size(318, 20);
            this.chkBlockImage.TabIndex = 16;
            this.chkBlockImage.Text = "Block sending controlled images to printer";
            this.chkBlockImage.UseVisualStyleBackColor = true;
            // 
            // chkSendImage
            // 
            this.chkSendImage.AutoSize = true;
            this.chkSendImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSendImage.Location = new System.Drawing.Point(12, 201);
            this.chkSendImage.Name = "chkSendImage";
            this.chkSendImage.Size = new System.Drawing.Size(285, 20);
            this.chkSendImage.TabIndex = 15;
            this.chkSendImage.Text = "Can send controlled images to printer";
            this.chkSendImage.UseVisualStyleBackColor = true;
            // 
            // chkUserAdmin
            // 
            this.chkUserAdmin.AutoSize = true;
            this.chkUserAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUserAdmin.Location = new System.Drawing.Point(12, 122);
            this.chkUserAdmin.Name = "chkUserAdmin";
            this.chkUserAdmin.Size = new System.Drawing.Size(162, 20);
            this.chkUserAdmin.TabIndex = 14;
            this.chkUserAdmin.Text = "User Administration";
            this.chkUserAdmin.UseVisualStyleBackColor = true;
            // 
            // chkMaintainImage
            // 
            this.chkMaintainImage.AutoSize = true;
            this.chkMaintainImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMaintainImage.Location = new System.Drawing.Point(12, 161);
            this.chkMaintainImage.Name = "chkMaintainImage";
            this.chkMaintainImage.Size = new System.Drawing.Size(311, 20);
            this.chkMaintainImage.TabIndex = 17;
            this.chkMaintainImage.Text = "Can maintain controlled image definitions";
            this.chkMaintainImage.UseVisualStyleBackColor = true;
            // 
            // chkPackCarton
            // 
            this.chkPackCarton.AutoSize = true;
            this.chkPackCarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPackCarton.Location = new System.Drawing.Point(12, 81);
            this.chkPackCarton.Name = "chkPackCarton";
            this.chkPackCarton.Size = new System.Drawing.Size(162, 20);
            this.chkPackCarton.TabIndex = 18;
            this.chkPackCarton.Text = "Pack Cartons / Bins";
            this.chkPackCarton.UseVisualStyleBackColor = true;
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 422);
            this.Controls.Add(this.chkPackCarton);
            this.Controls.Add(this.chkMaintainImage);
            this.Controls.Add(this.chkBlockImage);
            this.Controls.Add(this.chkSendImage);
            this.Controls.Add(this.chkUserAdmin);
            this.Controls.Add(this.btnSaveUser);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Name = "EditUserForm";
            this.Text = "Edit User";
            this.Load += new System.EventHandler(this.EditUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Button btnSaveUser;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.CheckBox chkBlockImage;
        private System.Windows.Forms.CheckBox chkSendImage;
        private System.Windows.Forms.CheckBox chkUserAdmin;
        private System.Windows.Forms.CheckBox chkMaintainImage;
        private System.Windows.Forms.CheckBox chkPackCarton;
    }
}