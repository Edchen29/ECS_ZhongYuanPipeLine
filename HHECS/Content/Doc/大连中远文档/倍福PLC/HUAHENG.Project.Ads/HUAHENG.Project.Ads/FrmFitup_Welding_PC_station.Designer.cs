namespace HUAHENG.Project.Ads
{
    partial class FrmFitup_Welding_PC_station
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tBox_MaterialType = new System.Windows.Forms.TextBox();
            this.tBox_OD = new System.Windows.Forms.TextBox();
            this.tBox_WT = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Send);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tBox_MaterialType);
            this.groupBox1.Controls.Add(this.tBox_OD);
            this.groupBox1.Controls.Add(this.tBox_WT);
            this.groupBox1.Location = new System.Drawing.Point(16, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 217);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Write to Control";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Location = new System.Drawing.Point(177, 181);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(75, 23);
            this.Btn_Send.TabIndex = 41;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "MaterialType:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "OD:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "WT:";
            // 
            // tBox_MaterialType
            // 
            this.tBox_MaterialType.Location = new System.Drawing.Point(110, 41);
            this.tBox_MaterialType.Name = "tBox_MaterialType";
            this.tBox_MaterialType.Size = new System.Drawing.Size(142, 21);
            this.tBox_MaterialType.TabIndex = 22;
            this.tBox_MaterialType.Text = "sss";
            // 
            // tBox_OD
            // 
            this.tBox_OD.Location = new System.Drawing.Point(110, 79);
            this.tBox_OD.Name = "tBox_OD";
            this.tBox_OD.Size = new System.Drawing.Size(142, 21);
            this.tBox_OD.TabIndex = 24;
            this.tBox_OD.Text = "3.124";
            // 
            // tBox_WT
            // 
            this.tBox_WT.Location = new System.Drawing.Point(110, 124);
            this.tBox_WT.Name = "tBox_WT";
            this.tBox_WT.Size = new System.Drawing.Size(142, 21);
            this.tBox_WT.TabIndex = 23;
            this.tBox_WT.Text = "2.155";
            // 
            // FrmFitup_Welding_PC_station
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 241);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmFitup_Welding_PC_station";
            this.Text = "FrmFitup_Welding_PC_station";
            this.Load += new System.EventHandler(this.FrmFitup_Welding_PC_station_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBox_MaterialType;
        private System.Windows.Forms.TextBox tBox_OD;
        private System.Windows.Forms.TextBox tBox_WT;
    }
}