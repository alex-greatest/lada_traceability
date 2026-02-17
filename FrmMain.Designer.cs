namespace Review
{
    partial class FrmMain
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
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMin = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonChangeLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.生产看板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuData = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemReview = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNet = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBarcode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBarcode = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemReviewData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.proTemplate = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.alwaysVerification = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.EnableVerification = new System.Windows.Forms.CheckBox();
            this.cbgrade = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.plan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.proCodeText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comProduct = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusDevelop = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusNow = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("宋体", 10F);
            this.btnStop.Location = new System.Drawing.Point(355, 434);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(127, 46);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "Ост. произв";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("宋体", 10F);
            this.btnStart.Location = new System.Drawing.Point(31, 434);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(127, 46);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Начать произв";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSystem,
            this.MenuProduct,
            this.MenuData,
            this.MenuBarcode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(644, 28);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuSystem
            // 
            this.MenuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemMin,
            this.buttonChangeLanguage,
            this.生产看板ToolStripMenuItem});
            this.MenuSystem.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.MenuSystem.Name = "MenuSystem";
            this.MenuSystem.Size = new System.Drawing.Size(81, 24);
            this.MenuSystem.Text = "【系统】";
            // 
            // MenuItemMin
            // 
            this.MenuItemMin.Name = "MenuItemMin";
            this.MenuItemMin.Size = new System.Drawing.Size(180, 24);
            this.MenuItemMin.Text = "[最小化]";
            this.MenuItemMin.Click += new System.EventHandler(this.MenuItemMin_Click);
            // 
            // buttonChangeLanguage
            // 
            this.buttonChangeLanguage.Name = "buttonChangeLanguage";
            this.buttonChangeLanguage.Size = new System.Drawing.Size(180, 24);
            this.buttonChangeLanguage.Text = "[切换语言]";
            this.buttonChangeLanguage.Click += new System.EventHandler(this.buttonChangeLanguage_Click);
            // 
            // 生产看板ToolStripMenuItem
            // 
            this.生产看板ToolStripMenuItem.Name = "生产看板ToolStripMenuItem";
            this.生产看板ToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.生产看板ToolStripMenuItem.Text = "[生产看板]";
            this.生产看板ToolStripMenuItem.Click += new System.EventHandler(this.生产看板ToolStripMenuItem_Click);
            // 
            // MenuProduct
            // 
            this.MenuProduct.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemPerson,
            this.MenuItemProduct});
            this.MenuProduct.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.MenuProduct.Name = "MenuProduct";
            this.MenuProduct.Size = new System.Drawing.Size(144, 24);
            this.MenuProduct.Text = "【Упр. линией】";
            // 
            // MenuItemPerson
            // 
            this.MenuItemPerson.Name = "MenuItemPerson";
            this.MenuItemPerson.Size = new System.Drawing.Size(221, 24);
            this.MenuItemPerson.Text = "[Настр. персонала]";
            this.MenuItemPerson.Click += new System.EventHandler(this.MenuItemPerson_Click);
            // 
            // MenuItemProduct
            // 
            this.MenuItemProduct.Name = "MenuItemProduct";
            this.MenuItemProduct.Size = new System.Drawing.Size(221, 24);
            this.MenuItemProduct.Text = "[Настр. продукта]";
            this.MenuItemProduct.Click += new System.EventHandler(this.MenuItemProduct_Click);
            // 
            // MenuData
            // 
            this.MenuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemReview,
            this.MenuItemNet});
            this.MenuData.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.MenuData.Name = "MenuData";
            this.MenuData.Size = new System.Drawing.Size(166, 24);
            this.MenuData.Text = "【Просм. данных】";
            // 
            // MenuItemReview
            // 
            this.MenuItemReview.Name = "MenuItemReview";
            this.MenuItemReview.Size = new System.Drawing.Size(213, 24);
            this.MenuItemReview.Text = "[Отсл. данных]";
            this.MenuItemReview.Click += new System.EventHandler(this.MenuItemReview_Click);
            // 
            // MenuItemNet
            // 
            this.MenuItemNet.Name = "MenuItemNet";
            this.MenuItemNet.Size = new System.Drawing.Size(213, 24);
            this.MenuItemNet.Text = "[Сетевые данные]";
            this.MenuItemNet.Click += new System.EventHandler(this.MenuItemNet_Click);
            // 
            // MenuBarcode
            // 
            this.MenuBarcode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBarcode,
            this.MenuItemReviewData,
            this.toolStripMenuItem1});
            this.MenuBarcode.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.MenuBarcode.Name = "MenuBarcode";
            this.MenuBarcode.Size = new System.Drawing.Size(156, 24);
            this.MenuBarcode.Text = "【Упр. данными】";
            // 
            // MenuItemBarcode
            // 
            this.MenuItemBarcode.Name = "MenuItemBarcode";
            this.MenuItemBarcode.Size = new System.Drawing.Size(230, 24);
            this.MenuItemBarcode.Text = "[Настр. штрих-кода]";
            this.MenuItemBarcode.Click += new System.EventHandler(this.MenuItemBarcode_Click);
            // 
            // MenuItemReviewData
            // 
            this.MenuItemReviewData.Name = "MenuItemReviewData";
            this.MenuItemReviewData.Size = new System.Drawing.Size(230, 24);
            this.MenuItemReviewData.Text = "[Настр. отслеж.]";
            this.MenuItemReviewData.Click += new System.EventHandler(this.MenuItemReviewData_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(230, 24);
            this.toolStripMenuItem1.Text = "档次号设置";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.panel1);
            this.groupBox.Controls.Add(this.alwaysVerification);
            this.groupBox.Controls.Add(this.button3);
            this.groupBox.Controls.Add(this.button2);
            this.groupBox.Controls.Add(this.EnableVerification);
            this.groupBox.Controls.Add(this.cbgrade);
            this.groupBox.Controls.Add(this.label7);
            this.groupBox.Controls.Add(this.plan);
            this.groupBox.Controls.Add(this.label5);
            this.groupBox.Controls.Add(this.proCodeText);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.comProduct);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Font = new System.Drawing.Font("微软雅黑 Light", 17F, System.Drawing.FontStyle.Bold);
            this.groupBox.Location = new System.Drawing.Point(11, 43);
            this.groupBox.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox.Size = new System.Drawing.Size(620, 373);
            this.groupBox.TabIndex = 22;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Произв. настр";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.proTemplate);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(10, 265);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 95);
            this.panel1.TabIndex = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑 Light", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(5, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 21);
            this.label4.TabIndex = 49;
            this.label4.Text = "Выбор шабл. штрих-кода";
            // 
            // proTemplate
            // 
            this.proTemplate.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.proTemplate.Location = new System.Drawing.Point(6, 43);
            this.proTemplate.Name = "proTemplate";
            this.proTemplate.ReadOnly = true;
            this.proTemplate.Size = new System.Drawing.Size(291, 26);
            this.proTemplate.TabIndex = 50;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(333, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 33);
            this.button1.TabIndex = 46;
            this.button1.Text = "Выбор";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // alwaysVerification
            // 
            this.alwaysVerification.AutoSize = true;
            this.alwaysVerification.Font = new System.Drawing.Font("微软雅黑 Light", 13F, System.Drawing.FontStyle.Bold);
            this.alwaysVerification.Location = new System.Drawing.Point(344, 59);
            this.alwaysVerification.Name = "alwaysVerification";
            this.alwaysVerification.Size = new System.Drawing.Size(264, 28);
            this.alwaysVerification.TabIndex = 59;
            this.alwaysVerification.Text = "Запуск сверки № партии";
            this.alwaysVerification.UseVisualStyleBackColor = true;
            this.alwaysVerification.CheckedChanged += new System.EventHandler(this.alwaysVerification_CheckedChanged);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.button3.Location = new System.Drawing.Point(429, 112);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 48);
            this.button3.TabIndex = 58;
            this.button3.Text = "Скан. № партии";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(403, 182);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 71);
            this.button2.TabIndex = 57;
            this.button2.Text = "Настройки печати производственной модели";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EnableVerification
            // 
            this.EnableVerification.AutoSize = true;
            this.EnableVerification.Checked = true;
            this.EnableVerification.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableVerification.Font = new System.Drawing.Font("微软雅黑 Light", 13F, System.Drawing.FontStyle.Bold);
            this.EnableVerification.Location = new System.Drawing.Point(344, 23);
            this.EnableVerification.Name = "EnableVerification";
            this.EnableVerification.Size = new System.Drawing.Size(264, 28);
            this.EnableVerification.TabIndex = 56;
            this.EnableVerification.Text = "Запуск сверки № партии";
            this.EnableVerification.UseVisualStyleBackColor = true;
            this.EnableVerification.CheckedChanged += new System.EventHandler(this.EnableVerification_CheckedChanged);
            // 
            // cbgrade
            // 
            this.cbgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbgrade.Font = new System.Drawing.Font("宋体", 14F);
            this.cbgrade.FormattingEnabled = true;
            this.cbgrade.Location = new System.Drawing.Point(155, 209);
            this.cbgrade.Margin = new System.Windows.Forms.Padding(2);
            this.cbgrade.Name = "cbgrade";
            this.cbgrade.Size = new System.Drawing.Size(153, 27);
            this.cbgrade.TabIndex = 55;
            this.cbgrade.Click += new System.EventHandler(this.cbgrade_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(12, 209);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 25);
            this.label7.TabIndex = 54;
            this.label7.Text = "№ группы";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // plan
            // 
            this.plan.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.plan.Location = new System.Drawing.Point(155, 153);
            this.plan.Name = "plan";
            this.plan.Size = new System.Drawing.Size(153, 26);
            this.plan.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(12, 142);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 53);
            this.label5.TabIndex = 51;
            this.label5.Text = "Планируемый выход：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // proCodeText
            // 
            this.proCodeText.Enabled = false;
            this.proCodeText.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.proCodeText.Location = new System.Drawing.Point(155, 100);
            this.proCodeText.Name = "proCodeText";
            this.proCodeText.ReadOnly = true;
            this.proCodeText.Size = new System.Drawing.Size(153, 26);
            this.proCodeText.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(15, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 25);
            this.label3.TabIndex = 47;
            this.label3.Text = "Штрих-код：";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // comProduct
            // 
            this.comProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comProduct.Font = new System.Drawing.Font("宋体", 14F);
            this.comProduct.FormattingEnabled = true;
            this.comProduct.Location = new System.Drawing.Point(155, 47);
            this.comProduct.Margin = new System.Windows.Forms.Padding(2);
            this.comProduct.Name = "comProduct";
            this.comProduct.Size = new System.Drawing.Size(153, 27);
            this.comProduct.TabIndex = 44;
            this.comProduct.SelectedValueChanged += new System.EventHandler(this.comProduct_SelectedValueChanged);
            this.comProduct.Click += new System.EventHandler(this.comProduct_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(45, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 25);
            this.label1.TabIndex = 43;
            this.label1.Text = "Модель：";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusInfo,
            this.toolStatusUser,
            this.toolStatusDevelop,
            this.toolStatusNow});
            this.statusStrip.Location = new System.Drawing.Point(0, 496);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(644, 22);
            this.statusStrip.TabIndex = 32;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStatusInfo
            // 
            this.toolStatusInfo.Name = "toolStatusInfo";
            this.toolStatusInfo.Size = new System.Drawing.Size(131, 17);
            this.toolStatusInfo.Text = "toolStripStatusLabel1";
            // 
            // toolStatusUser
            // 
            this.toolStatusUser.Name = "toolStatusUser";
            this.toolStatusUser.Size = new System.Drawing.Size(131, 17);
            this.toolStatusUser.Text = "toolStripStatusLabel1";
            // 
            // toolStatusDevelop
            // 
            this.toolStatusDevelop.Name = "toolStatusDevelop";
            this.toolStatusDevelop.Size = new System.Drawing.Size(131, 17);
            this.toolStatusDevelop.Text = "toolStripStatusLabel1";
            // 
            // toolStatusNow
            // 
            this.toolStatusNow.Name = "toolStatusNow";
            this.toolStatusNow.Size = new System.Drawing.Size(131, 17);
            this.toolStatusNow.Text = "toolStripStatusLabel1";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 518);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "质量追溯系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuSystem;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMin;
        private System.Windows.Forms.ToolStripMenuItem buttonChangeLanguage;
        private System.Windows.Forms.ToolStripMenuItem MenuProduct;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPerson;
        private System.Windows.Forms.ToolStripMenuItem MenuItemProduct;
        private System.Windows.Forms.ToolStripMenuItem MenuData;
        private System.Windows.Forms.ToolStripMenuItem MenuBarcode;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBarcode;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusDevelop;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusNow;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem MenuItemReview;
        private System.Windows.Forms.ToolStripMenuItem MenuItemReviewData;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNet;
        private System.Windows.Forms.ComboBox comProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox proTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox proCodeText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem 生产看板ToolStripMenuItem;
        private System.Windows.Forms.TextBox plan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbgrade;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox EnableVerification;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox alwaysVerification;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}