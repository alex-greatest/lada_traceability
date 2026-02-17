using System;
using Review.Repository;
using System.Windows.Forms;
using Review.Model;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.IO.Ports;

namespace Review.UI
{
    public partial class FrmStandCode : Form
    {
        static int txtCount = 0;
        string s1, s2, s3, s4, s5, s6, s7;
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        private SerialPort serialPort;
        // 用来管理控件
        List<TextBox> FirtextBoxList = new List<TextBox>();
        List<TextBox> SectextBoxList = new List<TextBox>();
        public FrmStandCode()
        {
            InitializeComponent();
            txtgrade.Items.Add("1");
            txtgrade.Items.Add("2");
            txtgrade.Items.Add("3");
            initialProName();
        }
        public FrmStandCode(ResourceManager resManager, CultureInfo currentCulture)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;
            txtCount = 0;
            UpdateLanguage(currentCulture);
            initialProName();
            initSeri();
            initGradelist();
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN")
            {

                s1 = "保存成功";
                s2 = "保存失败";
                s3 = "部件名称有重复项";
                s4 = "当前行内容为空，将不会保存数据";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                s1 = "Сохранено";
                s2 = "Ошибка сохранения";
                s3 = "Уже есть деталь с таким наименованием";
                s4 = "Отсутствует содержание строки, данные не будут сохранены";
            }
            // 更新窗体文本
            labelInfo.Text = resManager.GetString("FvscLabelinfo");
            labelPName.Text = resManager.GetString("FvscLabelPname");
            labelgrade.Text = resManager.GetString("FvscLabelgrade");
            labelStand.Text = resManager.GetString("Fvsclabelstand");
            btnSave.Text = resManager.GetString("FscbtnSave");
            btnAdd.Text = resManager.GetString("FscbtnAdd");
            btnOpenSc.Text = resManager.GetString("Fvscopensc");
        }
        private void initSeri()
        {
            serialPort = new SerialPort
            {
                PortName = "COM1",       // 修改为实际端口
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBits.One,
                Parity = Parity.None,
                Encoding = System.Text.Encoding.ASCII
            };
            try
            {
                if (!serialPort.IsOpen)
                    serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口打开失败: " + ex.Message);
            }
            serialPort.DataReceived += SerialPort_DataReceived;
        }
        private void FrmStandCode_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
                serialPort.Close();
        }
        // 串口接收数据
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadExisting();

                // 切回UI线程，把数据写到当前激活的控件
                this.Invoke(new Action(() =>
                {
                    if (this.ActiveControl is TextBox tb)
                    {
                        tb.AppendText(data.Trim()); // 直接输入到当前激活的TextBox
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取失败: " + ex.Message);
            }
        }
        private void btnOpenSc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort.IsOpen)
                    serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("串口打开失败: " + ex.Message);
            }
        }

        private void initialProName() {
            string selectSql = $"select productName from product";
            var s = DBHelper.GetDataList(selectSql , 0);
            if(s != null) txtpname.DataSource = s;
        }
        
        private void btnSure_Click(object sender, EventArgs e)
        {
            
        }

        private void txtpname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtpname.Text) && !string.IsNullOrEmpty(txtgrade.Text)) {
                initialData();
            }
        }

        private void initialData() {
            var standardCodeList = SqlSugarHelper.GetDataList<standardcode>(s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text);
            txtCount = 0;
            FirtextBoxList.Clear();
            SectextBoxList.Clear();
            panel3.Controls.Clear();
            if (standardCodeList.Count > 0)
            {
                for (int i = 0; i < standardCodeList.Count; i++)
                {
                    var sc = standardCodeList[i];
                    addControlBox();
                    FirtextBoxList[i].Text = sc.PartName;
                    SectextBoxList[i].Text = sc.PartCode;
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addControlBox();
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            var duplicates = FirtextBoxList
                .GroupBy(x => x.Text)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Any())
            {
                MessageBox.Show(s3);
            }
            else
            {
                Console.WriteLine("没有重复项");
                saveStandardCodeData();
            }
            
        }
        private void initGradelist()
        {
            txtgrade.Items.Clear();
            var grade = SqlSugarHelper.GetDataList<tablegrade>(t => t.Id == 1);
            if (grade.Count > 0)
            {
                var type = typeof(tablegrade);
                for (int i = 1; i < 11; i++)
                {
                    string vname = "grade" + (i).ToString("D2");
                    var prop = type.GetProperty(vname);
                    if (prop != null && prop.CanWrite)
                    {
                        var va = prop.GetValue(grade[0]).ToString();
                        if (!string.IsNullOrWhiteSpace(va))
                        {
                            txtgrade.Items.Add(va);
                        }
                    }
                }
            }
        }
        private void saveStandardCodeData() {
            List<standardcode> sdclist = new List<standardcode>();
            List<string> newDataPartNamelist = new List<string>();
            for (int i = 0; i < FirtextBoxList.Count; i++)
            {
                if (string.IsNullOrEmpty(FirtextBoxList[i].Text) || string.IsNullOrEmpty(SectextBoxList[i].Text))
                {
                    MessageBox.Show(s4); continue;
                }
                var test = new standardcode
                {
                    ProductName = txtpname.Text,
                    ProductType = 1,
                    Grade = txtgrade.Text,
                    PartName = FirtextBoxList[i].Text,
                    PartCode = SectextBoxList[i].Text,
                    checkDigit = 1
                };
                newDataPartNamelist.Add(FirtextBoxList[i].Text);
                sdclist.Add(test);
            }
            if (sdclist.Count > 0) {
                if (SqlSugarHelper.ParaMetersUpOrInEntity(sdclist) > 0) {
                    MessageBox.Show(s1);
                    //数据保存更新成功后，清除多余数据
                    var partNameList = SqlSugarHelper.GetColumnList<standardcode, string>(s => s.PartName, s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text);
                    foreach (var name in partNameList) {
                        if (!newDataPartNamelist.Contains(name)) {
                            var deleteRes = SqlSugarHelper.DeleteByCondition<standardcode>(s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text && s.PartName == name);
                            if (deleteRes > 0) Console.WriteLine($"标准码数据记录清除旧数据完成 产品型号 - {txtpname.Text} - 档次号 - {txtgrade.Text} - 部件名称 - {name} -");
                            else Console.WriteLine($"数据记录清除失败");
                        }
                    }
                }
                else MessageBox.Show(s2); }

        }
        private void addControlBox()
        {
            var titileName = resManager.GetString("FscAddtitle");
            TextBox tb1 = new TextBox();
            tb1.Tag = txtCount;  // 可用于后续操作时识别数据索引
            tb1.Location = new System.Drawing.Point(24, 30 * txtCount + 20);
            tb1.Name = $"FtextBox{txtCount}";
            tb1.Size = new System.Drawing.Size(300, 21);
            tb1.BorderStyle = BorderStyle.None;
            tb1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            tb1.BackColor = panel3.BackColor;
            tb1.Text = titileName;

            TextBox tb2 = new TextBox();
            tb2.Width = 200;
            tb2.Tag = txtCount;  // 可用于后续操作时识别数据索引
            tb2.Location = new System.Drawing.Point(326, 30 * txtCount + 20);
            tb2.Name = $"StextBox{txtCount}";
            tb2.Size = new System.Drawing.Size(170, 21);
            if (tb2.Location.Y + tb2.Size.Height >= (panel3.Size.Height - 10))
            {
                if (panel3.Location.Y + panel3.Size.Height > (this.Height - 80))
                {
                    this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + (30 * txtCount + 70));
                }
                panel3.Size = new System.Drawing.Size(panel3.Size.Width, panel3.Size.Height + (30 * txtCount + 50));
            }
            // 加入 Panel 和列表
            panel3.Controls.Add(tb1);
            panel3.Controls.Add(tb2);
            FirtextBoxList.Add(tb1);
            SectextBoxList.Add(tb2);
            txtCount++;
        }
    }
}
