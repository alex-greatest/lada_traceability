namespace Review
{
    partial class FrmSetPrintCode
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.save = new System.Windows.Forms.Button();
            this.txtPType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.txtFrontCode = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.save, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtPType, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comProduct, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFrontCode, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 203);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // save
            // 
            this.save.Font = new System.Drawing.Font("宋体", 9F);
            this.save.Location = new System.Drawing.Point(465, 135);
            this.save.Name = "save";
            this.tableLayoutPanel1.SetRowSpan(this.save, 2);
            this.save.Size = new System.Drawing.Size(108, 60);
            this.save.TabIndex = 8;
            this.save.Text = "添加/修改";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPType
            // 
            this.txtPType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPType.Font = new System.Drawing.Font("宋体", 13F);
            this.txtPType.Location = new System.Drawing.Point(3, 102);
            this.txtPType.Name = "txtPType";
            this.txtPType.Size = new System.Drawing.Size(456, 27);
            this.txtPType.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(3, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(456, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "条码前缀";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(456, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "产品代号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(456, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品型号";
            // 
            // comProduct
            // 
            this.comProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProduct.Font = new System.Drawing.Font("宋体", 11F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(3, 36);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(121, 23);
            this.comProduct.TabIndex = 1;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comProduct_SelectedValueChanged);
            // 
            // txtFrontCode
            // 
            this.txtFrontCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrontCode.Font = new System.Drawing.Font("宋体", 13F);
            this.txtFrontCode.Location = new System.Drawing.Point(3, 168);
            this.txtFrontCode.Name = "txtFrontCode";
            this.txtFrontCode.Size = new System.Drawing.Size(456, 27);
            this.txtFrontCode.TabIndex = 3;
            // 
            // FrmSetPrintCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 229);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmSetPrintCode";
            this.Text = "FrmSetPrintCode";
            this.Activated += new System.EventHandler(this.FrmSetPrintCode_Activated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtPType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.TextBox txtFrontCode;
        private System.Windows.Forms.Button save;
    }
}