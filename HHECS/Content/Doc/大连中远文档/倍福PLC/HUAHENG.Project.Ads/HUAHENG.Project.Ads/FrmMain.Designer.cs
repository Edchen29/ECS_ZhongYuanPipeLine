namespace HUAHENG.Project.Ads
{
    partial class FrmMain
    {        
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 
     
        private void InitializeComponent()
        {
            this.Btn_CuttingPcStation = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_CuttingPcStation
            // 
            this.Btn_CuttingPcStation.Location = new System.Drawing.Point(22, 97);
            this.Btn_CuttingPcStation.Name = "Btn_CuttingPcStation";
            this.Btn_CuttingPcStation.Size = new System.Drawing.Size(195, 25);
            this.Btn_CuttingPcStation.TabIndex = 0;
            this.Btn_CuttingPcStation.Text = "Cutting PC station";
            this.Btn_CuttingPcStation.UseVisualStyleBackColor = true;
            this.Btn_CuttingPcStation.Visible = false;
            this.Btn_CuttingPcStation.Click += new System.EventHandler(this.Btn_CuttingPcStation_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 69);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cutting & Beveling PC station";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 25);
            this.button3.TabIndex = 0;
            this.button3.Text = "Endbeveling PC station";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(22, 159);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 25);
            this.button4.TabIndex = 0;
            this.button4.Text = "Fitup & Welding PC station";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 194);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Btn_CuttingPcStation);
            this.Name = "FrmMain";
            this.Text = "AdsTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_CuttingPcStation;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

