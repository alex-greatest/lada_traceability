namespace Review
{
    partial class moreStation
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
            this.productComboBox = new System.Windows.Forms.ComboBox();
            this.productLabel = new System.Windows.Forms.Label();
            this.mainLabel = new System.Windows.Forms.Label();
            this.mainComboBox = new System.Windows.Forms.ComboBox();
            this.otherComboBox = new System.Windows.Forms.ComboBox();
            this.otherLabel = new System.Windows.Forms.Label();
            this.moreDataGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherStation01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherStation02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherStation03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherStation04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherStation05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.moreDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // productComboBox
            // 
            this.productComboBox.FormattingEnabled = true;
            this.productComboBox.Location = new System.Drawing.Point(14, 51);
            this.productComboBox.Name = "productComboBox";
            this.productComboBox.Size = new System.Drawing.Size(82, 20);
            this.productComboBox.TabIndex = 0;
            this.productComboBox.SelectedIndexChanged += new System.EventHandler(this.productComboBox_SelectedIndexChanged);
            // 
            // productLabel
            // 
            this.productLabel.AutoSize = true;
            this.productLabel.Location = new System.Drawing.Point(12, 33);
            this.productLabel.Name = "productLabel";
            this.productLabel.Size = new System.Drawing.Size(41, 12);
            this.productLabel.TabIndex = 1;
            this.productLabel.Text = "产品名";
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(109, 33);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(41, 12);
            this.mainLabel.TabIndex = 2;
            this.mainLabel.Text = "主工序";
            // 
            // mainComboBox
            // 
            this.mainComboBox.FormattingEnabled = true;
            this.mainComboBox.Location = new System.Drawing.Point(111, 51);
            this.mainComboBox.Name = "mainComboBox";
            this.mainComboBox.Size = new System.Drawing.Size(89, 20);
            this.mainComboBox.TabIndex = 3;
            // 
            // otherComboBox
            // 
            this.otherComboBox.FormattingEnabled = true;
            this.otherComboBox.Location = new System.Drawing.Point(225, 51);
            this.otherComboBox.Name = "otherComboBox";
            this.otherComboBox.Size = new System.Drawing.Size(83, 20);
            this.otherComboBox.TabIndex = 4;
            // 
            // otherLabel
            // 
            this.otherLabel.AutoSize = true;
            this.otherLabel.Location = new System.Drawing.Point(223, 33);
            this.otherLabel.Name = "otherLabel";
            this.otherLabel.Size = new System.Drawing.Size(53, 12);
            this.otherLabel.TabIndex = 5;
            this.otherLabel.Text = "其他工位";
            // 
            // moreDataGridView
            // 
            this.moreDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.moreDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.moreDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.product,
            this.mainStation,
            this.otherStation01,
            this.otherStation02,
            this.otherStation03,
            this.otherStation04,
            this.otherStation05,
            this.status});
            this.moreDataGridView.Location = new System.Drawing.Point(3, 80);
            this.moreDataGridView.Name = "moreDataGridView";
            this.moreDataGridView.RowTemplate.Height = 23;
            this.moreDataGridView.Size = new System.Drawing.Size(856, 369);
            this.moreDataGridView.TabIndex = 6;
            this.moreDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.moreDataGridView_CellContentClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // product
            // 
            this.product.DataPropertyName = "product";
            this.product.HeaderText = "产品名";
            this.product.Name = "product";
            this.product.ReadOnly = true;
            // 
            // mainStation
            // 
            this.mainStation.DataPropertyName = "mainStation";
            this.mainStation.HeaderText = "主工序";
            this.mainStation.Name = "mainStation";
            this.mainStation.ReadOnly = true;
            // 
            // otherStation01
            // 
            this.otherStation01.DataPropertyName = "otherStation01";
            this.otherStation01.HeaderText = "其他工位01";
            this.otherStation01.Name = "otherStation01";
            this.otherStation01.ReadOnly = true;
            // 
            // otherStation02
            // 
            this.otherStation02.DataPropertyName = "otherStation02";
            this.otherStation02.HeaderText = "其他工位02";
            this.otherStation02.Name = "otherStation02";
            this.otherStation02.ReadOnly = true;
            // 
            // otherStation03
            // 
            this.otherStation03.DataPropertyName = "otherStation03";
            this.otherStation03.HeaderText = "其他工位03";
            this.otherStation03.Name = "otherStation03";
            this.otherStation03.ReadOnly = true;
            // 
            // otherStation04
            // 
            this.otherStation04.DataPropertyName = "otherStation04";
            this.otherStation04.HeaderText = "其他工位04";
            this.otherStation04.Name = "otherStation04";
            this.otherStation04.ReadOnly = true;
            // 
            // otherStation05
            // 
            this.otherStation05.DataPropertyName = "otherStation05";
            this.otherStation05.HeaderText = "其他工位05";
            this.otherStation05.Name = "otherStation05";
            this.otherStation05.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(613, 51);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "添加";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(694, 51);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 8;
            this.changeButton.Text = "修改";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(775, 51);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 9;
            this.deleteButton.Text = "删除";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(341, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 10;
            // 
            // moreStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.moreDataGridView);
            this.Controls.Add(this.otherLabel);
            this.Controls.Add(this.otherComboBox);
            this.Controls.Add(this.mainComboBox);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.productLabel);
            this.Controls.Add(this.productComboBox);
            this.Name = "moreStation";
            this.Text = "moreStation";
            this.Load += new System.EventHandler(this.moreStation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.moreDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox productComboBox;
        private System.Windows.Forms.Label productLabel;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.ComboBox mainComboBox;
        private System.Windows.Forms.ComboBox otherComboBox;
        private System.Windows.Forms.Label otherLabel;
        private System.Windows.Forms.DataGridView moreDataGridView;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn product;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn otherStation01;
        private System.Windows.Forms.DataGridViewTextBoxColumn otherStation02;
        private System.Windows.Forms.DataGridViewTextBoxColumn otherStation03;
        private System.Windows.Forms.DataGridViewTextBoxColumn otherStation04;
        private System.Windows.Forms.DataGridViewTextBoxColumn otherStation05;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}