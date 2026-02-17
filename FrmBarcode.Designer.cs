
namespace Review
{
    partial class FrmBarcode
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
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.comStation = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textCode1 = new System.Windows.Forms.TextBox();
            this.textCode2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textCode4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textCode3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textCode8 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textCode7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textCode6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textCode5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBarcode1 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName1 = new System.Windows.Forms.TextBox();
            this.txtBarcodeName2 = new System.Windows.Forms.TextBox();
            this.checkBarcode2 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName3 = new System.Windows.Forms.TextBox();
            this.checkBarcode3 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName4 = new System.Windows.Forms.TextBox();
            this.checkBarcode4 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName5 = new System.Windows.Forms.TextBox();
            this.checkBarcode5 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName6 = new System.Windows.Forms.TextBox();
            this.checkBarcode6 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName7 = new System.Windows.Forms.TextBox();
            this.checkBarcode7 = new System.Windows.Forms.CheckBox();
            this.txtBarcodeName8 = new System.Windows.Forms.TextBox();
            this.checkBarcode8 = new System.Windows.Forms.CheckBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.textCodeRule8 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textCodeRule7 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textCodeRule6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textCodeRule5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textCodeRule4 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textCodeRule3 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textCodeRule2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textCodeRule1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnRule = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_DataReceived);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(406, 44);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // comProduct
            // 
            this.comProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProduct.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(509, 28);
            this.comProduct.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(84, 22);
            this.comProduct.TabIndex = 37;
            this.comProduct.SelectedIndexChanged += new System.EventHandler(this.comProduct_SelectedIndexChanged);
            // 
            // btnSet
            // 
            this.btnSet.BackColor = System.Drawing.SystemColors.Control;
            this.btnSet.Location = new System.Drawing.Point(600, 28);
            this.btnSet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(76, 22);
            this.btnSet.TabIndex = 36;
            this.btnSet.Text = "设置条码";
            this.btnSet.UseVisualStyleBackColor = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // comStation
            // 
            this.comStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comStation.Font = new System.Drawing.Font("宋体", 10.8F);
            this.comStation.FormattingEnabled = true;
            this.comStation.Location = new System.Drawing.Point(419, 28);
            this.comStation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comStation.Name = "comStation";
            this.comStation.Size = new System.Drawing.Size(84, 22);
            this.comStation.TabIndex = 35;
            this.comStation.SelectedIndexChanged += new System.EventHandler(this.comStation_SelectedIndexChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(85, 18);
            this.label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(35, 12);
            this.label.TabIndex = 2;
            this.label.Text = "label";
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.SystemColors.Control;
            this.btnOpen.Location = new System.Drawing.Point(8, 13);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(68, 22);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "条码1:";
            // 
            // textCode1
            // 
            this.textCode1.Location = new System.Drawing.Point(53, 67);
            this.textCode1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode1.Name = "textCode1";
            this.textCode1.Size = new System.Drawing.Size(252, 21);
            this.textCode1.TabIndex = 4;
            this.textCode1.Text = "0123456789012345678901234567890123456789";
            // 
            // textCode2
            // 
            this.textCode2.Location = new System.Drawing.Point(53, 92);
            this.textCode2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode2.Name = "textCode2";
            this.textCode2.Size = new System.Drawing.Size(251, 21);
            this.textCode2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "条码2:";
            // 
            // textCode4
            // 
            this.textCode4.Location = new System.Drawing.Point(53, 141);
            this.textCode4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode4.Name = "textCode4";
            this.textCode4.Size = new System.Drawing.Size(251, 21);
            this.textCode4.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "条码4:";
            // 
            // textCode3
            // 
            this.textCode3.Location = new System.Drawing.Point(53, 117);
            this.textCode3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode3.Name = "textCode3";
            this.textCode3.Size = new System.Drawing.Size(251, 21);
            this.textCode3.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "条码3:";
            // 
            // textCode8
            // 
            this.textCode8.Location = new System.Drawing.Point(53, 241);
            this.textCode8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode8.Name = "textCode8";
            this.textCode8.Size = new System.Drawing.Size(251, 21);
            this.textCode8.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 245);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "条码8:";
            // 
            // textCode7
            // 
            this.textCode7.Location = new System.Drawing.Point(53, 216);
            this.textCode7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode7.Name = "textCode7";
            this.textCode7.Size = new System.Drawing.Size(251, 21);
            this.textCode7.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 221);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "条码7:";
            // 
            // textCode6
            // 
            this.textCode6.Location = new System.Drawing.Point(53, 191);
            this.textCode6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode6.Name = "textCode6";
            this.textCode6.Size = new System.Drawing.Size(251, 21);
            this.textCode6.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 196);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "条码6:";
            // 
            // textCode5
            // 
            this.textCode5.Location = new System.Drawing.Point(53, 166);
            this.textCode5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textCode5.Name = "textCode5";
            this.textCode5.Size = new System.Drawing.Size(251, 21);
            this.textCode5.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 171);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "条码5:";
            // 
            // checkBarcode1
            // 
            this.checkBarcode1.AutoSize = true;
            this.checkBarcode1.Location = new System.Drawing.Point(307, 69);
            this.checkBarcode1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode1.Name = "checkBarcode1";
            this.checkBarcode1.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode1.TabIndex = 19;
            this.checkBarcode1.Text = "PLC";
            this.checkBarcode1.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName1
            // 
            this.txtBarcodeName1.Location = new System.Drawing.Point(346, 66);
            this.txtBarcodeName1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName1.Name = "txtBarcodeName1";
            this.txtBarcodeName1.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName1.TabIndex = 20;
            // 
            // txtBarcodeName2
            // 
            this.txtBarcodeName2.Location = new System.Drawing.Point(346, 91);
            this.txtBarcodeName2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName2.Name = "txtBarcodeName2";
            this.txtBarcodeName2.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName2.TabIndex = 22;
            // 
            // checkBarcode2
            // 
            this.checkBarcode2.AutoSize = true;
            this.checkBarcode2.Location = new System.Drawing.Point(307, 93);
            this.checkBarcode2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode2.Name = "checkBarcode2";
            this.checkBarcode2.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode2.TabIndex = 21;
            this.checkBarcode2.Text = "PLC";
            this.checkBarcode2.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName3
            // 
            this.txtBarcodeName3.Location = new System.Drawing.Point(346, 116);
            this.txtBarcodeName3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName3.Name = "txtBarcodeName3";
            this.txtBarcodeName3.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName3.TabIndex = 24;
            // 
            // checkBarcode3
            // 
            this.checkBarcode3.AutoSize = true;
            this.checkBarcode3.Location = new System.Drawing.Point(307, 118);
            this.checkBarcode3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode3.Name = "checkBarcode3";
            this.checkBarcode3.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode3.TabIndex = 23;
            this.checkBarcode3.Text = "PLC";
            this.checkBarcode3.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName4
            // 
            this.txtBarcodeName4.Location = new System.Drawing.Point(346, 141);
            this.txtBarcodeName4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName4.Name = "txtBarcodeName4";
            this.txtBarcodeName4.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName4.TabIndex = 26;
            // 
            // checkBarcode4
            // 
            this.checkBarcode4.AutoSize = true;
            this.checkBarcode4.Location = new System.Drawing.Point(307, 144);
            this.checkBarcode4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode4.Name = "checkBarcode4";
            this.checkBarcode4.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode4.TabIndex = 25;
            this.checkBarcode4.Text = "PLC";
            this.checkBarcode4.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName5
            // 
            this.txtBarcodeName5.Location = new System.Drawing.Point(346, 166);
            this.txtBarcodeName5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName5.Name = "txtBarcodeName5";
            this.txtBarcodeName5.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName5.TabIndex = 28;
            // 
            // checkBarcode5
            // 
            this.checkBarcode5.AutoSize = true;
            this.checkBarcode5.Location = new System.Drawing.Point(307, 169);
            this.checkBarcode5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode5.Name = "checkBarcode5";
            this.checkBarcode5.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode5.TabIndex = 27;
            this.checkBarcode5.Text = "PLC";
            this.checkBarcode5.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName6
            // 
            this.txtBarcodeName6.Location = new System.Drawing.Point(346, 191);
            this.txtBarcodeName6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName6.Name = "txtBarcodeName6";
            this.txtBarcodeName6.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName6.TabIndex = 30;
            // 
            // checkBarcode6
            // 
            this.checkBarcode6.AutoSize = true;
            this.checkBarcode6.Location = new System.Drawing.Point(307, 193);
            this.checkBarcode6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode6.Name = "checkBarcode6";
            this.checkBarcode6.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode6.TabIndex = 29;
            this.checkBarcode6.Text = "PLC";
            this.checkBarcode6.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName7
            // 
            this.txtBarcodeName7.Location = new System.Drawing.Point(346, 216);
            this.txtBarcodeName7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName7.Name = "txtBarcodeName7";
            this.txtBarcodeName7.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName7.TabIndex = 32;
            // 
            // checkBarcode7
            // 
            this.checkBarcode7.AutoSize = true;
            this.checkBarcode7.Location = new System.Drawing.Point(307, 218);
            this.checkBarcode7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode7.Name = "checkBarcode7";
            this.checkBarcode7.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode7.TabIndex = 31;
            this.checkBarcode7.Text = "PLC";
            this.checkBarcode7.UseVisualStyleBackColor = true;
            // 
            // txtBarcodeName8
            // 
            this.txtBarcodeName8.Location = new System.Drawing.Point(346, 241);
            this.txtBarcodeName8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBarcodeName8.Name = "txtBarcodeName8";
            this.txtBarcodeName8.Size = new System.Drawing.Size(96, 21);
            this.txtBarcodeName8.TabIndex = 34;
            // 
            // checkBarcode8
            // 
            this.checkBarcode8.AutoSize = true;
            this.checkBarcode8.Location = new System.Drawing.Point(307, 243);
            this.checkBarcode8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBarcode8.Name = "checkBarcode8";
            this.checkBarcode8.Size = new System.Drawing.Size(42, 16);
            this.checkBarcode8.TabIndex = 33;
            this.checkBarcode8.Text = "PLC";
            this.checkBarcode8.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // textCodeRule8
            // 
            this.textCodeRule8.Location = new System.Drawing.Point(521, 241);
            this.textCodeRule8.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule8.Name = "textCodeRule8";
            this.textCodeRule8.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule8.TabIndex = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(455, 245);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "条码规则8:";
            // 
            // textCodeRule7
            // 
            this.textCodeRule7.Location = new System.Drawing.Point(521, 216);
            this.textCodeRule7.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule7.Name = "textCodeRule7";
            this.textCodeRule7.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule7.TabIndex = 48;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(455, 221);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 47;
            this.label10.Text = "条码规则7:";
            // 
            // textCodeRule6
            // 
            this.textCodeRule6.Location = new System.Drawing.Point(521, 191);
            this.textCodeRule6.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule6.Name = "textCodeRule6";
            this.textCodeRule6.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule6.TabIndex = 46;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(455, 196);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 45;
            this.label11.Text = "条码规则6:";
            // 
            // textCodeRule5
            // 
            this.textCodeRule5.Location = new System.Drawing.Point(521, 166);
            this.textCodeRule5.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule5.Name = "textCodeRule5";
            this.textCodeRule5.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule5.TabIndex = 44;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(455, 171);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 43;
            this.label12.Text = "条码规则5:";
            // 
            // textCodeRule4
            // 
            this.textCodeRule4.Location = new System.Drawing.Point(521, 141);
            this.textCodeRule4.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule4.Name = "textCodeRule4";
            this.textCodeRule4.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule4.TabIndex = 42;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(455, 146);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 41;
            this.label13.Text = "条码规则4:";
            // 
            // textCodeRule3
            // 
            this.textCodeRule3.Location = new System.Drawing.Point(521, 117);
            this.textCodeRule3.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule3.Name = "textCodeRule3";
            this.textCodeRule3.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule3.TabIndex = 40;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(455, 121);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 39;
            this.label14.Text = "条码规则3:";
            // 
            // textCodeRule2
            // 
            this.textCodeRule2.Location = new System.Drawing.Point(521, 92);
            this.textCodeRule2.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule2.Name = "textCodeRule2";
            this.textCodeRule2.Size = new System.Drawing.Size(251, 21);
            this.textCodeRule2.TabIndex = 38;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(455, 97);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 37;
            this.label15.Text = "条码规则2:";
            // 
            // textCodeRule1
            // 
            this.textCodeRule1.Location = new System.Drawing.Point(521, 67);
            this.textCodeRule1.Margin = new System.Windows.Forms.Padding(2);
            this.textCodeRule1.Name = "textCodeRule1";
            this.textCodeRule1.Size = new System.Drawing.Size(252, 21);
            this.textCodeRule1.TabIndex = 36;
            this.textCodeRule1.Text = "0123456789012345678901234567890123456789";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(455, 72);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "条码规则1:";
            // 
            // btnRule
            // 
            this.btnRule.BackColor = System.Drawing.SystemColors.Control;
            this.btnRule.Location = new System.Drawing.Point(680, 28);
            this.btnRule.Margin = new System.Windows.Forms.Padding(2);
            this.btnRule.Name = "btnRule";
            this.btnRule.Size = new System.Drawing.Size(90, 22);
            this.btnRule.TabIndex = 51;
            this.btnRule.Text = "设置条码规则";
            this.btnRule.UseVisualStyleBackColor = false;
            this.btnRule.Click += new System.EventHandler(this.btnRule_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(509, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 22);
            this.button1.TabIndex = 52;
            this.button1.Text = "设置条码规则";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 276);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRule);
            this.Controls.Add(this.comProduct);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.textCodeRule8);
            this.Controls.Add(this.comStation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCodeRule7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textCodeRule6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textCodeRule5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textCodeRule4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textCodeRule3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textCodeRule2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textCodeRule1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtBarcodeName8);
            this.Controls.Add(this.checkBarcode8);
            this.Controls.Add(this.txtBarcodeName7);
            this.Controls.Add(this.checkBarcode7);
            this.Controls.Add(this.txtBarcodeName6);
            this.Controls.Add(this.checkBarcode6);
            this.Controls.Add(this.txtBarcodeName5);
            this.Controls.Add(this.checkBarcode5);
            this.Controls.Add(this.txtBarcodeName4);
            this.Controls.Add(this.checkBarcode4);
            this.Controls.Add(this.txtBarcodeName3);
            this.Controls.Add(this.checkBarcode3);
            this.Controls.Add(this.txtBarcodeName2);
            this.Controls.Add(this.checkBarcode2);
            this.Controls.Add(this.txtBarcodeName1);
            this.Controls.Add(this.checkBarcode1);
            this.Controls.Add(this.textCode8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textCode7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textCode6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textCode5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textCode4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textCode3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textCode2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textCode1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条码设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBarcode_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBarcode_FormClosed);
            this.Load += new System.EventHandler(this.FrmBarcode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox comStation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCode1;
        private System.Windows.Forms.TextBox textCode2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textCode4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textCode3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCode8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textCode7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textCode6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textCode5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.CheckBox checkBarcode1;
        private System.Windows.Forms.TextBox txtBarcodeName1;
        private System.Windows.Forms.TextBox txtBarcodeName2;
        private System.Windows.Forms.CheckBox checkBarcode2;
        private System.Windows.Forms.TextBox txtBarcodeName3;
        private System.Windows.Forms.CheckBox checkBarcode3;
        private System.Windows.Forms.TextBox txtBarcodeName4;
        private System.Windows.Forms.CheckBox checkBarcode4;
        private System.Windows.Forms.TextBox txtBarcodeName5;
        private System.Windows.Forms.CheckBox checkBarcode5;
        private System.Windows.Forms.TextBox txtBarcodeName6;
        private System.Windows.Forms.CheckBox checkBarcode6;
        private System.Windows.Forms.TextBox txtBarcodeName7;
        private System.Windows.Forms.CheckBox checkBarcode7;
        private System.Windows.Forms.TextBox txtBarcodeName8;
        private System.Windows.Forms.CheckBox checkBarcode8;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox textCodeRule8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textCodeRule7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textCodeRule6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textCodeRule5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textCodeRule4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textCodeRule3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textCodeRule2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textCodeRule1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnRule;
        private System.Windows.Forms.Button button1;
    }
}