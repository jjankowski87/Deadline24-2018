namespace Deadline24.ConsoleApp
{
    partial class VisualizationForm
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
            this.pbWorld = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWorld)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWorld
            // 
            this.pbWorld.Location = new System.Drawing.Point(12, 12);
            this.pbWorld.Name = "pbWorld";
            this.pbWorld.Size = new System.Drawing.Size(758, 531);
            this.pbWorld.TabIndex = 0;
            this.pbWorld.TabStop = false;
            // 
            // VisualizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 555);
            this.Controls.Add(this.pbWorld);
            this.Name = "VisualizationForm";
            this.Text = "VisualizationForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbWorld)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWorld;
    }
}