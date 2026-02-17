namespace Review
{
    partial class FrmNet
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listSendData = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listReceiveData = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtStationOrder = new System.Windows.Forms.TextBox();
            this.comStation = new System.Windows.Forms.ComboBox();
            this.timerUpData = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listSendData);
            this.groupBox1.Location = new System.Drawing.Point(9, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(509, 673);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "反馈数据:";
            // 
            // listSendData
            // 
            this.listSendData.Font = new System.Drawing.Font("宋体", 21F);
            this.listSendData.FormattingEnabled = true;
            this.listSendData.ItemHeight = 28;
            this.listSendData.Location = new System.Drawing.Point(6, 17);
            this.listSendData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listSendData.Name = "listSendData";
            this.listSendData.Size = new System.Drawing.Size(494, 648);
            this.listSendData.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listReceiveData);
            this.groupBox2.Location = new System.Drawing.Point(533, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(534, 726);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工位数据:";
            // 
            // listReceiveData
            // 
            this.listReceiveData.Font = new System.Drawing.Font("宋体", 21F);
            this.listReceiveData.FormattingEnabled = true;
            this.listReceiveData.ItemHeight = 28;
            this.listReceiveData.Location = new System.Drawing.Point(11, 13);
            this.listReceiveData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listReceiveData.Name = "listReceiveData";
            this.listReceiveData.Size = new System.Drawing.Size(519, 704);
            this.listReceiveData.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtStationOrder);
            this.groupBox3.Controls.Add(this.comStation);
            this.groupBox3.Location = new System.Drawing.Point(9, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(260, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工位选择";
            // 
            // txtStationOrder
            // 
            this.txtStationOrder.Enabled = false;
            this.txtStationOrder.Location = new System.Drawing.Point(188, 18);
            this.txtStationOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStationOrder.Name = "txtStationOrder";
            this.txtStationOrder.Size = new System.Drawing.Size(64, 21);
            this.txtStationOrder.TabIndex = 1;
            // 
            // comStation
            // 
            this.comStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStation.FormattingEnabled = true;
            this.comStation.Location = new System.Drawing.Point(10, 18);
            this.comStation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comStation.Name = "comStation";
            this.comStation.Size = new System.Drawing.Size(174, 20);
            this.comStation.TabIndex = 0;
            this.comStation.SelectedIndexChanged += new System.EventHandler(this.comStation_SelectedIndexChanged);
            // 
            // timerUpData
            // 
            this.timerUpData.Interval = 500;
            this.timerUpData.Tick += new System.EventHandler(this.timerUpData_Tick);
            // 
            // FrmNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 747);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯数据";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNet_FormClosed);
            this.Load += new System.EventHandler(this.FrmNet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comStation;
        private System.Windows.Forms.ListBox listSendData;
        private System.Windows.Forms.ListBox listReceiveData;
        private System.Windows.Forms.Timer timerUpData;
        private System.Windows.Forms.TextBox txtStationOrder;
    }
}