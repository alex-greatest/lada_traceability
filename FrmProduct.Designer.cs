
namespace Review
{
    partial class FrmProduct
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comOrder = new System.Windows.Forms.ComboBox();
            this.comStation = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridProduct = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(691, 16);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(78, 25);
            this.btnEdit.TabIndex = 32;
            this.btnEdit.Text = "Изменить";
            this.btnEdit.UseMnemonic = false;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comOrder);
            this.groupBox1.Controls.Add(this.comStation);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtProduct);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(927, 48);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.checkBox.Location = new System.Drawing.Point(848, 19);
            this.checkBox.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(75, 21);
            this.checkBox.TabIndex = 39;
            this.checkBox.Text = "Продукт";
            this.checkBox.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(195, 19);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(62, 21);
            this.txtID.TabIndex = 38;
            this.txtID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtID_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(161, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "编号:";
            // 
            // comOrder
            // 
            this.comOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comOrder.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comOrder.FormattingEnabled = true;
            this.comOrder.Location = new System.Drawing.Point(372, 17);
            this.comOrder.Margin = new System.Windows.Forms.Padding(2);
            this.comOrder.Name = "comOrder";
            this.comOrder.Size = new System.Drawing.Size(54, 22);
            this.comOrder.TabIndex = 36;
            this.comOrder.SelectedIndexChanged += new System.EventHandler(this.comOrder_SelectedIndexChanged);
            // 
            // comStation
            // 
            this.comStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStation.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comStation.FormattingEnabled = true;
            this.comStation.Location = new System.Drawing.Point(504, 17);
            this.comStation.Margin = new System.Windows.Forms.Padding(2);
            this.comStation.Name = "comStation";
            this.comStation.Size = new System.Drawing.Size(90, 22);
            this.comStation.TabIndex = 35;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(773, 16);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(69, 25);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseMnemonic = false;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(607, 16);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 25);
            this.btnAdd.TabIndex = 31;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseMnemonic = false;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(463, 20);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "工位:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑 Light", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(332, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "工序:";
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(91, 19);
            this.txtProduct.Margin = new System.Windows.Forms.Padding(2);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(62, 21);
            this.txtProduct.TabIndex = 26;
            this.txtProduct.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑 Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(37, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "产品名.:";
            // 
            // dataGridProduct
            // 
            this.dataGridProduct.AllowUserToAddRows = false;
            this.dataGridProduct.AllowUserToDeleteRows = false;
            this.dataGridProduct.AllowUserToResizeColumns = false;
            this.dataGridProduct.AllowUserToResizeRows = false;
            this.dataGridProduct.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ProductName,
            this.编号,
            this.Station1,
            this.Station2,
            this.Station3,
            this.Station4,
            this.Station5,
            this.Station6,
            this.Station7,
            this.Station8,
            this.Station9,
            this.Station10,
            this.Station11,
            this.Station12,
            this.Station13,
            this.Station14});
            this.dataGridProduct.Location = new System.Drawing.Point(9, 73);
            this.dataGridProduct.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridProduct.Name = "dataGridProduct";
            this.dataGridProduct.ReadOnly = true;
            this.dataGridProduct.RowHeadersWidth = 51;
            this.dataGridProduct.RowTemplate.Height = 27;
            this.dataGridProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridProduct.Size = new System.Drawing.Size(923, 280);
            this.dataGridProduct.TabIndex = 29;
            this.dataGridProduct.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridStation_CellClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "Id";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "productName";
            this.ProductName.HeaderText = "产品名";
            this.ProductName.MinimumWidth = 6;
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 110;
            // 
            // 编号
            // 
            this.编号.DataPropertyName = "productID";
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.ReadOnly = true;
            // 
            // Station1
            // 
            this.Station1.DataPropertyName = "station1";
            this.Station1.HeaderText = "工序1";
            this.Station1.MinimumWidth = 6;
            this.Station1.Name = "Station1";
            this.Station1.ReadOnly = true;
            this.Station1.Width = 80;
            // 
            // Station2
            // 
            this.Station2.DataPropertyName = "station2";
            this.Station2.HeaderText = "工序2";
            this.Station2.MinimumWidth = 6;
            this.Station2.Name = "Station2";
            this.Station2.ReadOnly = true;
            this.Station2.Width = 80;
            // 
            // Station3
            // 
            this.Station3.DataPropertyName = "station3";
            this.Station3.HeaderText = "工序3";
            this.Station3.MinimumWidth = 6;
            this.Station3.Name = "Station3";
            this.Station3.ReadOnly = true;
            this.Station3.Width = 80;
            // 
            // Station4
            // 
            this.Station4.DataPropertyName = "station4";
            this.Station4.HeaderText = "工序4";
            this.Station4.MinimumWidth = 6;
            this.Station4.Name = "Station4";
            this.Station4.ReadOnly = true;
            this.Station4.Width = 80;
            // 
            // Station5
            // 
            this.Station5.DataPropertyName = "station5";
            this.Station5.HeaderText = "工序5";
            this.Station5.MinimumWidth = 6;
            this.Station5.Name = "Station5";
            this.Station5.ReadOnly = true;
            this.Station5.Width = 80;
            // 
            // Station6
            // 
            this.Station6.DataPropertyName = "station6";
            this.Station6.HeaderText = "工序6";
            this.Station6.MinimumWidth = 6;
            this.Station6.Name = "Station6";
            this.Station6.ReadOnly = true;
            this.Station6.Width = 80;
            // 
            // Station7
            // 
            this.Station7.DataPropertyName = "station7";
            this.Station7.HeaderText = "工序7";
            this.Station7.MinimumWidth = 6;
            this.Station7.Name = "Station7";
            this.Station7.ReadOnly = true;
            this.Station7.Width = 80;
            // 
            // Station8
            // 
            this.Station8.DataPropertyName = "station8";
            this.Station8.HeaderText = "工序8";
            this.Station8.MinimumWidth = 6;
            this.Station8.Name = "Station8";
            this.Station8.ReadOnly = true;
            this.Station8.Width = 80;
            // 
            // Station9
            // 
            this.Station9.DataPropertyName = "station9";
            this.Station9.HeaderText = "工序9";
            this.Station9.MinimumWidth = 6;
            this.Station9.Name = "Station9";
            this.Station9.ReadOnly = true;
            this.Station9.Width = 80;
            // 
            // Station10
            // 
            this.Station10.DataPropertyName = "station10";
            this.Station10.HeaderText = "工序10";
            this.Station10.MinimumWidth = 6;
            this.Station10.Name = "Station10";
            this.Station10.ReadOnly = true;
            this.Station10.Width = 80;
            // 
            // Station11
            // 
            this.Station11.DataPropertyName = "station11";
            this.Station11.HeaderText = "工序11";
            this.Station11.MinimumWidth = 6;
            this.Station11.Name = "Station11";
            this.Station11.ReadOnly = true;
            this.Station11.Width = 80;
            // 
            // Station12
            // 
            this.Station12.DataPropertyName = "station12";
            this.Station12.HeaderText = "工序12";
            this.Station12.MinimumWidth = 6;
            this.Station12.Name = "Station12";
            this.Station12.ReadOnly = true;
            this.Station12.Width = 80;
            // 
            // Station13
            // 
            this.Station13.DataPropertyName = "station13";
            this.Station13.HeaderText = "工序13";
            this.Station13.MinimumWidth = 6;
            this.Station13.Name = "Station13";
            this.Station13.ReadOnly = true;
            this.Station13.Width = 80;
            // 
            // Station14
            // 
            this.Station14.DataPropertyName = "station14";
            this.Station14.HeaderText = "工序14";
            this.Station14.MinimumWidth = 6;
            this.Station14.Name = "Station14";
            this.Station14.ReadOnly = true;
            this.Station14.Width = 80;
            // 
            // FrmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 363);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmProduct_FormClosed);
            this.Load += new System.EventHandler(this.FrmProduct_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridProduct;
        private System.Windows.Forms.ComboBox comOrder;
        private System.Windows.Forms.ComboBox comStation;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station14;
    }
}