using System;
using System.Collections.Generic;
using Review.Repository;
using Review.Model;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using Org.BouncyCastle.Bcpg.Sig;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.IO.Ports;

namespace Review.UI
{
    public partial class FrmVerifySCode : Form
    {
        static int txtCount = 0;
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        // 用来管理控件
        List<TextBox> FirtextBoxList = new List<TextBox>();
        List<TextBox> SectextBoxList = new List<TextBox>();
        private SerialPort serialPort;
        List<TextBox> ThitextBoxList = new List<TextBox>();
        string productName;
        string grade;
        string s1, s2, s3, s4, s5, s6, s7;
        bool alwaysVerification;
        public FrmVerifySCode()
        {
            InitializeComponent();
        }
        public FrmVerifySCode(ResourceManager resManager, CultureInfo currentCulture, string productName , string grade, bool alwaysVerification)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;
            txtCount = 0;
            this.grade = grade;
            this.productName = productName;
            this.alwaysVerification = alwaysVerification;
            UpdateLanguage(currentCulture);
            InitialProName();
            initSeri();
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
                s3 = "当前产品名称档次号尚未设置标准码";
                s4 = "当前行内容为空，将不会保存数据";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                s1 = "Сохранено";
                s2 = "Ошибка сохранения";
                s3 = "Не был задан стандарт номера группы для данного наименования модели";
                s4 = "Отсутствует содержание строки, данные не будут сохранены";
            }
            // 更新窗体文本
            labelInfo.Text = resManager.GetString("FvscLabelinfo");
            labelPName.Text = resManager.GetString("FvscLabelPname");
            labelgrade.Text = resManager.GetString("FvscLabelgrade");
            labelScan.Text = resManager.GetString("Fvsclabelscan");
            labelStand.Text = resManager.GetString("Fvsclabelstand");
            btnCheck.Text = resManager.GetString("Fvscbtncheck");
            btnOpenSc.Text = resManager.GetString("Fvscopensc");
            btnSure.Text = resManager.GetString("Fvscbtnsure");
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
        private void InitialProName()
        {
            txtpname.Text = productName;
            txtgrade.Text = grade;
            InitialStandAndScanCode();
        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (ThitextBoxList.Count < 1) { return; }
            for (int i = 0; i < ThitextBoxList.Count; i++) {
                if (ThitextBoxList[i].Text.Equals(FirtextBoxList[i].Text))
                {
                    FirtextBoxList[i].BackColor = System.Drawing.Color.LightGreen;
                }
                else { 
                    FirtextBoxList[i].BackColor = System.Drawing.Color.Red;
                }
            }
            SaveScanCodeData();
        }
        private void btnSure_Click(object sender, EventArgs e)
        {
            InitialStandAndScanCode();
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
        private void InitialStandAndScanCode()
        {
            txtCount = 0;
            panel3.Controls.Clear();
            InitialTitle();
            var sdcList = SqlSugarHelper.GetDataList<standardcode>(s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text);
            var scancDict = SqlSugarHelper
                .GetDataList<scancode>(s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text)
                .ToDictionary(s => s.PartName, s => s.ScanPartCode);
            if (sdcList.Count == 0) { MessageBox.Show($"{txtpname.Text}  -  {txtgrade.Text}  " + s3); }
            foreach (var std in sdcList)
            {
                scancDict.TryGetValue(std.PartName, out var scanCode);

                scanCode = scanCode ?? "";

                AddControlBox(std.PartCode, std.PartName, scanCode);
            }
        }
        private void SaveScanCodeData()
        {
            List<scancode> sdclist = new List<scancode>();
            List<string> newDataPartNamelist = new List<string>();
            for (int i = 0; i < FirtextBoxList.Count; i++)
            {
                if (string.IsNullOrEmpty(FirtextBoxList[i].Text))
                {
                    MessageBox.Show(s4); continue;
                }
                var test = new scancode
                {
                    ProductName = txtpname.Text,
                    ProductType = 1,
                    Grade = txtgrade.Text,
                    PartName = SectextBoxList[i].Text,
                    ScanPartCode = FirtextBoxList[i].Text,
                    checkDigit = 1
                };
                newDataPartNamelist.Add(SectextBoxList[i].Text);
                sdclist.Add(test);
            }
            if (sdclist.Count > 0)
            {
                if (SqlSugarHelper.ParaMetersUpOrInEntity(sdclist) > 0)
                {
                    MessageBox.Show(s1);
                    //数据保存更新成功后，清除多余数据
                    var partNameList = SqlSugarHelper.GetColumnList<scancode, string>(s => s.PartName, s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text);
                    foreach (var name in partNameList)
                    {
                        if (!newDataPartNamelist.Contains(name))
                        {
                            var deleteRes = SqlSugarHelper.DeleteByCondition<scancode>(s => s.ProductName == txtpname.Text && s.Grade == txtgrade.Text && s.PartName == name);
                            if (deleteRes > 0) Console.WriteLine($"扫描码数据记录清除旧数据完成 产品型号 - {txtpname.Text} - 档次号 - {txtgrade.Text} - 部件名称 - {name} -");
                            else Console.WriteLine($"数据记录清除失败");
                        }
                    }
                }
                else MessageBox.Show(s2);
            }

        }
        /// <summary>
        /// 用于开始生产时不启用批次码校验时，批次码比对方法
        /// </summary>
        /// <returns></returns>
        private void AddControlBox(string standardcode , string partname , string scancode)
        {
            if (alwaysVerification) { scancode = ""; }
            TextBox tb1 = new TextBox
            {
                Location = new System.Drawing.Point(27, 30 * txtCount + 48),
                Name = $"FtextBox{txtCount}",
                Size = new System.Drawing.Size(174, 26),
                Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                Text = scancode
            };


            TextBox tb2 = new TextBox
            {
                Width = 200,
                Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(210, 30 * txtCount + 48),
                Name = $"NtextBox{txtCount}",
                Text = partname,
                Size = new System.Drawing.Size(300, 19),
                ReadOnly = true,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                BackColor = panel3.BackColor
            };



            TextBox tb3 = new TextBox
            {
                Width = 200,
                Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(512, 30 * txtCount + 48),
                Name = $"StextBox{txtCount}",
                Text = standardcode,
                Size = new System.Drawing.Size(174, 19),
                ReadOnly = true,
                TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                BackColor = panel3.BackColor
            };


            if (tb3.Location.Y + tb3.Size.Height >= (panel3.Size.Height - 10))
            {
                if (panel3.Location.Y + panel3.Size.Height > (this.Height - 150))
                {
                    this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + (30 * txtCount + 70));
                }
                panel3.Size = new System.Drawing.Size(panel3.Size.Width, panel3.Size.Height + (30 * txtCount + 50));
            }
            // 加入 Panel 和列表
            panel3.Controls.Add(tb1);
            panel3.Controls.Add(tb2);
            panel3.Controls.Add(tb3);
            FirtextBoxList.Add(tb1);
            SectextBoxList.Add(tb2);
            ThitextBoxList.Add(tb3);
            txtCount++;
        }
        private void InitialTitle() {
            // 
            // labelStand
            // 
            var p1 = new System.Drawing.Point(456, 14);
            var p2 = new System.Drawing.Point(14, 14);
            if (currentCulture.Name == "zh-CN")
            {
                p1 = new System.Drawing.Point(550, 14);
                p2 = new System.Drawing.Point(64, 14);
            }
            Label lbl1 = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold),
                Location = p1,
                Name = "labelStand",
                Size = new System.Drawing.Size(92, 16),
                TabIndex = 5,
                Text = resManager.GetString("Fvsclabelstand"),
            };
            // 
            // labelScan
            // 
            Label lbl2 = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold),
                Location = p2,
                Name = "labelScan",
                Size = new System.Drawing.Size(92, 16),
                TabIndex = 4,
                Text = resManager.GetString("Fvsclabelscan"),
            };

            panel3.Controls.Add(lbl1);
            panel3.Controls.Add(lbl2);
        }
        public static Dictionary<string, string> LoadConfig(string filePath)
        {
            var defaultConfig = new Dictionary<string, string>()
            {
                { "Mode", "1" },
                { "StartIndex", "3" },
                { "CodeLength", "2" },
                { "Delimiter", "-" }
            };

            var resultConfig = new Dictionary<string, string>(defaultConfig); // 先复制默认

            if (!File.Exists(filePath))
            {
                // 文件不存在则创建默认
                using (var writer = new StreamWriter(filePath))
                {
                    foreach (var kv in defaultConfig)
                    {
                        writer.WriteLine($"{kv.Key}={kv.Value}");
                    }
                }
                return resultConfig;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#"))
                    continue;

                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    // 只更新已知的标准键，避免无效或拼错的键污染配置
                    if (defaultConfig.ContainsKey(key))
                    {
                        resultConfig[key] = value;
                    }
                    else
                    {
                        Console.WriteLine($"⚠️ 配置文件中存在未识别的键：{key}，已忽略。");
                    }
                }
            }

            return resultConfig;
        }

    }
}
