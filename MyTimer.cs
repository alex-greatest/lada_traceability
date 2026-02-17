using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Bcpg.Sig;
using Review.Repository;
using Review.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace Review
{
    class MyTimer
    {
        /// <summary>
        /// 用于指定无限期等待一段时间，.Net 4.5及以上可以使用Timeout.InfiniteTimeSpan
        /// </summary>
        private static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);
        private readonly Timer timer;
        private static readonly object[] obj = new object[200];
        private static readonly object[] obj2 = new object[200];
        private static readonly object objPrint = new object();
        public static bool setPrint;
        public static int printCount = 0;
        private string productName;
        private bool[] haveSave = new bool[200];
        private bool[] Execution = new bool [200];
        string grade;
        private string[] finalPrintCode = new string[200];
        private Dictionary<string, StationData> dicUsedStationData = new Dictionary<string, StationData>();     //该生产工艺的工位数据信息
        private Dictionary<string, StationData> dicNUsedStationData = new Dictionary<string, StationData>();
        private Dictionary<string, MasterData> dicUsedMasterData = new Dictionary<string, MasterData>();        //该生产工艺的追溯反馈信息
        private Dictionary<string, MasterData> dicNUsedMasterData = new Dictionary<string, MasterData>();
        private Dictionary<string, string> dicConfig = new Dictionary<string, string>();

        /// <summary>
        /// //产品型号值
        /// </summary>
        public short productType;
        /// <summary>
        /// 生产时间
        /// </summary>
        public string proCodeTemplateFileName;
        HttpToSound play;


        //=================添加productType属性======================
        public MyTimer(Dictionary<string, StationData> dicUsedStationData, Dictionary<string, StationData> dicNUsedStationData, Dictionary<string, MasterData> dicUsedMasterData, Dictionary<string, MasterData> dicNUsedMasterData, string productName, short _productType, string proCodeTemplateFileName)
        {
            this.dicUsedStationData = dicUsedStationData;
            this.dicNUsedMasterData = dicNUsedMasterData;
            this.dicNUsedStationData = dicNUsedStationData;
            this.dicUsedMasterData = dicUsedMasterData;
            this.productName = productName;
            this.productType = _productType;
            this.proCodeTemplateFileName = proCodeTemplateFileName;
            //从main界面传入起始Id
            for (int i = 0; i < 200; i++) {
                obj[i] = obj2[i] = new object();
            }

            //创建定时器
            timer = new Timer(TimerFunc, null, InfiniteTimeSpan, InfiniteTimeSpan);
        }

        public void changeProductionName(short productType, string productionName)
        {
            this.productType = productType;
            this.productName = productionName;
        }
        public void iniialDicconfig(Dictionary<string, string> _dicConfig , string _grade) {
            this.dicConfig = _dicConfig;
            this.grade= _grade;
        }
        public void Start()
        {
            //启动定时器
            timer.Change(TimeSpan.FromMilliseconds(200), TimeSpan.FromMilliseconds(200));
        }

        public void Stop()
        {
            //停止计时器
            timer?.Change(InfiniteTimeSpan, InfiniteTimeSpan);
        }
        private void TimerFunc(object state)
        {
            DealNetUsedData(dicUsedStationData);
            DealNetDownLineUsedData(dicNUsedStationData);
        }
        List<string> printCodeList;
        private void DealNetUsedData(Dictionary<string, StationData> UStationData)
        {
            string stationName;
            int stationOrder;
            string productCode;
            foreach (var stationData in UStationData.Values)
            {
                stationName = stationData.stationName;
                productCode = stationData.productCode;
                
                if (stationName.Length == 4)
                {
                    stationOrder = Convert.ToInt32(stationName.Substring(2, 2));
                }
                else {
                    stationOrder = Convert.ToInt32(stationName.Substring(2, 3));
                }
                dicUsedMasterData[stationName].type = productType;
                //信号为1查询产品是否为返修,  （查询数据库 该条码的数据记录如有合格记录，不允许上线，如有不合格记录则可返修）
                //如果已经反馈信号则不再查询
                if (stationData.status == 1 && !Execution[stationOrder] && dicUsedMasterData[stationName].feedBack == 0)
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;
                    //产品总记录表结果
                    //var result = DBHelper.selectProResult(productName, productType, productCode, "OP30", "barcode3");
                    var pro = SqlSugarHelper.selectPro(productName , productCode);
                    var result = 0;
                    var LSTName = "";
                    if (pro.Count == 1) { result = pro[0].productResult; LSTName = pro[0].stationName; }
                    //var LSTName = DBHelper.selectStationName(productName, productType, productCode, "OP30", "barcode3");
                    if (result == 1)
                    {
                        if (stationData.firstStation)
                        {
                            //该产品已有合格记录
                            dicUsedMasterData[stationName].feedBack = 91;
                        }
                        else if (stationName.Equals("OP40"))
                        {
                            string pCode = DBHelper.selectPCode("OP30", "barcode3", productCode);
                            if (pCode == null)
                            {
                                pCode = DBHelper.selectPCode("OP20", "barcode4", productCode);
                            }
                            //找到轴杆条码对应的  打印条码校验位
                            var scanCode = DBHelper.selectScanCode(pCode, productName, productType);

                            //scanCode 为null 代表final表没有记录，这是一个新的产品
                            if (scanCode == null)
                            {
                                //查看是否为OP30_ST2 的合格记录
                                dicUsedMasterData[stationName].feedBack = (short)(LSTName == "OP30_ST2" ? 1 : 94);  //94当前工位的前置工位，无记录
                            }
                            else if (scanCode == "")
                            {
                                dicUsedMasterData[stationName].feedBack = 41;  //反馈plc 表示该产品等待执行条码校验
                            }
                        }
                        else
                        {
                            try
                            {
                                //查看是否为前置工位的合格记录
                                int Order = LSTName.Length == 4 ? Convert.ToInt32(LSTName.Substring(2, 2)) : Convert.ToInt32(LSTName.Substring(2, 3));

                                if (LSTName == stationData.lastStationName)
                                {
                                    dicUsedMasterData[stationName].feedBack = 1;

                                }
                                else
                                {
                                    dicUsedMasterData[stationName].feedBack = (short)(Order > stationOrder ? 93 : 94);//93当前产品后续工位有合格记录   94当前工位的前置工位，无记录

                                }
                            }
                            catch (ArgumentOutOfRangeException ex) {
                                LogFile.Errorlog.Error($"信号执行  工位名称长度有问题  {productCode}   {ex}");
                            }
                        }
                    }
                    else if (result == 2 && LSTName == stationData.lastStationName)
                    {
                        //该产品上一工位不合格，不允许工作
                        dicUsedMasterData[stationName].feedBack = 92; //上一工位不合格，不允许工作
                    }
                    else
                    {
                        if (stationData.firstStation || stationName.Equals("OP50"))
                        {
                            dicUsedMasterData[stationName].feedBack = 1;
                        }
                        else {
                            dicUsedMasterData[stationName].feedBack = -1;
                        }
                    }
                    Execution[stationOrder] = false;
                }
                //信号为2将产品数据存储
                else if (stationData.status == 2 && !Execution[stationOrder] && !haveSave[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;
                    if (stationData.firstStation || stationName.Equals("OP50"))
                    {
                        DBHelper.insertStationProcode(stationName, productName, productType, productCode);
                    }
                    else
                    {
                        if (stationName.Equals("OP40"))
                        {
                            printCodeList = DBHelper.selectPrintCode(productName);

                            //当前产品流程最后一个工位，保存打印条码，产品结果
                            DBHelper.saveToFinalTable(stationData.quality, stationData.productCode, printCodeList[8], productName, productType);

                            if (stationData.quality != 2)
                            {
                                printCode(printCodeList);
                            }
                            else
                            {
                                Console.WriteLine("打印条码为空");
                            }
                        }
                        if (DBHelper.updateProCode(productType, productCode, stationData.quality, productName, stationName) == 0)
                        {
                            string pCode = DBHelper.selectPCode("OP30", "barcode3", productCode);
                            if (DBHelper.updateProCode(productType, pCode, stationData.quality, productName, stationName) == 0)
                            {
                                pCode = DBHelper.selectPCode("OP20", "barcode4", productCode);
                                DBHelper.updateProCode(productType, pCode, stationData.quality, productName, stationName);
                            }
                            stationData.productCode = pCode;
                        }
                    }

                    DBHelper.SaveStationData(stationData, productType, productName);
                    haveSave[stationOrder] = true;
                    Execution[stationOrder] = false;
                    dicUsedMasterData[stationName].feedBack = 2;
                }
                //信号为3将产品条码绑定RFID
                else if (stationData.status == 3 && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;

                    if (productCode == "")
                    {
                        dicUsedMasterData[stationName].feedBack = 71;  //绑定RFID时条码为空反馈71
                    }
                    else if (stationData.RFID == "")
                    {

                        dicUsedMasterData[stationName].feedBack = 72;  //绑定RFID时  RFID为空  反馈72
                    }
                    else
                    {
                        int result = DBHelper.RFIDBind(productType, productCode, stationData.RFID, stationName);

                        dicUsedMasterData[stationName].feedBack = 2;
                    }
                    Console.WriteLine(" 时间   " + DateTime.Now + "   工位   " + stationName + "   条码  " + stationData.productCode + "  RFID  " + stationData.RFID);
                }
                //信号为4将当前条码发送至plc，解绑RFID与产品条码
                else if (stationData.status == 4 && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;

                    string RFID = stationData.RFID;
                    string backProCode = DBHelper.SelectRFIDBindingCode(productType, RFID, stationName);
                    if (backProCode == "" || backProCode == null)
                    {
                        dicUsedMasterData[stationName].feedBack = 73;  //解绑时  RFID上条码为空  反馈73
                    }
                    else if (stationData.RFID == "")
                    {

                        dicUsedMasterData[stationName].feedBack = 74;  //解绑时  RFID为空  反馈74
                    }
                    else
                    {
                        dicUsedMasterData[stationName].productCode = backProCode;
                        DBHelper.clearRFIDBind(backProCode, productType, RFID, stationName);
                        dicUsedMasterData[stationName].feedBack = 2;
                    }
                }
                //信号为21 工位OP20_ST1 查询产品合格状态
                //信号为23 工位OP20_ST2 查询产品合格状态
                //信号为31 工位OP30_ST1 查询产品合格状态
                //信号为33 工位OP30_ST2 查询产品合格状态
                else if ((stationData.status == 21 || stationData.status == 31) && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;
                    //产品总记录表结果
                    int result = 0;

                    result = DBHelper.selectProResult(productName, productType, productCode);

                    var LSTName = DBHelper.selectStationName(productName, productType, productCode, "OP20", "barcode4");
                    var Order = LSTName.Length == 4 ? Convert.ToInt32(LSTName.Substring(2, 2)) : Convert.ToInt32(LSTName.Substring(2, 2));

                    if (result == 1)
                    {
                        if (stationData.status == 21)
                        {
                            if (LSTName == "OP10")
                            {
                                dicUsedMasterData[stationName].leftOrRight = 1;
                                dicUsedMasterData[stationName].feedBack = 1;
                            }
                            else if (LSTName == "OP20_ST1")
                            {
                                dicUsedMasterData[stationName].leftOrRight = 2;
                                dicUsedMasterData[stationName].feedBack = 1;
                            }
                            else
                            {
                                dicUsedMasterData[stationName].feedBack = (short)(Order >= stationOrder ? 93 : 94);//93该产品当前工位/后续工位有合格记录   94当前工位的前置工位，无记录
                            }
                        }
                        if (stationData.status == 31)
                        {
                            if (LSTName == "OP20_ST2")
                            {
                                dicUsedMasterData[stationName].leftOrRight = 1;
                                dicUsedMasterData[stationName].feedBack = 1;

                            }
                            else if (LSTName == "OP30_ST1")
                            {
                                dicUsedMasterData[stationName].leftOrRight = 2;
                                dicUsedMasterData[stationName].feedBack = 1;
                            }
                            else
                            {
                                dicUsedMasterData[stationName].feedBack = (short)(Order >= stationOrder ? 93 : 94);//93当前产品后续工位有合格记录   94当前工位的前置工位，无记录
                            }
                        }
                    }
                    else if (result == 2)
                    {
                        if (stationData.status == 21 && (LSTName == "OP10" || LSTName == "OP20_ST1"))
                        {
                            dicUsedMasterData[stationName].feedBack = 92; //上一工位不合格，不允许工作
                        }
                        else if (stationData.status == 31 && (LSTName == "OP20_ST2" || LSTName == "OP30_ST1"))
                        {
                            dicUsedMasterData[stationName].feedBack = 92; //上一工位不合格，不允许工作
                        }
                        else
                        {
                            dicUsedMasterData[stationName].feedBack = (short)(Order > stationOrder ? 94 : 1);
                        }

                    }
                    else
                    {
                        dicUsedMasterData[stationName].feedBack = 94;
                    }
                    Execution[stationOrder] = false;
                }
                //信号为22 工位OP20_ST1 存储产品数据 更新总表状态
                //信号为24 工位OP20_ST2 存储产品数据 更新总表状态
                //信号为32 工位OP30_ST1 存储产品数据 更新总表状态
                //信号为34 工位OP30_ST2 存储产品数据 更新总表状态
                else if ((stationData.status == 22 || stationData.status == 24 || stationData.status == 32 || stationData.status == 34) && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;
                    string STName = null;
                    if (stationData.status == 32 || stationData.status == 34)
                    {
                        string pCode = DBHelper.selectStationProcode(stationData.productCode, productName, productType);
                        if (pCode == null)
                        {
                            stationData.productCode = DBHelper.selectPCode("op20", "barcode4", stationData.productCode);
                        }
                    }
                    switch (stationData.status)
                    {
                        case 22:
                            STName = "OP20_ST1";
                            DBHelper.SaveStationData(stationData, productType, productName);
                            break;
                        case 24:
                            STName = "OP20_ST2";
                            DBHelper.updateStationData_20(stationData, productType, productName);
                            break;
                        case 32:
                            STName = "OP30_ST1";
                            DBHelper.SaveStationData(stationData, productType, productName);
                            break;
                        case 34:
                            STName = "OP30_ST2";
                            DBHelper.updateStationData_30(stationData, productType, productName);
                            break;
                    }
                    if (DBHelper.updateProCode(productType, productCode, stationData.quality, productName, STName) == 0)
                    {
                        string pCode = DBHelper.selectPCode("OP20", "barcode4", productCode);
                        DBHelper.updateProCode(productType, pCode, stationData.quality, productName, STName);
                    }

                    haveSave[stationOrder] = true;
                    dicUsedMasterData[stationName].feedBack = 2;
                }
                //信号为41  更新final表的scancode 用于打印条码最终校验
                else if (stationData.status == 41 && !Execution[stationOrder])
                {
                    Execution[stationOrder] = true;
                    string printCode = DBHelper.selectFinalPrintCode(productName, productType, stationData.productCode);
                    if (printCode == stationData.productCode)
                    {
                        string newCode = utils.GenerateNextCode(printCodeList[8], false);
                        DBHelper.updatePrintCode(newCode , productName);
                        DBHelper.updateScanCode(stationData.productCode, productName, productType);
                        dicUsedMasterData[stationName].feedBack = 2;

                    }
                    else
                    {
                        dicUsedMasterData[stationName].feedBack = 3;
                    }

                }

                
                //无信号交互的常态
                else if (stationData.status == 0)
                {
                    if (stationData.stationName == "OP40") {
                        FrmDisplay.setprocount(stationData.result[18] , stationData.result[19]);
                    }

                    //置状态为等待执行
                    Execution[stationOrder] = false;

                    haveSave[stationOrder] = false;

                    dicUsedMasterData[stationName].feedBack = 0;
                }
                //信号为88脱离追溯将产品数据存储
                lock (obj[stationOrder])
                {
                    if (stationData.by2 == 88 && !Execution[stationOrder] && !haveSave[stationOrder])
                    {
                        //置状态为执行中
                        Execution[stationOrder] = true;
                        DBHelper.SaveStationData(stationData, productType, productName);
                        haveSave[stationOrder] = true;
                        Execution[stationOrder] = false;
                        dicUsedMasterData[stationName].feedBack = 2;
                        Thread.Sleep(3000);
                    }
                }
                lock (obj2[stationOrder])
                {
                    if (stationData.by2 == 89 && !haveSave[stationOrder])
                    {
                        DBHelper.SaveStationData(stationData, productType, productName);
                        haveSave[stationOrder] = true;
                        Execution[stationOrder] = false;
                        dicUsedMasterData[stationName].feedBack = 2;
                        Thread.Sleep(3000);
                    }

                }
                lock (objPrint)
                {
                    if (stationData.by2 == 42 && !setPrint)
                    {
                        setPrint = true;
                        printCodeList = DBHelper.selectPrintCode(productName);
                        printCode(printCodeList);

                        string newCode = utils.GenerateNextCode(printCodeList[8], false);
                        DBHelper.updatePrintCode(newCode , productName);
                        dicUsedMasterData[stationName].byI2 = 42;
                        Thread.Sleep(3000);
                    }
                }
                if (stationData.by2 == 0)
                {
                    setPrint = false;
                    dicUsedMasterData[stationName].byI2 = 0;
                }
            }
            
        }
        private void DealNetDownLineUsedData(Dictionary<string, StationData> DownLineStationData)
        {
            string stationName;
            int stationOrder;
            string productCode;
            foreach (var stationData in DownLineStationData.Values)
            {
                stationName = stationData.stationName;
                productCode = stationData.productCode;
                if (stationName.Length == 4)
                {
                    stationOrder = Convert.ToInt32(stationName.Substring(2, 2));
                }
                else
                {
                    stationOrder = Convert.ToInt32(stationName.Substring(2, 3));
                }
                dicNUsedMasterData[stationName].type = productType;
                //信号为1查询产品是否为返修,  （查询数据库 该条码的数据记录如有合格记录，不允许上线，如有不合格记录则可返修）
                if (stationData.status == 1 && !haveSave[stationOrder])
                {
                    haveSave[stationOrder] = true;
                    if (stationData.stationName.Equals("OP90")) {
                        dicConfig.TryGetValue("Mode", out var modeType);
                        dicConfig.TryGetValue("StartIndex", out var startindex);
                        dicConfig.TryGetValue("CodeLength", out var codelength);
                        dicConfig.TryGetValue("Delimiter", out var delimiter);
                        switch (modeType) {
                            case "1":
                                var codelist = stationData.productCode.Split(Char.Parse(delimiter));
                                for (int i = 0; i < codelist.Length; i++)
                                {
                                    if (codelist[i].Length == int.Parse(codelength))
                                    {
                                        productCode = i == 0 ? codelist[1] : codelist[0];
                                        dicNUsedMasterData[stationName].feedBack = (short)(codelist[i].Equals(this.grade) ? 1 : -1); // -1 暂且定为档次号不匹配反馈信号
                                        break;
                                    }
                                    else if(i == codelist.Length - 1)
                                    {
                                        //表示上传的条码中并没有找到与配置文件中相匹配的   档次号信息
                                        dicNUsedMasterData[stationName].feedBack = -2;
                                    }
                                }
                                break;
                            case "2":
                                var si = int.Parse(startindex) == 0 ? 1 : int.Parse(startindex);
                                var nowgrade = productCode.Substring(si , int.Parse(codelength));
                                dicNUsedMasterData[stationName].feedBack = (short)(nowgrade.Equals(this.grade) ? 1 : -1);
                                break;
                            default:
                                Console.WriteLine("配置文件中的Mode输入有误，无匹配方式");
                                break;
                        }
                    }

                    int result = 0;
                    var resultList = DBHelper.checkSingleStation(stationData.stationName,productCode);
                    foreach (var i in resultList) {
                        if (i == "1") {
                            result = 1;
                        }
                    }
                    if (result != 1)
                    {
                        DBHelper.SaveStationData(stationData, productType, productName);

                        dicNUsedMasterData[stationName].feedBack = 1;
                    }
                    else { 
                        dicNUsedMasterData[stationName].feedBack = 93;//该产品当前工位/后续工位已有合格记录
                    }
                }
                else if (stationData.status == 2 && !haveSave[stationOrder])
                {
                    DBHelper.updateStationData(stationData, productType, productName);
                    haveSave[stationOrder] = true;
                    dicNUsedMasterData[stationName].feedBack = 2;
                }
                //信号为3将产品条码绑定RFID
                else if (stationData.status == 3 && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;

                    if (productCode == "")
                    {
                        dicNUsedMasterData[stationName].feedBack = 71;  //绑定RFID时条码为空反馈71
                    }
                    else if (stationData.RFID == "")
                    {

                        dicNUsedMasterData[stationName].feedBack = 72;  //绑定RFID时  RFID为空  反馈72
                    }
                    else
                    {
                        int result = DBHelper.RFIDBind(productType, productCode, stationData.RFID, stationName);

                        dicNUsedMasterData[stationName].feedBack = (short)(result == 1 ? 2 : 77); //77 表示绑定RFID时更新了不止一条数据 
                    }
                    Console.WriteLine(" 时间   " + DateTime.Now + "   工位   " + stationName + "   条码  " + stationData.productCode + "  RFID  " + stationData.RFID);
                }
                //信号为4将当前条码发送至plc，解绑RFID与产品条码
                else if (stationData.status == 4 && !Execution[stationOrder])
                {
                    //置状态为执行中
                    Execution[stationOrder] = true;

                    string RFID = stationData.RFID;
                    string backProCode = DBHelper.SelectRFIDBindingCode(productType, RFID, stationName);
                    if (backProCode == "" || backProCode == null)
                    {
                        dicNUsedMasterData[stationName].feedBack = 73;  //解绑时  RFID上条码为空  反馈73
                    }
                    else if (stationData.RFID == "")
                    {

                        dicNUsedMasterData[stationName].feedBack = 74;  //解绑时  RFID为空  反馈74
                    }
                    else
                    {
                        dicNUsedMasterData[stationName].productCode = backProCode;
                        DBHelper.clearRFIDBind(backProCode, productType, RFID, stationName);
                        dicNUsedMasterData[stationName].feedBack = 2;
                    }
                }
                //信号为82将数据存储至80_2 
                else if (stationData.status == 82 && !Execution[stationOrder] && !haveSave[stationOrder]) {
                    DBHelper.save80_2Data(stationData , "OP80_2" , productType , productName);
                    Console.WriteLine("OP80_2 存储数据");
                    haveSave[stationOrder] = true;
                    dicNUsedMasterData[stationName].feedBack = 2;
                }
                //信号为88脱离追溯将产品数据存储
                lock (obj[stationOrder])
                {
                    if (stationData.by2 == 88 && !Execution[stationOrder] && !haveSave[stationOrder])
                    {
                        //置状态为执行中
                        Execution[stationOrder] = true;
                        DBHelper.SaveStationData(stationData, productType, productName);
                        haveSave[stationOrder] = true;
                        Execution[stationOrder] = false;
                        Thread.Sleep(3000);

                    }
                }
                lock (obj2[stationOrder])
                {
                    if (stationData.by2 == 89 && !haveSave[stationOrder])
                    {
                        DBHelper.save80_2Data(stationData, "OP80_2", productType, productName);
                        Console.WriteLine("OP80_2 存储数据");
                        haveSave[stationOrder] = true;
                        Thread.Sleep(3000);

                    }

                }

                //无信号交互的常态
                if (stationData.by2 == 0)
                {
                    haveSave[stationOrder] = false;
                    Execution[stationOrder] = false;
                }
                if (stationData.status == 0)
                {
                    haveSave[stationOrder] = false;
                    dicNUsedMasterData[stationName].feedBack = 0;
                }
            }

        }
        List<string> zplList = new List<string>();
        private void printCode(List<string> printcode) {
            string printerIp = "192.168.1.45";
            string filePath = proCodeTemplateFileName;
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
                        string modifiedZpl = line.Replace("^FD"+ printcode[2] + "^FS", $"^FD{printcode[6]}^FS");
                        //modifiedZpl = modifiedZpl.Replace("^FD"+ printcode[3] + "^FS", $"^FD{printcode[7]}^FS");
                        modifiedZpl = modifiedZpl.Replace("^FD"+ printcode[4] + "^FS", $"^FD{printcode[7] + " " + printcode[8]}^FS");
                        modifiedZpl = modifiedZpl.Replace("^FD,"+ printcode[5] + "^FS", $"^FD,{printcode[6] + " " + printcode[7] + " " + printcode[8]}^FS");

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
                Console.WriteLine(printCount + "    打印文件   ");

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending ZPL to printer: " + ex.Message);
            }
        }
    }
}