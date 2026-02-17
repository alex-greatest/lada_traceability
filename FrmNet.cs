using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmNet : Form
    {
        private Dictionary<string, StationData> dicStationData = new Dictionary<string, StationData>();    //该生产工艺所用工位的数据信息
        private Dictionary<string, MasterData> dicMasterData = new Dictionary<string, MasterData>();       //该生产工艺所用工位的追溯反馈信息、
        private Dictionary<string, StationData> dicNStationData = new Dictionary<string, StationData>();    //该生产工艺不用工位的数据信息
        private Dictionary<string, MasterData> dicNMasterData = new Dictionary<string, MasterData>();       //该生产工艺不用工位的追溯反馈信息、
        private StationData sd = new StationData();
        private MasterData md = new MasterData();
        public FrmNet(Dictionary<string, StationData> dicStationData,Dictionary<string, MasterData> dicMasterData)
        {
            InitializeComponent();
            this.dicStationData = dicStationData;
            this.dicMasterData = dicMasterData;
            ini();
        }
        public FrmNet(Dictionary<string, StationData> dicStationData, Dictionary<string, MasterData> dicMasterData,Dictionary<string, StationData> dicNStationData, Dictionary<string, MasterData> dicNMasterData)
        {
            InitializeComponent();
            this.dicStationData = dicStationData;
            this.dicMasterData = dicMasterData;
            this.dicNStationData = dicNStationData;
            this.dicNMasterData = dicNMasterData;
            ini();
        }

        private void ini()
        {
            foreach(var dic in dicStationData)
            {
                comStation.Items.Add(dic.Key);
                if (comStation.Text.Length == 0)
                {
                    comStation.Text = dic.Key.ToString();
                }
            }
            foreach (var dic in dicNStationData)
            {
                if (comStation.Items.Contains(dic.Key) == false)
                {
                    comStation.Items.Add(dic.Key);
                }
                
            }
        }


        private void showAllStationData()
        {
            listSendData.Items.Clear();
            string resultData = null;
            listSendData.Items.Add("心跳:" + md.heart);
            listSendData.Items.Add("运行:" + md.run);
            listSendData.Items.Add("启用:" + md.enable);
            listSendData.Items.Add("备用:" + md.byI2);
            listSendData.Items.Add("型号:" + md.type);
            //listSendData.Items.Add(md.by03);
            //listSendData.Items.Add(md.by04);
            //listSendData.Items.Add(md.by05);
            //listSendData.Items.Add(md.by06);
            //listSendData.Items.Add(md.by07);
            listSendData.Items.Add("来源1:" + md.codeSource1);
            listSendData.Items.Add("来源2:" + md.codeSource2);
            listSendData.Items.Add("来源3:" + md.codeSource3);
            listSendData.Items.Add("来源4:" + md.codeSource4);
            listSendData.Items.Add("来源5:" + md.codeSource5);
            listSendData.Items.Add("来源6:" + md.codeSource6);
            listSendData.Items.Add("来源7:" + md.codeSource7);
            listSendData.Items.Add("来源8:" + md.codeSource8);
            listSendData.Items.Add("反馈:" + md.feedBack);
            //listSendData.Items.Add(md.byI1);
            //listSendData.Items.Add(md.byI2);
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.productCode));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode2));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode3));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode4));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode5));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode6));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode7));
            listSendData.Items.Add("附件:" + MyConvert.CheckNull(md.barcode8));


            listReceiveData.Items.Clear();
            listReceiveData.Items.Add("工位:" + MyConvert.CheckNull(sd.stationName));
            listReceiveData.Items.Add("地址L:" + MyConvert.CheckNull(sd.localIP));
            //listReceiveData.Items.Add(sd.localPort);
            listReceiveData.Items.Add("地址R:" + MyConvert.CheckNull(sd.remoteIP));
            //listReceiveData.Items.Add(checkNull(sd.remotePort));
            //listReceiveData.Items.Add(checkNull(sd.stationType));
            listReceiveData.Items.Add("操作:" + MyConvert.CheckNull(sd.operater));
            //listReceiveData.Items.Add(sd.lineID);
            //listReceiveData.Items.Add(sd.team);
            //listReceiveData.Items.Add(checkNull(sd.productID));
            listReceiveData.Items.Add("通讯:" + sd.connected);
            listReceiveData.Items.Add("最初:" + sd.firstStation);
            listReceiveData.Items.Add("最末:" + sd.endStation);
            listReceiveData.Items.Add("备用:" + sd.by2);


            //listReceiveData.Items.Add(sd.Estop);
            listReceiveData.Items.Add("心跳:" + sd.heart);
            //listReceiveData.Items.Add(sd.alarmMaterial);
            //listReceiveData.Items.Add(sd.alarmQuality);
            //listReceiveData.Items.Add(sd.alarmEquipment);
            //listReceiveData.Items.Add(sd.alarmHelp);
            //listReceiveData.Items.Add(sd.by06);
            //listReceiveData.Items.Add(sd.by07);
            //listReceiveData.Items.Add(sd.by10);
            //listReceiveData.Items.Add(sd.by11);
            //listReceiveData.Items.Add(sd.by12);
            //listReceiveData.Items.Add(sd.by13);
            //listReceiveData.Items.Add(sd.by14);
            //listReceiveData.Items.Add(sd.by15);
            //listReceiveData.Items.Add(sd.by16);
            //listReceiveData.Items.Add(sd.by17);
            listReceiveData.Items.Add("状态:" + sd.status);
            listReceiveData.Items.Add("结果:" + sd.quality);
            //listReceiveData.Items.Add(sd.by1);
            //listReceiveData.Items.Add(sd.by2);
            listReceiveData.Items.Add("RFID:" + MyConvert.CheckNull(sd.RFID));
            //listReceiveData.Items.Add("产品:" + MyConvert.CheckNull(sd.RFID).Length);
            for (int j = 0; j < 5; j++)
            {
                resultData = "";
                for (int i = 0 + j * 4; i < j * 4 + 4; i++)
                {
                    if (i == 0 + j * 4)
                    {
                        resultData = i.ToString() + ":" + sd.result[i].ToString();
                    }
                    else
                    {
                        resultData = resultData + "/" + i.ToString() + ":" + sd.result[i].ToString();
                    }
                }
                listReceiveData.Items.Add("结果:" + resultData);
            }
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.productCode));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode2));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode3));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode4));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode5));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode6));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode7));
            listReceiveData.Items.Add("附件:" + MyConvert.CheckNull(sd.barcode8));
        }
        private void comStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stationName = comStation.Text.ToString();
            if (dicStationData.ContainsKey(stationName))
            {
                sd = dicStationData[stationName];
                md = dicMasterData[stationName];
            }
            else
            {
                sd = dicNStationData[stationName];
                md = dicNMasterData[stationName];
            }
            txtStationOrder.Text = sd.stationOrder.ToString();
            //showStationData();
            showAllStationData();
        }

        private void timerUpData_Tick(object sender, EventArgs e)
        {
            //showStationData();
            showAllStationData();
        }

        private void FrmNet_Load(object sender, EventArgs e)
        {
            timerUpData.Enabled = true;
        }

        private void FrmNet_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmNetWindow = null;
        }
    }
}
