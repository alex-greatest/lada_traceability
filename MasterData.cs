using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review
{
    public class MasterData
    {
        public bool heart;                 //主站心跳信号
        public bool run;                   //主站运行信号
        public bool enable;                //启用该工位信号
        public bool model;                 //生产模式，正常还是返修,false 生产，1返修
        public bool by04;
        public bool by05;
        public bool by06;
        public bool by07;
        public bool codeSource1;            //条码一的数据来源
        public bool codeSource2;            //条码二的数据来源
        public bool codeSource3;            //条码三的数据来源
        public bool codeSource4;            //条码四的数据来源
        public bool codeSource5;            //条码五的数据来源
        public bool codeSource6;            //条码六的数据来源
        public bool codeSource7;            //条码七的数据来源
        public bool codeSource8;            //条码八的数据来源
        public Int16 feedBack;              //主站反馈信息   1:RFID验证OK，允许进行下一步动作，11:验证NG或者无相关信息
        public Int16 type;                 //产品型号
        public Int16 leftOrRight;                  //反馈产品放哪个工位
        public Int16 byI2; //多工位2号工位反馈结果                 //备用 16位
        public string productCode;              //产品条码
        public string barcode2;              //附件条码
        public string barcode3;              //附件条码
        public string barcode4;              //附件条码
        public string barcode5;              //附件条码
        public string barcode6;              //附件条码
        public string barcode7;              //附件条码
        public string barcode8;              //附件条码

        public string stationName;
        public MasterData(string stationName)
        {
            this.stationName = stationName;
            productCode = "";
            barcode2 = "";
            barcode3 = "";
            barcode4 = "";
            barcode5 = "";
            barcode6 = "";
            barcode7 = "";
            barcode8 = "";
        }
        public MasterData()
        {

        }
    }
}
