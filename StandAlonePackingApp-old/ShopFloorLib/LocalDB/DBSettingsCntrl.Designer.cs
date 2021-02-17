namespace ShopFloorLib
{
    partial class DBSettingsCntrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupDB = new System.Windows.Forms.GroupBox();
            this.txtDBPassword = new System.Windows.Forms.TextBox();
            this.lblDBPassword = new System.Windows.Forms.Label();
            this.txtDBUser = new System.Windows.Forms.TextBox();
            this.lblDBUser = new System.Windows.Forms.Label();
            this.txtDBHost = new System.Windows.Forms.TextBox();
            this.lblDBHost = new System.Windows.Forms.Label();
            this.groupDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupDB
            // 
            this.groupDB.BackColor = System.Drawing.Color.Snow;
            this.groupDB.Controls.Add(this.txtDBPassword);
            this.groupDB.Controls.Add(this.lblDBPassword);
            this.groupDB.Controls.Add(this.txtDBUser);
            this.groupDB.Controls.Add(this.lblDBUser);
            this.groupDB.Controls.Add(this.txtDBHost);
            this.groupDB.Controls.Add(this.lblDBHost);
            this.groupDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDB.Location = new System.Drawing.Point(3, 3);
            this.groupDB.Name = "groupDB";
            this.groupDB.Size = new System.Drawing.Size(479, 148);
            this.groupDB.TabIndex = 12;
            this.groupDB.TabStop = false;
            this.groupDB.Text = "Database Settings";
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBPassword.Location = new System.Drawing.Point(136, 89);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.Size = new System.Drawing.Size(132, 22);
            this.txtDBPassword.TabIndex = 8;
            this.txtDBPassword.UseSystemPasswordChar = true;
            // 
            // lblDBPassword
            // 
            this.lblDBPassword.AutoSize = true;
            this.lblDBPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBPassword.Location = new System.Drawing.Point(6, 89);
            this.lblDBPassword.Name = "lblDBPassword";
            this.lblDBPassword.Size = new System.Drawing.Size(68, 16);
            this.lblDBPassword.TabIndex = 7;
            this.lblDBPassword.Text = "Password";
            // 
            // txtDBUser
            // 
            this.txtDBUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBUser.Location = new System.Drawing.Point(136, 55);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Size = new System.Drawing.Size(112, 22);
            this.txtDBUser.TabIndex = 6;
            // 
            // lblDBUser
            // 
            this.lblDBUser.AutoSize = true;
            this.lblDBUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBUser.Location = new System.Drawing.Point(6, 58);
            this.lblDBUser.Name = "lblDBUser";
            this.lblDBUser.Size = new System.Drawing.Size(37, 16);
            this.lblDBUser.TabIndex = 5;
            this.lblDBUser.Text = "User";
            // 
            // txtDBHost
            // 
            this.txtDBHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBHost.Location = new System.Drawing.Point(136, 24);
            this.txtDBHost.Name = "txtDBHost";
            this.txtDBHost.Size = new System.Drawing.Size(227, 22);
            this.txtDBHost.TabIndex = 2;
            // 
            // lblDBHost
            // 
            this.lblDBHost.AutoSize = true;
            this.lblDBHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBHost.Location = new System.Drawing.Point(6, 30);
            this.lblDBHost.Name = "lblDBHost";
            this.lblDBHost.Size = new System.Drawing.Size(124, 16);
            this.lblDBHost.TabIndex = 1;
            this.lblDBHost.Text = "Hostname / IP Addr";
            // 
            // DBSettingsCntrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupDB);
            this.Name = "DBSettingsCntrl";
            this.Size = new System.Drawing.Size(482, 152);
            this.groupDB.ResumeLayout(false);
            this.groupDB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupDB;
        private System.Windows.Forms.TextBox txtDBPassword;
        private System.Windows.Forms.Label lblDBPassword;
        private System.Windows.Forms.TextBox txtDBUser;
        private System.Windows.Forms.Label lblDBUser;
        private System.Windows.Forms.TextBox txtDBHost;
        private System.Windows.Forms.Label lblDBHost;
    }
}
