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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbRockets = new System.Windows.Forms.Label();
            this.lbTurns = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbMoney = new System.Windows.Forms.Label();
            this.lbBase = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.lbMoneyDiff = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbUpgradeCost = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rockets Launched:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Turns till end:";
            // 
            // lbRockets
            // 
            this.lbRockets.AutoSize = true;
            this.lbRockets.Location = new System.Drawing.Point(148, 9);
            this.lbRockets.Name = "lbRockets";
            this.lbRockets.Size = new System.Drawing.Size(14, 17);
            this.lbRockets.TabIndex = 3;
            this.lbRockets.Text = "x";
            // 
            // lbTurns
            // 
            this.lbTurns.AutoSize = true;
            this.lbTurns.Location = new System.Drawing.Point(148, 26);
            this.lbTurns.Name = "lbTurns";
            this.lbTurns.Size = new System.Drawing.Size(14, 17);
            this.lbTurns.TabIndex = 4;
            this.lbTurns.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Hajs:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Base Id:";
            // 
            // lbMoney
            // 
            this.lbMoney.AutoSize = true;
            this.lbMoney.Location = new System.Drawing.Point(312, 9);
            this.lbMoney.Name = "lbMoney";
            this.lbMoney.Size = new System.Drawing.Size(14, 17);
            this.lbMoney.TabIndex = 7;
            this.lbMoney.Text = "x";
            // 
            // lbBase
            // 
            this.lbBase.AutoSize = true;
            this.lbBase.Location = new System.Drawing.Point(312, 26);
            this.lbBase.Name = "lbBase";
            this.lbBase.Size = new System.Drawing.Size(14, 17);
            this.lbBase.TabIndex = 8;
            this.lbBase.Text = "x";
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(12, 46);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1120, 313);
            this.panel.TabIndex = 9;
            // 
            // lbMoneyDiff
            // 
            this.lbMoneyDiff.AutoSize = true;
            this.lbMoneyDiff.Location = new System.Drawing.Point(471, 9);
            this.lbMoneyDiff.Name = "lbMoneyDiff";
            this.lbMoneyDiff.Size = new System.Drawing.Size(24, 17);
            this.lbMoneyDiff.TabIndex = 10;
            this.lbMoneyDiff.Text = "0$";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(402, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hajs diff:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Upgrade cost:";
            // 
            // lbUpgradeCost
            // 
            this.lbUpgradeCost.AutoSize = true;
            this.lbUpgradeCost.Location = new System.Drawing.Point(471, 26);
            this.lbUpgradeCost.Name = "lbUpgradeCost";
            this.lbUpgradeCost.Size = new System.Drawing.Size(24, 17);
            this.lbUpgradeCost.TabIndex = 13;
            this.lbUpgradeCost.Text = "0$";
            // 
            // VisualizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1144, 555);
            this.Controls.Add(this.lbUpgradeCost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbMoneyDiff);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.lbBase);
            this.Controls.Add(this.lbMoney);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTurns);
            this.Controls.Add(this.lbRockets);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "VisualizationForm";
            this.Text = "VisualizationForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbRockets;
        private System.Windows.Forms.Label lbTurns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbMoney;
        private System.Windows.Forms.Label lbBase;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lbMoneyDiff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbUpgradeCost;
    }
}