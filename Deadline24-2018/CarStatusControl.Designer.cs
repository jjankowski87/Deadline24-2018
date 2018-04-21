namespace Deadline24.ConsoleApp
{
    partial class CarStatusControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbCarId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbFuel = new System.Windows.Forms.Label();
            this.lbDiff = new System.Windows.Forms.Label();
            this.lbRoute = new System.Windows.Forms.Label();
            this.lbUpgrade = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "CarId";
            // 
            // lbCarId
            // 
            this.lbCarId.AutoSize = true;
            this.lbCarId.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbCarId.Location = new System.Drawing.Point(41, 0);
            this.lbCarId.Name = "lbCarId";
            this.lbCarId.Size = new System.Drawing.Size(40, 17);
            this.lbCarId.TabIndex = 1;
            this.lbCarId.Text = "0000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fuel";
            // 
            // lbFuel
            // 
            this.lbFuel.AutoSize = true;
            this.lbFuel.ForeColor = System.Drawing.Color.Red;
            this.lbFuel.Location = new System.Drawing.Point(41, 15);
            this.lbFuel.Name = "lbFuel";
            this.lbFuel.Size = new System.Drawing.Size(32, 17);
            this.lbFuel.TabIndex = 3;
            this.lbFuel.Text = "000";
            this.lbFuel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDiff
            // 
            this.lbDiff.AutoSize = true;
            this.lbDiff.ForeColor = System.Drawing.Color.Black;
            this.lbDiff.Location = new System.Drawing.Point(68, 15);
            this.lbDiff.Name = "lbDiff";
            this.lbDiff.Size = new System.Drawing.Size(32, 17);
            this.lbDiff.TabIndex = 4;
            this.lbDiff.Text = "000";
            this.lbDiff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRoute
            // 
            this.lbRoute.AutoSize = true;
            this.lbRoute.Location = new System.Drawing.Point(106, 9);
            this.lbRoute.Name = "lbRoute";
            this.lbRoute.Size = new System.Drawing.Size(13, 17);
            this.lbRoute.TabIndex = 5;
            this.lbRoute.Text = "-";
            // 
            // lbUpgrade
            // 
            this.lbUpgrade.AutoSize = true;
            this.lbUpgrade.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lbUpgrade.Location = new System.Drawing.Point(80, 0);
            this.lbUpgrade.Name = "lbUpgrade";
            this.lbUpgrade.Size = new System.Drawing.Size(12, 17);
            this.lbUpgrade.TabIndex = 6;
            this.lbUpgrade.Text = " ";
            // 
            // CarStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbUpgrade);
            this.Controls.Add(this.lbRoute);
            this.Controls.Add(this.lbDiff);
            this.Controls.Add(this.lbFuel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCarId);
            this.Controls.Add(this.label1);
            this.Name = "CarStatusControl";
            this.Size = new System.Drawing.Size(1623, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCarId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbFuel;
        private System.Windows.Forms.Label lbDiff;
        private System.Windows.Forms.Label lbRoute;
        private System.Windows.Forms.Label lbUpgrade;
    }
}
