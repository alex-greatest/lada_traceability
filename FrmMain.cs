using DemoMessageBox;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Review.Model;
using Review.Repository;
using Review.Utils;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using System.Linq;
using Review.UI;
using Windows.UI.Xaml.Documents;
using Windows.Gaming.Preview.GamesEnumeration;

namespace Review
{
    public struct StationInfo
    {
        public Socket socket;
        public StationData sData;
        public MasterData mData;
        public string name;
    }
    public partial class FrmMain : Form
    {
        private ProductLine lineMCU = null;
        public static FrmNet frmNetWindow = null;
        public static FrmUser frmUserWindow = null;
        public static FrmStation frmStationWindow = null;
        public static FrmProduct frmProductWindow = null;
        public static FrmGroup frmGroupWindow = null;
        public static FrmRS frmRSWindow = null;
        public static FrmReview frmReviewWindow = null;
        public static FrmBarcode frmBarcodeWindow = null;
        public static FrmReviewData frmReviewDataWindow = null;
        public static FrmDisplay frmDisplayWindow = null;
        private string user;
        private int level;
        private string serverIP;
        private string serverPort;
        private Dictionary<string, string> allDicStation = new Dictionary<string, string>();               //所有的IP和端口信息
        private Dictionary<string, string> dicStation = new Dictionary<string, string>();                  //该生产工艺所用的工位的IP和端口信息
        private Dictionary<string, string> dicNStation = new Dictionary<string, string>();                 //该生产工艺不用的工位的IP和端口信息
        private Dictionary<string, StationData> dicUsedStationData = new Dictionary<string, StationData>();     //该生产工艺的工位数据信息
        private Dictionary<string, MasterData> dicUsedMasterData = new Dictionary<string, MasterData>();        //该生产工艺的追溯反馈信息
        private Dictionary<string, StationData> dicNUsedStationData = new Dictionary<string, StationData>();    //该生产工艺的工位数据信息
        private Dictionary<string, MasterData> dicNUsedMasterData = new Dictionary<string, MasterData>();       //该生产工艺的追溯反馈信息
        private MyTimer dealNetData;
        string s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12,s13,s14,s15,s16;
        /// <summary>
        /// //产品型号值
        /// </summary>
        public short productType;

        /// <summary>
        /// 生产时间
        /// </summary>
        string productiveTime;

        /// <summary>
        /// 看板点击
        /// </summary>
        int isClick = 0;

        /// <summary>
        /// 上一产品型号
        /// </summary>
        public string lastProduct = string.Empty;


        string s = string.Empty;
        string productName = string.Empty;
        DateTime dTime = new DateTime();
        string productGroup = string.Empty;
        string productPlan = string.Empty;
        string productNgText = string.Empty;
        string productInfo = string.Empty;
        string proCodeTemplate = "";

        private ResourceManager resManager;
        private CultureInfo currentCulture;
        public FrmMain(string user, int level)
        {
            InitializeComponent();  //  初始化窗体控件
            ReadIPAndPort();        //  读取所有的IP和端口号，包括服务器
            this.user = user;
            this.level = level;

            // 初始化资源管理器
            resManager = new ResourceManager("Review.Resources.Strings", typeof(FrmMain).Assembly);
            currentCulture = new CultureInfo("ru-RU");  // 默认使用当前文化设置

            // 设置初始语言
            UpdateLanguage(currentCulture);
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            if (currentCulture.Name == "zh-CN")
            {
                //label4.Location = new Point(428, 232);
                label3.Location = new Point(45, 101);
                //label2.Location = new Point(48, 215);
                //label6.Location = new Point(413, 169);
                label5.Location = new Point(46, 154);
                label7.Location = new Point(65, 209);
                label5.AutoSize = true;
                //label6.AutoSize = true;
                EnableVerification.Location = new Point(406, 23);
                alwaysVerification.Location = new Point(406, 59);


                s1 = "请先开始生产！";
                s2 = "提示";
                s3 = "用户权限不够!";
                s4 = "打印条码不能为空";
                s5 = "条码打印模板不能为空";
                s6 = "请选择要生产的型号";
                s7 = "选择文件";
                s8 = "标签文件";
                s9 = "用户取消了选择";
                s10 = "开始生产后才能查看数据";
                s11 = "产品代号为空，请先设置以免条码打印出问题";
                s12 = "请检查型号的标准码及扫描批次码是否正确设置";
                s13 = "当前产品型号的扫描码与标准码匹配失败，请重新扫描";
            }
            else if(currentCulture.Name == "ru-RU")
            {
                
                //label4.Location = new Point(13, 262);
                label3.Location = new Point(15, 101);
                //label2.Location = new Point(3, 215);
                //label6.Location = new Point(399, 148);
                label5.Location = new Point(12, 142);
                label7.Location = new Point(12, 209);

                label5.AutoSize = false;
                //label6.AutoSize = false;
                //label6.Size = new Size(169, 73);
                EnableVerification.Location = new Point(344, 23);
                alwaysVerification.Location = new Point(344, 59);

                s1 = "Пожалуйста, запустите производство";
                s2 = "Подсказка";
                s3 = "Недостаточно прав";
                s4 = "Штрих-код не может быть пустым";
                s5 = "Шаблон штрих-кода не может быть пустым";
                s6 = "Выберите какую модель производить";
                s7 = "Выбор файла";
                s8 = "Теговый файл";
                s9 = "Польз. отменил выбор";
                s10 = "Данные будут доступны после начала произв";
                s11 = "Код продукта пуст. Сначала задайте его, чтобы избежать проблем с печатью штрих-кода.";
                s12 = "Проверьте, корректно ли задан станд. номер и скан. номер партии для данной модели";
                s13 = "Отскан. номер и станд. номер не совпадают для данной моедли, отсканируйте корректный";
            }
            // 更新窗体文本
            this.Text = resManager.GetString("frmMain");
            buttonChangeLanguage.Text = resManager.GetString("buttonChangeLanguage");
            MenuSystem.Text = resManager.GetString("MenuSystem");
            MenuItemMin.Text = resManager.GetString("MenuItemMin");
            MenuProduct.Text = resManager.GetString("MenuProduct");
            MenuItemPerson.Text = "[" + resManager.GetString("MenuItemPerson") + "]";
            MenuItemProduct.Text = "[" + resManager.GetString("MenuItemProduct") + "]";
            MenuData.Text = resManager.GetString("MenuData");
            MenuItemReview.Text = "[" + resManager.GetString("MenuItemReview") + "]";
            MenuItemNet.Text = resManager.GetString("MenuItemNet");
            MenuBarcode.Text = resManager.GetString("MenuBarcode");
            MenuItemBarcode.Text = resManager.GetString("MenuItemBarcode");
            MenuItemReviewData.Text = "[" +resManager.GetString("MenuItemReviewData")+"]";
            groupBox.Text = resManager.GetString("groupBox");
            label1.Text = resManager.GetString("label1");
            label3.Text = resManager.GetString("label3");
            //label2.Text = resManager.GetString("label2");
            btnStart.Text = resManager.GetString("btnStart");
            btnStop.Text = resManager.GetString("btnStop");
            button1.Text = resManager.GetString("button1");
            label4.Text = resManager.GetString("label4");
            生产看板ToolStripMenuItem.Text = resManager.GetString("生产看板ToolStripMenuItem");
            button2.Text = resManager.GetString("Mlabel6");
            label5.Text = resManager.GetString("Mlabel5");
            label7.Text = resManager.GetString("Mlabel7");
            EnableVerification.Text = resManager.GetString("EnableVerification");
            button3.Text = resManager.GetString("MtoolStripMI");
            alwaysVerification.Text = resManager.GetString("alwaysVerification");
        }

        private void buttonChangeLanguage_Click(object sender, EventArgs e)
        {
            // 切换语言
            if (currentCulture.Name == "en-US" || currentCulture.Name == "zh-CN")
            {
                currentCulture = new CultureInfo("ru-RU");
            }
            else
            {
                currentCulture = new CultureInfo("zh-CN");
            }

            // 更新界面
            UpdateLanguage(currentCulture);
        }
        #region 读取所有的IP地址和端口号，不包括服务器
        private void ReadIPAndPort()
        {
            string sql = "select * from station";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                if (dr[1].ToString() == "server")
                {
                    serverIP = dr[2].ToString();
                    serverPort = dr[3].ToString();
                }
                else
                {
                    allDicStation.Add(dr[1].ToString(), dr[2].ToString() + ":" + dr[3].ToString());
                }
            }
            foreach (var ad in allDicStation)
            {
                MyTools.MyWrite(MyTools.GetMethodName(1) + "/  " + ad.Key + ":" + ad.Value);
            }
        }
        #endregion
        #region 向生产线列表内添加数据
        private void FillcomProduct()
        {
            comProduct.Items.Clear();

            string sql = "select * from Product";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                comProduct.Items.Add(dr[1]);
            }
        }
        private void initGradelist()
        {
            cbgrade.Items.Clear();
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
                            cbgrade.Items.Add(va);
                        }
                    }
                }
            }
        }
        #endregion
        #region 向班组列表内添加数据
        /*private void FillcomGroup()
        {
            string sql = "select * from Operator";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                if (comGroup.Items.Contains(dr[1]) == false)
                {
                    comGroup.Items.Add(dr[1]);
                    if (comGroup.Text.Length == 0)
                    {
                        comGroup.Text = dr[1].ToString();
                    }
                    //     Console.Write(dr[2] + "\r\n");
                }
            }
        }*/
        #endregion
        #region 配置该工艺用到的工位的名称以及IP地址和端口号
        private void ConfigIPAndPort()
        {
            List<string> list = DBHelper.selectStationList(productType , comProduct.Text , 3) ;
            string stationip;
            dicStation.Clear();
            foreach (string dic in list)
            {
                stationip = allDicStation[dic];
                dicStation.Add(dic, stationip);
            }
        }
        #endregion
        #region 配置生产线涉及到工位上的附件条码
        
        #endregion
        #region 配置生产线涉及到工位上的run标志位,以及使能标志
        private void ConfigLineFlag()
        {
            //
            foreach (var ds in dicUsedMasterData)
            {
                if (btnStart.Enabled)
                {
                    ds.Value.heart = false;
                    ds.Value.run = false;
                    ds.Value.enable = false;
                }
                else
                {
                    ds.Value.run = true;
                    ds.Value.enable = true;
                    if (ds.Value.heart)
                    {
                        ds.Value.heart = false;
                    }
                    else
                    {
                        ds.Value.heart = true;
                    }
                }
            }
            foreach (var ds in dicNUsedMasterData)
            {
                if (btnStart.Enabled)
                {
                    ds.Value.heart = false;
                    ds.Value.run = false;
                    ds.Value.enable = false;
                }
                else
                {
                    ds.Value.run = true;
                    ds.Value.enable = true;
                    if (ds.Value.heart)
                    {
                        ds.Value.heart = false;
                    }
                    else
                    {
                        ds.Value.heart = true;
                    }
                }
            }
            // MyTools.ProgrameLog(MyTools.GetMethodName(1));
        }
        #endregion

        private void TechnologyDataConfig()
        {
            ConfigIPAndPort();

            lineMCU = new ProductLine(IPAddress.Parse(serverIP), Int32.Parse(serverPort), dicStation, allDicStation, dicUsedStationData, dicUsedMasterData, dicNUsedStationData, dicNUsedMasterData);

            ConfigLineFlag();
        }
        private void MinWindows()
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void ShowWindow(int level, string windowName)
        {
            bool logOK = false;
            if (level < 1)
            {
                FrmLog frmLog = new FrmLog();
                frmLog.ShowDialog();
                if (frmLog.LoginFlag)
                {
                    if (frmLog.level > 1)
                    {
                        logOK = true;
                    }
                    else
                    {
                        MessageBox.Show(this, s3, s2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                logOK = true;
            }
            if (logOK == true)
            {
                if (windowName == "Person")
                {
                    if (frmUserWindow == null)
                    {
                        frmUserWindow = new FrmUser(this.resManager, this.currentCulture);
                        frmUserWindow.Show();
                    }
                    else
                    {
                        frmUserWindow.Activate();
                    }
                }
                else if (windowName == "Station")
                {
                    if (frmStationWindow == null)
                    {
                        frmStationWindow = new FrmStation();
                        frmStationWindow.Show();
                    }
                    else
                    {
                        frmStationWindow.Activate();
                    }
                }
                else if (windowName == "Product")
                {
                    if (frmProductWindow == null)
                    {
                        frmProductWindow = new FrmProduct(this.resManager , this.currentCulture);
                        frmProductWindow.Show();
                    }
                    else
                    {
                        frmProductWindow.Activate();
                    }
                }
                else if (windowName == "Group")
                {
                    if (frmGroupWindow == null)
                    {
                        frmGroupWindow = new FrmGroup();
                        frmGroupWindow.Show();
                    }
                    else
                    {
                        frmGroupWindow.Activate();
                    }
                }
                else if (windowName == "RS")
                {
                    if (frmRSWindow == null)
                    {
                        frmRSWindow = new FrmRS();
                        frmRSWindow.Show();
                    }
                    else
                    {
                        frmRSWindow.Activate();
                    }
                }
                else if (windowName == "Review")
                {
                    if (frmReviewWindow == null)
                    {
                        frmReviewWindow = new FrmReview(this.resManager, this.currentCulture);
                        frmReviewWindow.Show();
                    }
                    else
                    {
                        frmReviewWindow.Activate();
                    }
                }
                else if (windowName == "Barcode")
                {
                    if (frmBarcodeWindow == null)
                    {
                        frmBarcodeWindow = new FrmBarcode();
                        frmBarcodeWindow.Show();
                    }
                    else
                    {
                        frmBarcodeWindow.Activate();
                    }
                }
                else if (windowName == "ReviewData")
                {
                    if (frmReviewDataWindow == null)
                    {
                        frmReviewDataWindow = new FrmReviewData(this.resManager, this.currentCulture);
                        frmReviewDataWindow.Show();
                    }
                    else
                    {
                        frmReviewDataWindow.Activate();
                    }
                }
            }
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (dealNetData == null)
            {
                groupBox.Enabled = true;
                button1.Enabled = true;
                //dealNetData.Stop();
                SqlSugarHelper.UpdateByCondition<scancode>(s => s.Id != -1 , s => new scancode { checkDigit = -1});
            }
            else { MessageBox.Show(s1, s2); }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /*if (proCodeText.Text == "") {
                MessageBox.Show(s4 , s2);
                return;
            }
            else*/
            {
                var printCodeList = DBHelper.selectPrintCode(comProduct.Text);
                DBHelper.updatePrintCode(utils.GenerateNextCode(printCodeList[8], true),comProduct.Text);
            }
            if (proTemplate.Text == "") {
                MessageBox.Show(s5 , s2);
                return;
            }
            if (comProduct.Text.Length == 0 || cbgrade.Text.Length == 0)
            {
                MessageBox.Show(s6, s2);
                return;
            }
            if (EnableVerification.Checked)
            {
                bool checkresult = CodeVerification(alwaysVerification.Checked ? -1 : -2);

                if (!checkresult)
                {
                    MessageBox.Show(s13, s2);
                    return;
                }
            }
            /*if (textBox1.Text == "") {
                MessageBox.Show(s11 , s2);
                return;
            }*/
            //insertOrUpdateProTyep();
            //setPrintCode();
            if (dealNetData == null)
            {
                if (MyTools.isPortAvalaible(Int32.Parse(serverPort)) == false)
                {
                    LogFile.Errorlog.Error($"通讯端口{Int32.Parse(serverPort)}" + "已占用，请卸载相关软件");
                    return;
                }
                getProductType();
                TechnologyDataConfig();
                dealNetData = new MyTimer(dicUsedStationData, dicNUsedStationData, dicUsedMasterData, dicNUsedMasterData, comProduct.Text, productType, proCodeTemplate);
                dealNetData.Start();
            }
            else
            {
                getProductType();
                dealNetData.changeProductionName(productType, comProduct.Text);
                dealNetData.Start();
            }
            //dealNetData?.iniialDicconfig(LoadConfig2() , cbgrade.Text);
            groupBox.Enabled = false;
            btnStart.Enabled = false;
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            FillcomProduct();
            initGradelist();
        }
        
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //线程终止标识,
            MessageBoxHelper.IsWorking = false;
        }

        /// <summary>
        /// //获取产品型号对应的值
        /// </summary>
        private void getProductType()
        {
            string productName = this.comProduct.Text.Trim();

            string sql = "select ProductID from Product where productName='" + productName + "'";
            try
            {
                productType = Convert.ToInt16(DBHelper.ExecuteScalar(sql, 0, null));
            }
            catch (Exception ex)
            {
                LogFile.Errorlog.Error("getProductType():" + ex.Message);
            }


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            toolStatusInfo.Text = "※软件:质量追溯系统";
            toolStatusUser.Text = "  ※用户:" + user;
            toolStatusDevelop.Text = "  ※开发:竹尔机器人";
            toolStatusNow.Text = "  ※时间:" + DateTime.Now.ToString();
            ConfigLineFlag();
        }

        #region 其他窗口显示程序
        private void MenuItemNet_Click(object sender, EventArgs e)
        {
            if (btnStart.Enabled == false)
            {
                if (frmNetWindow == null)
                {
                    //frmNetWindow = new FrmNet(dicUsedStationData, dicUsedMasterData);
                    frmNetWindow = new FrmNet(dicUsedStationData, dicUsedMasterData, dicNUsedStationData, dicNUsedMasterData);
                    frmNetWindow.Show();
                }
                else
                {
                    frmNetWindow.Activate();
                }
            }
            else
            {
                MessageBox.Show(this, s10, s2, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void MenuItemMin_Click(object sender, EventArgs e)
        {
            MinWindows();
        }

        private void MenuItemPerson_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "Person");
        }

        private void MenuItemStation_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "Station");
        }

        private void MenuItemProduct_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "Product");
        }

        private void MenuItemRS232_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "RS");
        }

        private void MenuItemReview_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "Review");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            new FrmSetPrintCode(this.currentCulture, this.resManager).ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comProduct.Text) || string.IsNullOrEmpty(cbgrade.Text))
            {
                MessageBox.Show("请先选择产品型号及档次");
                return;
            }
            new FrmVerifySCode(this.resManager, this.currentCulture , comProduct.Text , cbgrade.Text , alwaysVerification.Checked).ShowDialog();
        }

        

        private void EnableVerification_CheckedChanged(object sender, EventArgs e)
        {
            if (!EnableVerification.Checked) { alwaysVerification.Checked = false; }
        }

        private void alwaysVerification_CheckedChanged(object sender, EventArgs e)
        {
            if (alwaysVerification.Checked) { EnableVerification.Checked = true;  }
        }

        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FormGradeNumber(this.resManager , this.currentCulture).ShowDialog();
        }

        private void comProduct_Click(object sender, EventArgs e)
        {
            FillcomProduct();
        }

        private void cbgrade_Click(object sender, EventArgs e)
        {
            initGradelist();
        }

        private void MenuItemBarcode_Click(object sender, EventArgs e)
        {
            new FrmStandCode(this.resManager ,this.currentCulture).ShowDialog();
        }

        private void MenuItemReviewData_Click(object sender, EventArgs e)
        {
            ShowWindow(level, "ReviewData");
        }
        #endregion

        private void 生产看板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(plan.Text)) {
                new FrmDisplay(comProduct.Text ,cbgrade.Text, user, plan.Text, "").Show();
            }
        }

        Thread tr = null;
        List<string> zplList = new List<string>();
        private void printCode(List<string> printcode)
        {
            string printerIp = "192.168.1.45";
            string filePath = proTemplate.Text;
            string filePath2 = "D:\\Marking Lable Template\\11.prn";
            zplList.Clear();
            try
            {
                // 使用 StreamReader 从文本文件中读取值
                using (StreamReader Stream = new StreamReader(filePath))
                {
                    int lineNumber = 1;
                    string line;

                    while ((line = Stream.ReadLine()) != null)
                    {
                        string modifiedZpl = line.Replace("^FD" + printcode[2] + "^FS", $"^FD{printcode[6]}^FS");
                        //modifiedZpl = modifiedZpl.Replace("^FD"+ printcode[3] + "^FS", $"^FD{printcode[7]}^FS");
                        modifiedZpl = modifiedZpl.Replace("^FD" + printcode[4] + "^FS", $"^FD{printcode[7] + " " + printcode[8]}^FS");
                        modifiedZpl = modifiedZpl.Replace("^FD," + printcode[5] + "^FS", $"^FD,{printcode[6] + " " + printcode[7] + " " + printcode[8]}^FS");

                        lineNumber++;
                        zplList.Add(modifiedZpl);
                        Console.WriteLine(modifiedZpl);
                    }
                    //Stream.Dispose();
                }
                using (StreamWriter sw = new StreamWriter(filePath2))
                {
                    foreach (var kvp in zplList)
                    {
                        // 写入字典的值（不包括键）
                        sw.WriteLine(kvp);
                    }
                }


                Connection connection = new TcpConnection(printerIp, TcpConnection.DEFAULT_ZPL_TCP_PORT);
                connection.Open();
                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(connection);
                printer.SendFileContents(filePath2);

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending ZPL to printer: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // 创建 OpenFileDialog 实例
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置对话框的标题
            openFileDialog.Title = s7;

            // 设置初始目录（可选）
            openFileDialog.InitialDirectory = @"D:\";

            // 设置文件过滤器（可选）
            openFileDialog.Filter = s8+ " (*.prn)|*.prn";

            // 允许选择多个文件（可选）
            openFileDialog.Multiselect = false;

            // 显示对话框并检查用户是否点击了“确定”
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string selectedFilePath = openFileDialog.SafeFileName;

                // 输出文件路径
                proTemplate.Text = selectedFilePath;
                proCodeTemplate = openFileDialog.FileName;
            }
            else
            {
                LogFile.Operationlog.Info(s9);
            }
        }
        private void comProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            proCodeText.Text = "";

            try
            {
                var printCode = DBHelper.selectPrintCode(comProduct.Text);

                if (printCode != null)
                {
                    proCodeText.Text = printCode[8];

                    //textBox1.Text = printCode[1];
                }
            }
            catch (MySqlException ex) { MessageBox.Show($"数据库异常   {ex}"); }
        }
        private void label7_Click(object sender, EventArgs e)
        {
            //找到轴杆条码对应的  打印条码校验位
            Review.Utils.AppContext.IncrementProductCount(true);
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Review.Utils.AppContext.IncrementProductCount(false);
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Review.Utils.AppContext.Total.Reset();
        }
        #region 批次码校验更新代码
        public static Dictionary<string, string> LoadConfig(string filePath)
        {
            LogFile.Operationlog.Info($"执行读取配置文件操作  ----  {filePath}  -----");
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
                        LogFile.Errorlog.Error($"⚠️ 配置文件中存在未识别的键：{key}，已忽略。");
                    }
                }
            }
            foreach (var kvp in resultConfig) {
                LogFile.Operationlog.Info($"配置文件  -  键 ：{kvp.Key}  --  值 ：{kvp.Value}");
            }
            return resultConfig;
        }
        /*public static Dictionary<string, string> LoadConfig2()
        {
            
        }*/
        private bool CodeVerification(int selectMode)
        {
            var sdcList = SqlSugarHelper.GetDataList<standardcode>(s => s.ProductName == comProduct.Text && s.Grade == cbgrade.Text);
            var scancList = SqlSugarHelper.GetDataList<scancode>(s => s.ProductName == comProduct.Text && s.Grade == cbgrade.Text && s.checkDigit != selectMode);
            if (sdcList.Count < 1 || sdcList.Count != scancList.Count) {
                MessageBox.Show(s12 , s2);
                return false;
            }
            foreach (var std in sdcList)
            {
                var match = scancList.FirstOrDefault(scan => scan.PartName == std.PartName);
                if (match == null)
                {
                    LogFile.NGPROlog.Error($"//反馈数据部件名称比对失败  部件名  {std.PartName}");
                    return false;
                }
                if (match.ScanPartCode != std.PartCode)
                {
                    LogFile.NGPROlog.Error($"//反馈扫描码与标准码比对失败  部件条码  {std.PartCode}");
                    return false;
                }
            }

            LogFile.Operationlog.Info("//循环正确比对完成   反馈比对合格");
            return true;
        }
        #endregion
    }
}
