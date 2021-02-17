namespace StandAlonePackingApp
{
    partial class ChooseModeDialog
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
            this.buttonMatrix1.Name = "buttonMatrix1";
            this.buttonMatrix1.Size = new System.Drawing.Size(373, 401);
            this.buttonMatrix1.TabIndex = 0;
            // 
            // ChooseMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 426);
            this.Controls.Add(this.buttonMatrix1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChooseMode";
            this.Text = "Change Processing Mode";
            this.Load += new System.EventHandler(this.ChooseMode_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ShopFloorLib.ButtonMatrix buttonMatrix1;
    }
}