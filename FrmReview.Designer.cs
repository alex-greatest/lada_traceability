
namespace Review
{
    partial class FrmReview
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
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comStation = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.boxCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productCode = new System.Windows.Forms.TextBox();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.comResult = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dTP1 = new System.Windows.Forms.DateTimePicker();
            this.dTP2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.group1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 42;
            this.label1.Text = "Наим. прод:";
            // 
            // comProduct
            // 
            this.comProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProduct.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(84, 27);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(129, 22);
            this.comProduct.TabIndex = 41;
            this.comProduct.SelectedIndexChanged += new System.EventHandler(this.comProduct_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label2.Location = new System.Drawing.Point(790, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 44;
            this.label2.Text = "Штрих-код ";
            // 
            // comStation
            // 
            this.comStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStation.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comStation.FormattingEnabled = true;
            this.comStation.Location = new System.Drawing.Point(298, 28);
            this.comStation.Name = "comStation";
            this.comStation.Size = new System.Drawing.Size(129, 22);
            this.comStation.TabIndex = 43;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.btnSearch.Location = new System.Drawing.Point(1041, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(156, 37);
            this.btnSearch.TabIndex = 49;
            this.btnSearch.Text = "Поиск штрих-кода";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.Search_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.boxCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.productCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comStation);
            this.groupBox1.Controls.Add(this.comProduct);
            this.groupBox1.Controls.Add(this.btnToExcel);
            this.groupBox1.Controls.Add(this.comResult);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1325, 71);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // boxCode
            // 
            this.boxCode.Location = new System.Drawing.Point(641, 23);
            this.boxCode.Margin = new System.Windows.Forms.Padding(4);
            this.boxCode.Name = "boxCode";
            this.boxCode.Size = new System.Drawing.Size(132, 26);
            this.boxCode.TabIndex = 55;
            this.boxCode.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(582, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 54;
            this.label3.Text = "箱码：";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label5.Location = new System.Drawing.Point(224, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 53;
            this.label5.Text = "Операция:";
            // 
            // productCode
            // 
            this.productCode.Location = new System.Drawing.Point(866, 24);
            this.productCode.Margin = new System.Windows.Forms.Padding(4);
            this.productCode.Name = "productCode";
            this.productCode.Size = new System.Drawing.Size(132, 26);
            this.productCode.TabIndex = 52;
            // 
            // btnToExcel
            // 
            this.btnToExcel.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.btnToExcel.Location = new System.Drawing.Point(1203, 21);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(117, 37);
            this.btnToExcel.TabIndex = 51;
            this.btnToExcel.Text = "Вывести";
            this.btnToExcel.UseVisualStyleBackColor = true;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // comResult
            // 
            this.comResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comResult.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comResult.FormattingEnabled = true;
            this.comResult.Location = new System.Drawing.Point(474, 28);
            this.comResult.Name = "comResult";
            this.comResult.Size = new System.Drawing.Size(81, 22);
            this.comResult.TabIndex = 50;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(15, 168);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(1325, 452);
            this.dataGridView.TabIndex = 51;
            // 
            // dTP1
            // 
            this.dTP1.CalendarFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTP1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP1.Location = new System.Drawing.Point(179, 28);
            this.dTP1.Margin = new System.Windows.Forms.Padding(4);
            this.dTP1.Name = "dTP1";
            this.dTP1.Size = new System.Drawing.Size(165, 29);
            this.dTP1.TabIndex = 56;
            this.dTP1.ValueChanged += new System.EventHandler(this.dTP1_ValueChanged);
            // 
            // dTP2
            // 
            this.dTP2.CalendarFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dTP2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP2.Location = new System.Drawing.Point(400, 28);
            this.dTP2.Margin = new System.Windows.Forms.Padding(4);
            this.dTP2.Name = "dTP2";
            this.dTP2.Size = new System.Drawing.Size(155, 29);
            this.dTP2.TabIndex = 57;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1041, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(191, 37);
            this.button1.TabIndex = 58;
            this.button1.Text = "Поиск диап. времени";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Search_Click);
            // 
            // group1
            // 
            this.group1.Controls.Add(this.checkBox1);
            this.group1.Controls.Add(this.label6);
            this.group1.Controls.Add(this.label4);
            this.group1.Controls.Add(this.dTP2);
            this.group1.Controls.Add(this.button1);
            this.group1.Controls.Add(this.dTP1);
            this.group1.Font = new System.Drawing.Font("微软雅黑 Light", 12F);
            this.group1.Location = new System.Drawing.Point(15, 85);
            this.group1.Margin = new System.Windows.Forms.Padding(4);
            this.group1.Name = "group1";
            this.group1.Padding = new System.Windows.Forms.Padding(4);
            this.group1.Size = new System.Drawing.Size(1325, 76);
            this.group1.TabIndex = 59;
            this.group1.TabStop = false;
            this.group1.Text = "Поиск по времени";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(834, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(201, 25);
            this.checkBox1.TabIndex = 60;
            this.checkBox1.Text = "Включить трассировку";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(358, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 15);
            this.label6.TabIndex = 59;
            this.label6.Text = "—";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑 Light", 11F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Диапазон времени";
            // 
            // FrmReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 627);
            this.Controls.Add(this.group1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmReview";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据追溯";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmReview_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comStation;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comResult;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnToExcel;
        private System.Windows.Forms.TextBox productCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox boxCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dTP1;
        private System.Windows.Forms.DateTimePicker dTP2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox group1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}