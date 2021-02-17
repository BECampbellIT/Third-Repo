namespace StandAlonePackingApp
{
    partial class PrinterImagesDialog
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.btnEditImage = new System.Windows.Forms.Button();
            this.btnSendImage = new System.Windows.Forms.Button();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.btnLockImage = new System.Windows.Forms.Button();
            this.btnUnlockImage = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnShowLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(4, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(832, 187);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // btnAddImage
            // 
            this.btnAddImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddImage.Location = new System.Drawing.Point(12, 216);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(121, 46);
            this.btnAddImage.TabIndex = 6;
            this.btnAddImage.Text = "Add Image";
            this.btnAddImage.UseVisualStyleBackColor = false;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDeleteImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteImage.Location = new System.Drawing.Point(12, 340);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Size = new System.Drawing.Size(121, 46);
            this.btnDeleteImage.TabIndex = 7;
            this.btnDeleteImage.Text = "Delete Image";
            this.btnDeleteImage.UseVisualStyleBackColor = false;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // btnEditImage
            // 
            this.btnEditImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEditImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditImage.Location = new System.Drawing.Point(12, 278);
            this.btnEditImage.Name = "btnEditImage";
            this.btnEditImage.Size = new System.Drawing.Size(121, 46);
            this.btnEditImage.TabIndex = 8;
            this.btnEditImage.Text = "Edit Image";
            this.btnEditImage.UseVisualStyleBackColor = false;
            this.btnEditImage.Click += new System.EventHandler(this.btnEditImage_Click);
            // 
            // btnSendImage
            // 
            this.btnSendImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSendImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendImage.Location = new System.Drawing.Point(198, 216);
            this.btnSendImage.Name = "btnSendImage";
            this.btnSendImage.Size = new System.Drawing.Size(213, 46);
            this.btnSendImage.TabIndex = 9;
            this.btnSendImage.Text = "Send Image to Printer";
            this.btnSendImage.UseVisualStyleBackColor = false;
            this.btnSendImage.Click += new System.EventHandler(this.btnSendImage_Click);
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRemoveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImage.Location = new System.Drawing.Point(198, 278);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(213, 46);
            this.btnRemoveImage.TabIndex = 10;
            this.btnRemoveImage.Text = "Remove Image from Printer";
            this.btnRemoveImage.UseVisualStyleBackColor = false;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // btnLockImage
            // 
            this.btnLockImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLockImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockImage.Location = new System.Drawing.Point(463, 216);
            this.btnLockImage.Name = "btnLockImage";
            this.btnLockImage.Size = new System.Drawing.Size(213, 46);
            this.btnLockImage.TabIndex = 11;
            this.btnLockImage.Text = "Lock Image Use";
            this.btnLockImage.UseVisualStyleBackColor = false;
            this.btnLockImage.Click += new System.EventHandler(this.btnLockImage_Click);
            // 
            // btnUnlockImage
            // 
            this.btnUnlockImage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUnlockImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlockImage.Location = new System.Drawing.Point(463, 278);
            this.btnUnlockImage.Name = "btnUnlockImage";
            this.btnUnlockImage.Size = new System.Drawing.Size(213, 46);
            this.btnUnlockImage.TabIndex = 12;
            this.btnUnlockImage.Text = "Unlock Image for Use";
            this.btnUnlockImage.UseVisualStyleBackColor = false;
            this.btnUnlockImage.Click += new System.EventHandler(this.btnUnlockImage_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Crimson;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(740, 340);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(96, 46);
            this.btnBack.TabIndex = 13;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnShowLog
            // 
            this.btnShowLog.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnShowLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowLog.Location = new System.Drawing.Point(715, 216);
            this.btnShowLog.Name = "btnShowLog";
            this.btnShowLog.Size = new System.Drawing.Size(121, 46);
            this.btnShowLog.TabIndex = 14;
            this.btnShowLog.Text = "Show Log";
            this.btnShowLog.UseVisualStyleBackColor = false;
            this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
            // 
            // PrinterImagesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 410);
            this.Controls.Add(this.btnShowLog);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnUnlockImage);
            this.Controls.Add(this.btnLockImage);
            this.Controls.Add(this.btnRemoveImage);
            this.Controls.Add(this.btnSendImage);
            this.Controls.Add(this.btnEditImage);
            this.Controls.Add(this.btnDeleteImage);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.listView1);
            this.Name = "PrinterImagesDialog";
            this.Text = "Maintain Restricted Images on Printer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.Button btnEditImage;
        private System.Windows.Forms.Button btnSendImage;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.Button btnLockImage;
        private System.Windows.Forms.Button btnUnlockImage;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnShowLog;
    }
}