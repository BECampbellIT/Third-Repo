namespace ShopFloorLib
{
    partial class ButtonMatrix
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
            this.tabLayoutOutside = new System.Windows.Forms.TableLayoutPanel();
            this.btnPageUp = new System.Windows.Forms.Button();
            this.btnPageDown = new System.Windows.Forms.Button();
            this.tabLayoutInside = new System.Windows.Forms.TableLayoutPanel();
            this.tabLayoutOutside.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabLayoutOutside
            // 
            this.tabLayoutOutside.ColumnCount = 2;
            this.tabLayoutOutside.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayoutOutside.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayoutOutside.Controls.Add(this.btnPageUp, 0, 1);
            this.tabLayoutOutside.Controls.Add(this.btnPageDown, 1, 1);
            this.tabLayoutOutside.Controls.Add(this.tabLayoutInside, 0, 0);
            this.tabLayoutOutside.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLayoutOutside.Location = new System.Drawing.Point(0, 0);
            this.tabLayoutOutside.Name = "tabLayoutOutside";
            this.tabLayoutOutside.RowCount = 2;
            this.tabLayoutOutside.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabLayoutOutside.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tabLayoutOutside.Size = new System.Drawing.Size(239, 122);
            this.tabLayoutOutside.TabIndex = 0;
            // 
            // btnPageUp
            // 
            this.btnPageUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPageUp.Location = new System.Drawing.Point(3, 85);
            this.btnPageUp.Name = "btnPageUp";
            this.btnPageUp.Size = new System.Drawing.Size(113, 34);
            this.btnPageUp.TabIndex = 0;
            this.btnPageUp.Text = "Page Up";
            this.btnPageUp.UseVisualStyleBackColor = true;
            this.btnPageUp.Click += new System.EventHandler(this.btnPageUp_Click);
            // 
            // btnPageDown
            // 
            this.btnPageDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPageDown.Location = new System.Drawing.Point(122, 85);
            this.btnPageDown.Name = "btnPageDown";
            this.btnPageDown.Size = new System.Drawing.Size(114, 34);
            this.btnPageDown.TabIndex = 1;
            this.btnPageDown.Text = "Page Down";
            this.btnPageDown.UseVisualStyleBackColor = true;
            this.btnPageDown.Click += new System.EventHandler(this.btnPageDown_Click);
            // 
            // tabLayoutInside
            // 
            this.tabLayoutInside.ColumnCount = 1;
            this.tabLayoutOutside.SetColumnSpan(this.tabLayoutInside, 2);
            this.tabLayoutInside.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayoutInside.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLayoutInside.Location = new System.Drawing.Point(3, 3);
            this.tabLayoutInside.Name = "tabLayoutInside";
            this.tabLayoutInside.RowCount = 1;
            this.tabLayoutInside.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayoutInside.Size = new System.Drawing.Size(233, 76);
            this.tabLayoutInside.TabIndex = 2;
            // 
            // ButtonMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabLayoutOutside);
            this.Name = "ButtonMatrix";
            this.Size = new System.Drawing.Size(239, 122);
            this.tabLayoutOutside.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tabLayoutOutside;
        private System.Windows.Forms.Button btnPageUp;
        private System.Windows.Forms.Button btnPageDown;
        private System.Windows.Forms.TableLayoutPanel tabLayoutInside;
    }
}
