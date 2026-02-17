namespace Review.UI
{
    partial class FormGradeNumber
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lbGrade = new System.Windows.Forms.Label();
            this.tbGrade = new System.Windows.Forms.TextBox();
            this.lbGradeList = new System.Windows.Forms.Label();
            this.cbGradeList = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnsave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbmode = new System.Windows.Forms.ComboBox();
            this.tbcut = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbstart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbactualgd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbgrade2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbproname = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 281);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDel);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Controls.Add(this.lbGrade);
            this.tabPage1.Controls.Add(this.tbGrade);
            this.tabPage1.Controls.Add(this.lbGradeList);
            this.tabPage1.Controls.Add(this.cbGradeList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 255);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Leave += new System.EventHandler(this.tabPage1_Leave);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(22, 130);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "btnDel";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(316, 130);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lbGrade
            // 
            this.lbGrade.AutoSize = true;
            this.lbGrade.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lbGrade.Location = new System.Drawing.Point(318, 64);
            this.lbGrade.Name = "lbGrade";
            this.lbGrade.Size = new System.Drawing.Size(52, 14);
            this.lbGrade.TabIndex = 3;
            this.lbGrade.Text = "档次号";
            // 
            // tbGrade
            // 
            this.tbGrade.Location = new System.Drawing.Point(316, 93);
            this.tbGrade.Name = "tbGrade";
            this.tbGrade.Size = new System.Drawing.Size(100, 21);
            this.tbGrade.TabIndex = 2;
            // 
            // lbGradeList
            // 
            this.lbGradeList.AutoSize = true;
            this.lbGradeList.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lbGradeList.Location = new System.Drawing.Point(21, 64);
            this.lbGradeList.Name = "lbGradeList";
            this.lbGradeList.Size = new System.Drawing.Size(82, 14);
            this.lbGradeList.TabIndex = 1;
            this.lbGradeList.Text = "档次号列表";
            // 
            // cbGradeList
            // 
            this.cbGradeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGradeList.FormattingEnabled = true;
            this.cbGradeList.Location = new System.Drawing.Point(22, 94);
            this.cbGradeList.Name = "cbGradeList";
            this.cbGradeList.Size = new System.Drawing.Size(121, 20);
            this.cbGradeList.TabIndex = 0;
            this.cbGradeList.Enter += new System.EventHandler(this.cbGradeList_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnsave);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cbmode);
            this.tabPage2.Controls.Add(this.tbcut);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbstart);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbactualgd);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cbgrade2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbproname);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(574, 255);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(493, 209);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(78, 35);
            this.btnsave.TabIndex = 15;
            this.btnsave.Text = "btnsave";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(558, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 96);
            this.panel1.TabIndex = 14;
            this.panel1.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(3, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "类型 2 档次号与产品码为分开两个码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(3, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 14);
            this.label7.TabIndex = 14;
            this.label7.Text = "类型 1 档次号与产品码为一个总码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(306, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "条码类型";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cbmode
            // 
            this.cbmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmode.FormattingEnabled = true;
            this.cbmode.Location = new System.Drawing.Point(307, 132);
            this.cbmode.Name = "cbmode";
            this.cbmode.Size = new System.Drawing.Size(121, 20);
            this.cbmode.TabIndex = 12;
            // 
            // tbcut
            // 
            this.tbcut.Location = new System.Drawing.Point(305, 217);
            this.tbcut.Name = "tbcut";
            this.tbcut.Size = new System.Drawing.Size(121, 21);
            this.tbcut.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(304, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "条码分割符";
            // 
            // tbstart
            // 
            this.tbstart.Location = new System.Drawing.Point(19, 217);
            this.tbstart.Name = "tbstart";
            this.tbstart.Size = new System.Drawing.Size(119, 21);
            this.tbstart.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(18, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "档次号起始位置";
            // 
            // tbactualgd
            // 
            this.tbactualgd.Location = new System.Drawing.Point(17, 131);
            this.tbactualgd.Name = "tbactualgd";
            this.tbactualgd.Size = new System.Drawing.Size(121, 21);
            this.tbactualgd.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(16, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "实际档次号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(304, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "档次代号列表";
            // 
            // cbgrade2
            // 
            this.cbgrade2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbgrade2.FormattingEnabled = true;
            this.cbgrade2.Location = new System.Drawing.Point(305, 50);
            this.cbgrade2.Name = "cbgrade2";
            this.cbgrade2.Size = new System.Drawing.Size(121, 20);
            this.cbgrade2.TabIndex = 4;
            this.cbgrade2.SelectedValueChanged += new System.EventHandler(this.cbproname_SelectedValueChanged);
            this.cbgrade2.Enter += new System.EventHandler(this.cbGradeList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "产品名称列表";
            // 
            // cbproname
            // 
            this.cbproname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbproname.FormattingEnabled = true;
            this.cbproname.Location = new System.Drawing.Point(17, 50);
            this.cbproname.Name = "cbproname";
            this.cbproname.Size = new System.Drawing.Size(121, 20);
            this.cbproname.TabIndex = 2;
            this.cbproname.SelectedValueChanged += new System.EventHandler(this.cbproname_SelectedValueChanged);
            // 
            // FormGradeNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 295);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormGradeNumber";
            this.Text = "FormFradeNumber";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGradeNumber_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbGradeList;
        private System.Windows.Forms.Label lbGradeList;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lbGrade;
        private System.Windows.Forms.TextBox tbGrade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbgrade2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbproname;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbmode;
        private System.Windows.Forms.TextBox tbcut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbstart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbactualgd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnsave;
    }
}