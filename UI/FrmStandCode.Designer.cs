namespace Review.UI
{
    partial class FrmStandCode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelgrade = new System.Windows.Forms.Label();
            this.labelPName = new System.Windows.Forms.Label();
            this.txtgrade = new System.Windows.Forms.ComboBox();
            this.txtpname = new System.Windows.Forms.ComboBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.labelStand = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOpenSc = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(275, 141);
            this.panel1.TabIndex = 0;
            // 
            // labelgrade
            // 
            this.labelgrade.AutoSize = true;
            this.labelgrade.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelgrade.Location = new System.Drawing.Point(17, 76);
            this.labelgrade.Name = "labelgrade";
            this.labelgrade.Size = new System.Drawing.Size(58, 16);
            this.labelgrade.TabIndex = 5;
            this.labelgrade.Text = "档次号";
            // 
            // labelPName
            // 
            this.labelPName.AutoSize = true;
            this.labelPName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelPName.Location = new System.Drawing.Point(17, 14);
            this.labelPName.Name = "labelPName";
            this.labelPName.Size = new System.Drawing.Size(75, 16);
            this.labelPName.TabIndex = 4;
            this.labelPName.Text = "产品名称";
            // 
            // txtgrade
            // 
            this.txtgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtgrade.FormattingEnabled = true;
            this.txtgrade.Location = new System.Drawing.Point(13, 100);
            this.txtgrade.Name = "txtgrade";
            this.txtgrade.Size = new System.Drawing.Size(121, 20);
            this.txtgrade.TabIndex = 3;
            this.txtgrade.SelectedValueChanged += new System.EventHandler(this.txtpname_SelectedValueChanged);
            // 
            // txtpname
            // 
            this.txtpname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtpname.FormattingEnabled = true;
            this.txtpname.Location = new System.Drawing.Point(13, 42);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(121, 20);
            this.txtpname.TabIndex = 2;
            this.txtpname.SelectedValueChanged += new System.EventHandler(this.txtpname_SelectedValueChanged);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(75, 16);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "型号选择";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(430, 122);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 50);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(430, 31);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 50);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelStand
            // 
            this.labelStand.AutoSize = true;
            this.labelStand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelStand.Location = new System.Drawing.Point(12, 204);
            this.labelStand.Name = "labelStand";
            this.labelStand.Size = new System.Drawing.Size(92, 16);
            this.labelStand.TabIndex = 6;
            this.labelStand.Text = "标准批次码";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(11, 237);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(520, 177);
            this.panel3.TabIndex = 7;
            // 
            // btnOpenSc
            // 
            this.btnOpenSc.Location = new System.Drawing.Point(308, 122);
            this.btnOpenSc.Name = "btnOpenSc";
            this.btnOpenSc.Size = new System.Drawing.Size(101, 50);
            this.btnOpenSc.TabIndex = 8;
            this.btnOpenSc.Text = "OpenScan";
            this.btnOpenSc.UseVisualStyleBackColor = true;
            this.btnOpenSc.Click += new System.EventHandler(this.btnOpenSc_Click);
            // 
            // FrmStandCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 428);
            this.Controls.Add(this.btnOpenSc);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelStand);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "FrmStandCode";
            this.Text = "FrmStandCode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmStandCode_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox txtgrade;
        private System.Windows.Forms.ComboBox txtpname;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelgrade;
        private System.Windows.Forms.Label labelPName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labelStand;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnOpenSc;
    }
}