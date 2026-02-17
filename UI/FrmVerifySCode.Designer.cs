namespace Review.UI
{
    partial class FrmVerifySCode
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelgrade = new System.Windows.Forms.Label();
            this.labelPName = new System.Windows.Forms.Label();
            this.txtgrade = new System.Windows.Forms.ComboBox();
            this.txtpname = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelStand = new System.Windows.Forms.Label();
            this.labelScan = new System.Windows.Forms.Label();
            this.btnOpenSc = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnSure = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(203, 16);
            this.labelInfo.TabIndex = 3;
            this.labelInfo.Text = "Выбор модели";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelgrade);
            this.panel1.Controls.Add(this.labelPName);
            this.panel1.Controls.Add(this.txtgrade);
            this.panel1.Controls.Add(this.txtpname);
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 158);
            this.panel1.TabIndex = 2;
            // 
            // labelgrade
            // 
            this.labelgrade.AutoSize = true;
            this.labelgrade.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelgrade.Location = new System.Drawing.Point(7, 79);
            this.labelgrade.Name = "labelgrade";
            this.labelgrade.Size = new System.Drawing.Size(135, 16);
            this.labelgrade.TabIndex = 5;
            this.labelgrade.Text = "№ группы";
            // 
            // labelPName
            // 
            this.labelPName.AutoSize = true;
            this.labelPName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelPName.Location = new System.Drawing.Point(7, 18);
            this.labelPName.Name = "labelPName";
            this.labelPName.Size = new System.Drawing.Size(229, 16);
            this.labelPName.TabIndex = 4;
            this.labelPName.Text = "Наим. продукта";
            // 
            // txtgrade
            // 
            this.txtgrade.Enabled = false;
            this.txtgrade.FormattingEnabled = true;
            this.txtgrade.Location = new System.Drawing.Point(10, 109);
            this.txtgrade.Name = "txtgrade";
            this.txtgrade.Size = new System.Drawing.Size(121, 20);
            this.txtgrade.TabIndex = 3;
            // 
            // txtpname
            // 
            this.txtpname.Enabled = false;
            this.txtpname.FormattingEnabled = true;
            this.txtpname.Location = new System.Drawing.Point(10, 47);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(121, 20);
            this.txtpname.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelStand);
            this.panel3.Controls.Add(this.labelScan);
            this.panel3.Location = new System.Drawing.Point(12, 215);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(788, 142);
            this.panel3.TabIndex = 8;
            // 
            // labelStand
            // 
            this.labelStand.AutoSize = true;
            this.labelStand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelStand.Location = new System.Drawing.Point(436, 14);
            this.labelStand.Name = "labelStand";
            this.labelStand.Size = new System.Drawing.Size(306, 16);
            this.labelStand.TabIndex = 5;
            this.labelStand.Text = "Станд. номер партии";
            // 
            // labelScan
            // 
            this.labelScan.AutoSize = true;
            this.labelScan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelScan.Location = new System.Drawing.Point(64, 14);
            this.labelScan.Name = "labelScan";
            this.labelScan.Size = new System.Drawing.Size(221, 16);
            this.labelScan.TabIndex = 4;
            this.labelScan.Text = "Скан. № партии";
            // 
            // btnOpenSc
            // 
            this.btnOpenSc.Location = new System.Drawing.Point(317, 141);
            this.btnOpenSc.Name = "btnOpenSc";
            this.btnOpenSc.Size = new System.Drawing.Size(101, 50);
            this.btnOpenSc.TabIndex = 12;
            this.btnOpenSc.Text = "Открыть сканер";
            this.btnOpenSc.UseVisualStyleBackColor = true;
            this.btnOpenSc.Click += new System.EventHandler(this.btnOpenSc_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(677, 141);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(101, 50);
            this.btnCheck.TabIndex = 10;
            this.btnCheck.Text = "Сверка";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(474, 31);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(101, 50);
            this.btnSure.TabIndex = 9;
            this.btnSure.Text = "Подтвердить";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Visible = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // FrmVerifySCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 369);
            this.Controls.Add(this.btnOpenSc);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmVerifySCode";
            this.Text = "FrmVerifySCode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStandCode_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelgrade;
        private System.Windows.Forms.Label labelPName;
        private System.Windows.Forms.ComboBox txtgrade;
        private System.Windows.Forms.ComboBox txtpname;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpenSc;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Label labelStand;
        private System.Windows.Forms.Label labelScan;
    }
}