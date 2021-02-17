namespace StandAlonePackingApp
{
    partial class ChooseReworkDateDialog
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
            this.buttonMatrix1 = new ShopFloorLib.ButtonMatrix();
            this.SuspendLayout();
            // 
            // buttonMatrix1
            // 
            this.buttonMatrix1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMatrix1.Location = new System.Drawing.Point(13, 13);
            this.buttonMatrix1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonMatrix1.Name = "buttonMatrix1";
            this.buttonMatrix1.Size = new System.Drawing.Size(703, 401);
            this.buttonMatrix1.TabIndex = 0;
            // 
            // ChooseDateDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 426);
            this.Controls.Add(this.buttonMatrix1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChooseDateDialog";
            this.Text = "Choose Packed on Date for Re-work";
            this.Load += new System.EventHandler(this.ChooseMode_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ShopFloorLib.ButtonMatrix buttonMatrix1;
    }
}