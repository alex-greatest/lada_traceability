using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace Review{

    public partial class FrmBarcode : Form
    {

        private int txtposition;
        public FrmBarcode()
        {
            InitializeComponent();
        }
        private void FillStation()
        {
            string sql = "select * from Station";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                if (comStation.Items.Contains(dr[1]) == false)
                {
                    if(dr[1].ToString()!="Server")
                    {
                        comStation.Items.Add(dr[1]);
                    } 
                }
            }
            comStation.SelectedIndex = 0;
        }
        private void FillProduct()
        {
            string sql = "select * from Product";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                if (comProduct.Items.Contains(dr[1]) == false)
                {
                    comProduct.Items.Add(dr[1]);
                }
            }
            comProduct.SelectedIndex = 0;
        }
        private void IniSerialPort()
        {
            string sql = "select * from RS where Id=@pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1","1")
            };
            DataTable dt = DBHelper.GetDataTable(sql, 0, pms);
            serialPort.PortName = dt.Rows[0].ItemArray[1].ToString();
            serialPort.BaudRate = int.Parse(dt.Rows[0].ItemArray[2].ToString());
            serialPort.DataBits = int.Parse(dt.Rows[0].ItemArray[3].ToString());
            serialPort.StopBits = (StopBits)int.Parse(dt.Rows[0].ItemArray[4].ToString());
            serialPort.Parity = (Parity)int.Parse(dt.Rows[0].ItemArray[5].ToString());
            label.Text = "串口:" + serialPort.PortName;
            label.Text = label.Text + " 波特率:" + serialPort.BaudRate;
            label.Text = label.Text + " 数据位:" + serialPort.DataBits;
            label.Text = label.Text + " 校验位:" + serialPort.Parity;
            label.Text = label.Text + " 停止位:" + serialPort.StopBits;
        }
        private void IniTxtBarcode()
        {
            if (comProduct.Text.Length == 0)
            {
                return;
            }
            if (comStation.Text.Length == 0)
            {
                return;
            }
            textCode1.Text = "";
            textCode2.Text = "";
            textCode3.Text = "";
            textCode4.Text = "";
            textCode5.Text = "";
            textCode6.Text = "";
            textCode7.Text = "";
            textCode8.Text = "";
            checkBarcode1.Checked = false;
            checkBarcode2.Checked = false;
            checkBarcode3.Checked = false;
            checkBarcode4.Checked = false;
            checkBarcode5.Checked = false;
            checkBarcode6.Checked = false;
            checkBarcode7.Checked = false;
            checkBarcode8.Checked = false;
            txtBarcodeName1.Text = "";
            txtBarcodeName2.Text = "";
            txtBarcodeName3.Text = "";
            txtBarcodeName4.Text = "";
            txtBarcodeName5.Text = "";
            txtBarcodeName6.Text = "";
            txtBarcodeName7.Text = "";
            txtBarcodeName8.Text = "";

            string sql = "select * from Barcode  where product = @pms1 and station = @pms2";
            MySqlParameter[] pms = new MySqlParameter[] 
            {
                new MySqlParameter("pms1", comProduct.Text.ToString().Trim()),
                new MySqlParameter("pms2", comStation.Text.ToString().Trim())
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                textCode1.Text = dr[3].ToString();
                textCode2.Text = dr[4].ToString();
                textCode3.Text = dr[5].ToString();
                textCode4.Text = dr[6].ToString();
                textCode5.Text = dr[7].ToString();
                textCode6.Text = dr[8].ToString();
                textCode7.Text = dr[9].ToString();
                textCode8.Text = dr[10].ToString();
                checkBarcode1.Checked = bool.Parse(dr[11].ToString());
                checkBarcode2.Checked = bool.Parse(dr[12].ToString());
                checkBarcode3.Checked = bool.Parse(dr[13].ToString());
                checkBarcode4.Checked = bool.Parse(dr[14].ToString());
                checkBarcode5.Checked = bool.Parse(dr[15].ToString());
                checkBarcode6.Checked = bool.Parse(dr[16].ToString());
                checkBarcode7.Checked = bool.Parse(dr[17].ToString());
                checkBarcode8.Checked = bool.Parse(dr[18].ToString());
                txtBarcodeName1.Text = dr[19].ToString();
                txtBarcodeName2.Text = dr[20].ToString();
                txtBarcodeName3.Text = dr[21].ToString();
                txtBarcodeName4.Text = dr[22].ToString();
                txtBarcodeName5.Text = dr[23].ToString();
                txtBarcodeName6.Text = dr[24].ToString();
                txtBarcodeName7.Text = dr[25].ToString();
                txtBarcodeName8.Text = dr[26].ToString();
                textCodeRule1.Text = dr[27].ToString();
                textCodeRule2.Text = dr[28].ToString();
                textCodeRule3.Text = dr[29].ToString();
                textCodeRule4.Text = dr[30].ToString();
                textCodeRule5.Text = dr[31].ToString();
                textCodeRule6.Text = dr[32].ToString();
                textCodeRule7.Text = dr[33].ToString();
                textCodeRule8.Text = dr[34].ToString();
                break;
            }
        }

        private void FrmBarcode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmBarcodeWindow = null;
        }
        private void FrmBarcode_Load(object sender, EventArgs e)
        {
            FillStation();
            FillProduct();
            IniSerialPort();
            IniTxtBarcode();
        }
        private void btnOpen_Click(object sender, EventArgs e)  
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                serialPort.Dispose();
                label.BackColor = btnOpen.BackColor;
                btnOpen.Text = "打开串口";
            }
            else
            {
                try
                {
                    serialPort.Open();
                    label.BackColor = Color.FromArgb(0, 255, 0);
                    btnOpen.Text = "关闭串口";
                }
                catch 
                {
                    //捕获到异常信息，创建一个新的comm对象，之前的不能用了。 不知道为啥不能用，所以下面的代码没用。 
                    //serialPort = new System.IO.Ports.SerialPort();
                    //将异常信息传递给用户。  
                    MyTools.Show(serialPort.PortName+"不存在");
                    return;
                }
            }
        }
        private void FrmBarcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serialPort != null && serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
            }
            catch (Exception ex)
            {
                //将异常信息传递给用户。  
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(UpdateUIText));
            System.Console.Write("收到串口数据" + "\r\n");
        }
        private void UpdateUIText(object s, EventArgs e)
        {
            try
            {
                //必须要阻塞线程一段时间，以免在交易超时的情况下，由于read太快导致读取不完整
                System.Threading.Thread.Sleep(100);
                string txt = serialPort.ReadExisting().ToString().Trim();
                if (txtposition > 0)
                {
                    if (txtposition == 1)
                    {
                        textCode1.Text = txt;
                    }
                    else if (txtposition == 2)
                    {
                        textCode2.Text = txt;
                    }
                    else if (txtposition == 3)
                    {
                        textCode3.Text = txt;
                    }
                    else if (txtposition == 4)
                    {
                        textCode4.Text = txt;
                    }
                    else if (txtposition == 5)
                    {
                        textCode5.Text = txt;
                    }
                    else if (txtposition == 6)
                    {
                        textCode6.Text = txt;
                    }
                    else if (txtposition == 7)
                    {
                        textCode7.Text = txt;
                    }
                    else if (txtposition == 8)
                    {
                        textCode8.Text = txt;
                    }
                }
                else
                {
                    if (textCode1.Text.ToString().Trim().Length == 0)
                    {
                        textCode1.Text = txt;
                    }
                    else if (textCode2.Text.ToString().Trim().Length == 0)
                    {
                        textCode2.Text = txt;
                    }
                    else if (textCode3.Text.ToString().Trim().Length == 0)
                    {
                        textCode3.Text = txt;
                    }
                    else if (textCode4.Text.ToString().Trim().Length == 0)
                    {
                        textCode4.Text = txt;
                    }
                    else if (textCode5.Text.ToString().Trim().Length == 0)
                    {
                        textCode5.Text = txt;
                    }
                    else if (textCode6.Text.ToString().Trim().Length == 0)
                    {
                        textCode6.Text = txt;
                    }
                    else if (textCode7.Text.ToString().Trim().Length == 0)
                    {
                        textCode7.Text = txt;
                    }
                    else if (textCode8.Text.ToString().Trim().Length == 0)
                    {
                        textCode8.Text = txt;
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            bool LineOK=false;
            string sql;
            MySqlParameter[] pms;
            int i=0;
            sql = "select * from Barcode where product=@pms1 and station=@pms2";
            pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comProduct.Text.ToString().Trim()),
                new MySqlParameter("pms2",comStation.Text.ToString().Trim())
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LineOK = true;
            }
            if (LineOK)
            {
                sql = "update Barcode set ";
                sql = sql + "barcode1=@pms1" + ",";
                sql = sql + "barcode2=@pms2" + ",";
                sql = sql + "barcode3=@pms3" + ",";
                sql = sql + "barcode4=@pms4" + ",";
                sql = sql + "barcode5=@pms5" + ",";
                sql = sql + "barcode6=@pms6" + ",";
                sql = sql + "barcode7=@pms7" + ",";
                sql = sql + "barcode8=@pms8" + ",";
                sql = sql + "barcodesor1=@pms9" + ",";
                sql = sql + "barcodesor2=@pms10" + ",";
                sql = sql + "barcodesor3=@pms11" + ",";
                sql = sql + "barcodesor4=@pms12" + ",";
                sql = sql + "barcodesor5=@pms13" + ",";
                sql = sql + "barcodesor6=@pms14" + ",";
                sql = sql + "barcodesor7=@pms15" + ",";
                sql = sql + "barcodesor8=@pms16" + ",";
                sql = sql + "barcodeName1=@pms17" + ",";
                sql = sql + "barcodeName2=@pms18" + ",";
                sql = sql + "barcodeName3=@pms19" + ",";
                sql = sql + "barcodeName4=@pms20" + ",";
                sql = sql + "barcodeName5=@pms21" + ",";
                sql = sql + "barcodeName6=@pms22" + ",";
                sql = sql + "barcodeName7=@pms23" + ",";
                sql = sql + "barcodeName8=@pms24";
                sql = sql + " where product=@pms25 and station =@pms26";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", textCode1.Text.ToString().Trim()),
                    new MySqlParameter("pms2", textCode2.Text.ToString().Trim()),
                    new MySqlParameter("pms3", textCode3.Text.ToString().Trim()),
                    new MySqlParameter("pms4", textCode4.Text.ToString().Trim()),
                    new MySqlParameter("pms5", textCode5.Text.ToString().Trim()),
                    new MySqlParameter("pms6", textCode6.Text.ToString().Trim()),
                    new MySqlParameter("pms7", textCode7.Text.ToString().Trim()),
                    new MySqlParameter("pms8", textCode8.Text.ToString().Trim()),
                    new MySqlParameter("pms9", checkBarcode1.Checked.ToString().Trim()),
                    new MySqlParameter("pms10", checkBarcode2.Checked.ToString().Trim()),
                    new MySqlParameter("pms11", checkBarcode3.Checked.ToString().Trim()),
                    new MySqlParameter("pms12", checkBarcode4.Checked.ToString().Trim()),
                    new MySqlParameter("pms13", checkBarcode5.Checked.ToString().Trim()),
                    new MySqlParameter("pms14", checkBarcode6.Checked.ToString().Trim()),
                    new MySqlParameter("pms15", checkBarcode7.Checked.ToString().Trim()),
                    new MySqlParameter("pms16", checkBarcode8.Checked.ToString().Trim()),
                    new MySqlParameter("pms17", txtBarcodeName1.Text.ToString().Trim()),
                    new MySqlParameter("pms18", txtBarcodeName2.Text.ToString().Trim()),
                    new MySqlParameter("pms19", txtBarcodeName3.Text.ToString().Trim()),
                    new MySqlParameter("pms20", txtBarcodeName4.Text.ToString().Trim()),
                    new MySqlParameter("pms21", txtBarcodeName5.Text.ToString().Trim()),
                    new MySqlParameter("pms22", txtBarcodeName6.Text.ToString().Trim()),
                    new MySqlParameter("pms23", txtBarcodeName7.Text.ToString().Trim()),
                    new MySqlParameter("pms24", txtBarcodeName8.Text.ToString().Trim()),
                    new MySqlParameter("pms25", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms26", comStation.Text.ToString().Trim())
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    MyTools.Show("修改成功!");
                }
            }
            else
            {
                sql = "insert  into Barcode (product,station,barcode1,barcode2,barcode3,barcode4,barcode5,barcode6,barcode7,barcode8,";
                sql = sql + " barcodesor1,barcodesor2,barcodesor3,barcodesor4,barcodesor5,barcodesor6,barcodesor7,barcodesor8,";
                sql = sql + " barcodeName1,barcodeName2,barcodeName3,barcodeName4,barcodeName5,barcodeName6,barcodeName7,barcodeName8) values (";
                sql = sql + "@pms1" + ",";
                sql = sql + "@pms2" + ",";
                sql = sql + "@pms3" + ",";
                sql = sql + "@pms4" + ",";
                sql = sql + "@pms5" + ",";
                sql = sql + "@pms6" + ",";
                sql = sql + "@pms7" + ",";
                sql = sql + "@pms8" + ",";
                sql = sql + "@pms9" + ",";
                sql = sql + "@pms10" + ",";
                sql = sql + "@pms11" + ",";
                sql = sql + "@pms12" + ",";
                sql = sql + "@pms13" + ",";
                sql = sql + "@pms14" + ",";
                sql = sql + "@pms15" + ",";
                sql = sql + "@pms16" + ",";
                sql = sql + "@pms17" + ",";
                sql = sql + "@pms18" + ",";
                sql = sql + "@pms19" + ",";
                sql = sql + "@pms20" + ",";
                sql = sql + "@pms21" + ",";
                sql = sql + "@pms22" + ",";
                sql = sql + "@pms23" + ",";
                sql = sql + "@pms24" + ",";
                sql = sql + "@pms25" + ",";
                sql = sql + "@pms26";
                sql = sql + ")";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms2", comStation.Text.ToString().Trim()),
                    new MySqlParameter("pms3", textCode1.Text.ToString().Trim()),
                    new MySqlParameter("pms4", textCode2.Text.ToString().Trim()),
                    new MySqlParameter("pms5", textCode3.Text.ToString().Trim()),
                    new MySqlParameter("pms6", textCode4.Text.ToString().Trim()),
                    new MySqlParameter("pms7", textCode5.Text.ToString().Trim()),
                    new MySqlParameter("pms8", textCode6.Text.ToString().Trim()),
                    new MySqlParameter("pms9", textCode7.Text.ToString().Trim()),
                    new MySqlParameter("pms10", textCode8.Text.ToString().Trim()),
                    new MySqlParameter("pms11", checkBarcode1.Checked.ToString().Trim()),
                    new MySqlParameter("pms12", checkBarcode2.Checked.ToString().Trim()),
                    new MySqlParameter("pms13", checkBarcode3.Checked.ToString().Trim()),
                    new MySqlParameter("pms14", checkBarcode4.Checked.ToString().Trim()),
                    new MySqlParameter("pms15", checkBarcode5.Checked.ToString().Trim()),
                    new MySqlParameter("pms16", checkBarcode6.Checked.ToString().Trim()),
                    new MySqlParameter("pms17", checkBarcode7.Checked.ToString().Trim()),
                    new MySqlParameter("pms18", checkBarcode8.Checked.ToString().Trim()),
                    new MySqlParameter("pms19", txtBarcodeName1.Text.ToString().Trim()),
                    new MySqlParameter("pms20", txtBarcodeName2.Text.ToString().Trim()),
                    new MySqlParameter("pms21", txtBarcodeName3.Text.ToString().Trim()),
                    new MySqlParameter("pms22", txtBarcodeName4.Text.ToString().Trim()),
                    new MySqlParameter("pms23", txtBarcodeName5.Text.ToString().Trim()),
                    new MySqlParameter("pms24", txtBarcodeName6.Text.ToString().Trim()),
                    new MySqlParameter("pms25", txtBarcodeName7.Text.ToString().Trim()),
                    new MySqlParameter("pms26", txtBarcodeName8.Text.ToString().Trim())
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    MyTools.Show("添加成功!");
                }
            }
        }

        private void comStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTxtBarcode();
        }

        private void comProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            IniTxtBarcode();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string controlName;
            controlName = this.ActiveControl.Name;
            if (controlName == "textCode1")
            {
                txtposition = 1;
            }
            else if (controlName == "textCode2")
            {
                txtposition = 2;
            }
            else if (controlName == "textCode3")
            {
                txtposition = 3;
            }
            else if (controlName == "textCode4")
            {
                txtposition = 4;
            }
            else if (controlName == "textCode5")
            {
                txtposition = 5;
            }
            else if (controlName == "textCode6")
            {
                txtposition = 6;
            }
            else if (controlName == "textCode7")
            {
                txtposition = 7;
            }
            else if (controlName == "textCode8")
            {
                txtposition = 8;
            }
            else
            {
                txtposition = 0;
            }


        }

        private void btnRule_Click(object sender, EventArgs e)
        {
            bool LineOK = false;
            string sql;
            MySqlParameter[] pms;
            int i = 0;
            sql = "select * from Barcode where product=@pms1 and station=@pms2";
            pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comProduct.Text.ToString().Trim()),
                new MySqlParameter("pms2",comStation.Text.ToString().Trim())
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LineOK = true;
            }
            if (LineOK)
            {
                sql = "update Barcode set ";
                sql = sql + "codeRule1=@pms1" + ",";
                sql = sql + "codeRule2=@pms2" + ",";
                sql = sql + "codeRule3=@pms3" + ",";
                sql = sql + "codeRule4=@pms4" + ",";
                sql = sql + "codeRule5=@pms5" + ",";
                sql = sql + "codeRule6=@pms6" + ",";
                sql = sql + "codeRule7=@pms7" + ",";
                sql = sql + "codeRule8=@pms8";
                sql = sql + " where product=@pms25 and station =@pms26";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", textCodeRule1.Text.ToString().Trim()),
                    new MySqlParameter("pms2", textCodeRule2.Text.ToString().Trim()),
                    new MySqlParameter("pms3", textCodeRule3.Text.ToString().Trim()),
                    new MySqlParameter("pms4", textCodeRule4.Text.ToString().Trim()),
                    new MySqlParameter("pms5", textCodeRule5.Text.ToString().Trim()),
                    new MySqlParameter("pms6", textCodeRule6.Text.ToString().Trim()),
                    new MySqlParameter("pms7", textCodeRule7.Text.ToString().Trim()),
                    new MySqlParameter("pms8", textCodeRule8.Text.ToString().Trim()),
                    new MySqlParameter("pms25", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms26", comStation.Text.ToString().Trim())
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    MyTools.Show("修改成功!");
                }
            }
            else
            {
                sql = "insert  into Barcode (product,station,codeRule1,codeRule2,codeRule3,codeRule4,codeRule5,codeRule6,codeRule7,codeRule8";
                sql = sql + " ) values (";
                sql = sql + "@pms1" + ",";
                sql = sql + "@pms2" + ",";
                sql = sql + "@pms3" + ",";
                sql = sql + "@pms4" + ",";
                sql = sql + "@pms5" + ",";
                sql = sql + "@pms6" + ",";
                sql = sql + "@pms7" + ",";
                sql = sql + "@pms8" + ",";
                sql = sql + "@pms9" + ",";
                sql = sql + "@pms10";
                sql = sql + ")";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms2", comStation.Text.ToString().Trim()),
                    new MySqlParameter("pms3", textCodeRule1.Text.ToString().Trim()),
                    new MySqlParameter("pms4", textCodeRule2.Text.ToString().Trim()),
                    new MySqlParameter("pms5", textCodeRule3.Text.ToString().Trim()),
                    new MySqlParameter("pms6", textCodeRule4.Text.ToString().Trim()),
                    new MySqlParameter("pms7", textCodeRule5.Text.ToString().Trim()),
                    new MySqlParameter("pms8", textCodeRule6.Text.ToString().Trim()),
                    new MySqlParameter("pms9", textCodeRule7.Text.ToString().Trim()),
                    new MySqlParameter("pms10", textCodeRule8.Text.ToString().Trim())
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    MyTools.Show("添加成功!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = MyTools.checkString(textCode1.Text.ToString(), textCodeRule1.Text.ToString(), textCodeRule2.Text.ToString());
            MyTools.Show(ok.ToString());
        }
    }
}
