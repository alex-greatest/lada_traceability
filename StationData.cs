using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review
{
    public class StationData
    {
        public string stationName;         // 本工位名称
        public string lastStationName;     // 上一工位名称
        public string mainStationName;     // 主工序名称
        public string function = "";            // 功能
        public string localIP;             // 本地IP
        public int localPort;              // 本地端口号
        public string remoteIP;            // 远程IP
        public string remotePort;          // 远程端口号
        public string stationType;         //子站类型，线上或者线下
        public string operater = "";
        public int lineID;
        public byte team;
        public bool connected;                 //子站通讯状态
        public bool firstStation;              //是否是第一工位
        public bool endStation;                //是否是最后工位
        public int stationOrder;               //本工位工序
        public int stationCountID;             //工位独立计数id ， 用来匹配产品条形码

        //以下为从工位读取的数据
        public bool Estop;                     //子站急停
        public bool heart;                     //子站心跳信号
        public bool alarmMaterial;             //子站Andon报警信息物料报警
        public bool alarmQuality;              //子站Andon报警信息质量报警
        public bool alarmEquipment;            //子站Andon报警信息设备报警
        public bool alarmHelp;                 //子站Andon报警信息求助报警
        public bool by06;
        public bool by07;

        public bool by10;
        public bool by11;
        public bool by12;
        public bool by13;
        public bool by14;
        public bool by15;
        public bool by16;
        public bool by17;
        public Int16 status;                             //子站状态给主机的信息，1，发送RFID，2，发送测试数据，3，等待托盘
        public Int16 quality;                            //子站测试结果  质量代码--代表1是合格，2是不合格
        public Int16 by1;//是否是附加工位                                //备用
        public Int16 by2;// 工位2号工位状态信息                                //备用 
        public string RFID;
        public float[] result = new float[30];           //测试结果
        public string productCode;                          //产品条码
        public string barcode2;                          //附件条码
        public string barcode3;                          //附件条码
        public string barcode4;                          //附件条码
        public string barcode5;                          //附件条码
        public string barcode6;                          //附件条码 
        public string barcode7;                          //附件条码
        public string barcode8;//附加工位2号工位rfid                          //附件条码
        public StationData(string stationName)
        {
            this.stationName = stationName;
            //barcode1 = "";
            //barcode2 = "";
            //barcode3 = "";
            //barcode4 = "";
            //barcode5 = "";
            //barcode6 = "";
            //barcode7 = "";
            //barcode8 = "";
            //RFID = "123WD";
            barcode2 = stationName + "code2";
            barcode3 = stationName + "code3";
            barcode4 = stationName + "code4";
            barcode5 = stationName + "code5";
            barcode6 = stationName + "code6";
            barcode7 = stationName + "code7";
            barcode8 = stationName + "code8";
            //productID = "CPID";
            RFID = "RFID";
        }
        public StationData()
        {

        }
    }

}
