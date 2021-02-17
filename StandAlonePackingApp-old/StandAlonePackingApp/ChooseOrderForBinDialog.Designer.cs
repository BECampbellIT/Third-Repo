namespace StandAlonePackingApp
{
    partial class ChooseOrderForBinDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseOrderForBinDialog));
            this.buttonMatrix1 = new ShopFloorLib.ButtonMatrix();
            this.SuspendLayout();
            // 
            // buttonMatrix1
            // 
            this.buttonMatrix1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMatrix1.Location = new System.Drawing.Point(20, 16);
            this.buttonMatrix1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMatrix1.Name = "buttonMatrix1";
            this.buttonMatrix1.Size = new System.Drawing.Size(457, 544);
            this.buttonMatrix1.TabIndex = 0;
            // 
            // BinDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 564);
            this.Controls.Add(this.buttonMatrix1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BinDetailsDialog";
            this.Text = "Choose Customer Order for Bin Receipt";
            this.Load += new System.EventHandler(this.BinDetailsDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ShopFloorLib.ButtonMatrix buttonMatrix1;
    }
}