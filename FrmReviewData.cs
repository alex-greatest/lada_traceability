using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Review
{
    public partial class FrmReviewData : Form
    {
        private string rowIndex = "";
        private int gridRowIndex;
        private int gridColIndex;
        string s1, s2;
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        public FrmReviewData(ResourceManager resManager, CultureInfo currentCulture)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;

            UpdateLanguage(currentCulture);
            ShowReview();
            FillProduct();
            FillStation();
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN")
            {
                /*label1.Location = new System.Drawing.Point(77, 21);
                label2.Location = new System.Drawing.Point(89, 46);
                label4.Location = new System.Drawing.Point(89, 73);*/

                s1 = "修改成功!";
                s2 = "添加成功!";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                /*label1.Location = new System.Drawing.Point(13, 22);
                label2.Location = new System.Drawing.Point(127, 46);
                label4.Location = new System.Drawing.Point(115, 72);*/

                s1 = "Изменено";
                s2 = "Добавлено";
            }
            // 更新窗体文本
            this.Text = resManager.GetString("MenuItemPerson");
            label1.Text = resManager.GetString("plabel1");
            label30.Text = resManager.GetString("RDlabel30") + ":";
            label31.Text = resManager.GetString("RDlabel31") + ":";
            btnOK.Text = resManager.GetString("RDbtnOK");

            label2.Text = resManager.GetString("RDdata")+"1";
            label3.Text = resManager.GetString("RDdata")+"2";
            label4.Text = resManager.GetString("RDdata")+"3";
            label5.Text = resManager.GetString("RDdata")+"4";
            label6.Text = resManager.GetString("RDdata")+"5";
            label7.Text = resManager.GetString("RDdata")+"6";
            label8.Text = resManager.GetString("RDdata")+"7";
            label9.Text = resManager.GetString("RDdata")+"8";
            label10.Text = resManager.GetString("RDdata")+"9";
            label11.Text = resManager.GetString("RDdata")+"10";
            label12.Text = resManager.GetString("RDdata")+"11";
            label13.Text = resManager.GetString("RDdata")+"12";
            label14.Text = resManager.GetString("RDdata")+"13";
            label15.Text = resManager.GetString("RDdata")+"14";
            label16.Text = resManager.GetString("RDdata")+"15";
            label17.Text = resManager.GetString("RDdata")+"16";
            label18.Text = resManager.GetString("RDdata")+"17";
            label19.Text = resManager.GetString("RDdata")+"18";
            label20.Text = resManager.GetString("RDdata")+"19";
            label21.Text = resManager.GetString("RDdata")+"20";

            label22.Text = resManager.GetString("RDcode") +"1";
            label23.Text = resManager.GetString("RDcode") +"2";
            label24.Text = resManager.GetString("RDcode") +"3";
            label25.Text = resManager.GetString("RDcode") +"4";
            label26.Text = resManager.GetString("RDcode") +"5";
            label27.Text = resManager.GetString("RDcode") +"6";
            label28.Text = resManager.GetString("RDcode") +"7";
            label29.Text = resManager.GetString("RDcode") +"8";

            dataGridReview.Columns[1].HeaderText = resManager.GetString("RDstation");
            dataGridReview.Columns[2].HeaderText = resManager.GetString("RDdata") + "1";
            dataGridReview.Columns[3].HeaderText = resManager.GetString("RDdata") + "2";
            dataGridReview.Columns[4].HeaderText = resManager.GetString("RDdata") + "3";
            dataGridReview.Columns[5].HeaderText = resManager.GetString("RDdata") + "4";
            dataGridReview.Columns[6].HeaderText = resManager.GetString("RDdata") + "5";
            dataGridReview.Columns[7].HeaderText = resManager.GetString("RDdata") + "6";
            dataGridReview.Columns[8].HeaderText = resManager.GetString("RDdata") + "7";
            dataGridReview.Columns[9].HeaderText = resManager.GetString("RDdata") + "8";
            dataGridReview.Columns[10].HeaderText = resManager.GetString("RDdata") + "9";
            dataGridReview.Columns[11].HeaderText = resManager.GetString("RDdata") + "10";
            dataGridReview.Columns[12].HeaderText = resManager.GetString("RDdata") + "11";
            dataGridReview.Columns[13].HeaderText = resManager.GetString("RDdata") + "12";
            dataGridReview.Columns[14].HeaderText = resManager.GetString("RDdata") + "13";
            dataGridReview.Columns[15].HeaderText = resManager.GetString("RDdata") + "14";
            dataGridReview.Columns[16].HeaderText = resManager.GetString("RDdata") + "15";
            dataGridReview.Columns[17].HeaderText = resManager.GetString("RDdata") + "16";
            dataGridReview.Columns[18].HeaderText = resManager.GetString("RDdata") + "17";
            dataGridReview.Columns[19].HeaderText = resManager.GetString("RDdata") + "18";
            dataGridReview.Columns[20].HeaderText = resManager.GetString("RDdata") + "19";
            dataGridReview.Columns[21].HeaderText = resManager.GetString("RDdata") + "20";

            dataGridReview.Columns[22].HeaderText = resManager.GetString("RDcode") + "1";
            dataGridReview.Columns[23].HeaderText = resManager.GetString("RDcode") + "2";
            dataGridReview.Columns[24].HeaderText = resManager.GetString("RDcode") + "3";
            dataGridReview.Columns[25].HeaderText = resManager.GetString("RDcode") + "4";
            dataGridReview.Columns[26].HeaderText = resManager.GetString("RDcode") + "5";
            dataGridReview.Columns[27].HeaderText = resManager.GetString("RDcode") + "6";
            dataGridReview.Columns[28].HeaderText = resManager.GetString("RDcode") + "7";
            dataGridReview.Columns[29].HeaderText = resManager.GetString("RDcode") + "8";
        }
        private void FillProduct()
        {
            string sql = "select * from Product";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                comProduct.Items.Add(dr[1]);
                if (comProduct.Text.Length == 0)
                {
                    comProduct.Text = dr[1].ToString();
                }
            }
        }

        private void FillStation()
        {
            string sql = "select * from Product where productName=@pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comProduct.Text.ToString().Trim())
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            comLineStation.Items.Clear();
            while (dr.Read())
            {
                for (int i = 3; i < dr.FieldCount; i++)
                {
                    if (dr[i] == null || dr[i].ToString().Length == 0)
                    {
                        continue;
                    }
                    comLineStation.Items.Add(dr[i]);
                }
            }
            string selectSql = "select stationName from station";
            List<string> downStation = DBHelper.selectData(selectSql);
            downLineStation.Items.Clear();
            for (int i = 0; i < downStation.Count; i++)
            {
                if (!comLineStation.Items.Contains(downStation[i]) && downStation[i] != "server") {
                    downLineStation.Items.Add(downStation[i]);
                }
            }
        }
        private void ShowReview()
        {
            string sql = "select * from ReviewData where productName='" + comProduct.Text + "'";
            DataTable dt = DBHelper.GetDataTable(sql, 1);
            dataGridReview.AutoGenerateColumns = false;//不允许自动添加列，手动已添加，并且设置DataPropertyName与要显示的字段名称一致，该语句要在指定数据源的语句之前
            dataGridReview.DataSource = dt;
        }

        private void dataGridReview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex<0)
            {
                return;
            }
            rowIndex = dataGridReview.Rows[e.RowIndex].Cells[0].Value.ToString();
            gridRowIndex = e.RowIndex;
            gridColIndex = e.ColumnIndex;
            string sta = dataGridReview.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
            if (comLineStation.Items.Contains(sta)) { comLineStation.Text = sta; }
            else { downLineStation.Text = sta; }
            txtData01.Text = dataGridReview.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            txtData02.Text = dataGridReview.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();
            txtData03.Text = dataGridReview.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
            txtData04.Text = dataGridReview.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();
            txtData05.Text = dataGridReview.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();
            txtData06.Text = dataGridReview.Rows[e.RowIndex].Cells[7].Value.ToString().Trim();
            txtData07.Text = dataGridReview.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();
            txtData08.Text = dataGridReview.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();
            txtData09.Text = dataGridReview.Rows[e.RowIndex].Cells[10].Value.ToString().Trim();
            txtData10.Text = dataGridReview.Rows[e.RowIndex].Cells[11].Value.ToString().Trim();
            txtData11.Text = dataGridReview.Rows[e.RowIndex].Cells[12].Value.ToString().Trim();
            txtData12.Text = dataGridReview.Rows[e.RowIndex].Cells[13].Value.ToString().Trim();
            txtData13.Text = dataGridReview.Rows[e.RowIndex].Cells[14].Value.ToString().Trim();
            txtData14.Text = dataGridReview.Rows[e.RowIndex].Cells[15].Value.ToString().Trim();
            txtData15.Text = dataGridReview.Rows[e.RowIndex].Cells[16].Value.ToString().Trim();
            txtData16.Text = dataGridReview.Rows[e.RowIndex].Cells[17].Value.ToString().Trim();
            txtData17.Text = dataGridReview.Rows[e.RowIndex].Cells[18].Value.ToString().Trim();
            txtData18.Text = dataGridReview.Rows[e.RowIndex].Cells[19].Value.ToString().Trim();
            txtData19.Text = dataGridReview.Rows[e.RowIndex].Cells[20].Value.ToString().Trim();
            txtData20.Text = dataGridReview.Rows[e.RowIndex].Cells[21].Value.ToString().Trim();

            txtBarcode01.Text = dataGridReview.Rows[e.RowIndex].Cells[22].Value.ToString().Trim();
            txtBarcode02.Text = dataGridReview.Rows[e.RowIndex].Cells[23].Value.ToString().Trim();
            txtBarcode03.Text = dataGridReview.Rows[e.RowIndex].Cells[24].Value.ToString().Trim();
            txtBarcode04.Text = dataGridReview.Rows[e.RowIndex].Cells[25].Value.ToString().Trim();
            txtBarcode05.Text = dataGridReview.Rows[e.RowIndex].Cells[26].Value.ToString().Trim();
            txtBarcode06.Text = dataGridReview.Rows[e.RowIndex].Cells[27].Value.ToString().Trim();
            txtBarcode07.Text = dataGridReview.Rows[e.RowIndex].Cells[28].Value.ToString().Trim();
            txtBarcode08.Text = dataGridReview.Rows[e.RowIndex].Cells[29].Value.ToString().Trim();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckAllTextBoxesEmpty()) {
                return;
            }
            bool LineOK = false;
            string sql;
            MySqlParameter[] pms;
            int i = 0;
            sql = "select * from ReviewData where productName=@pms1 and stationName=@pms2";
            pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comProduct.Text.ToString().Trim()),
                new MySqlParameter("pms2",comLineStation.Text == "" ? downLineStation.Text : comLineStation.Text)
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LineOK = true;
            }
            if (LineOK)
            {
                sql = "update ReviewData set ";
                sql = sql + "data1=@pms1" + ",";
                sql = sql + "data2=@pms2" + ",";
                sql = sql + "data3=@pms3" + ",";
                sql = sql + "data4=@pms4" + ",";
                sql = sql + "data5=@pms5" + ",";
                sql = sql + "data6=@pms6" + ",";
                sql = sql + "data7=@pms7" + ",";
                sql = sql + "data8=@pms8" + ",";
                sql = sql + "data9=@pms9" + ",";
                sql = sql + "data10=@pms10" + ",";
                sql = sql + "data11=@pms11" + ",";
                sql = sql + "data12=@pms12" + ",";
                sql = sql + "data13=@pms13" + ",";
                sql = sql + "data14=@pms14" + ",";
                sql = sql + "data15=@pms15" + ",";
                sql = sql + "data16=@pms16" + ",";
                sql = sql + "data17=@pms17" + ",";
                sql = sql + "data18=@pms18" + ",";
                sql = sql + "data19=@pms19" + ",";
                sql = sql + "data20=@pms20" + ",";
                sql = sql + "barcode1=@pms21" + ",";
                sql = sql + "barcode2=@pms22" + ",";
                sql = sql + "barcode3=@pms23" + ",";
                sql = sql + "barcode4=@pms24" + ",";
                sql = sql + "barcode5=@pms25" + ",";
                sql = sql + "barcode6=@pms26" + ",";
                sql = sql + "barcode7=@pms27" + ",";
                sql = sql + "barcode8=@pms28";
                sql = sql + " where productName=@pms29 and stationName=@pms30";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", txtData01.Text.ToString().Trim()),
                    new MySqlParameter("pms2", txtData02.Text.ToString().Trim()),
                    new MySqlParameter("pms3", txtData03.Text.ToString().Trim()),
                    new MySqlParameter("pms4", txtData04.Text.ToString().Trim()),
                    new MySqlParameter("pms5", txtData05.Text.ToString().Trim()),
                    new MySqlParameter("pms6", txtData06.Text.ToString().Trim()),
                    new MySqlParameter("pms7", txtData07.Text.ToString().Trim()),
                    new MySqlParameter("pms8", txtData08.Text.ToString().Trim()),
                    new MySqlParameter("pms9", txtData09.Text.ToString().Trim()),
                    new MySqlParameter("pms10", txtData10.Text.ToString().Trim()),
                    new MySqlParameter("pms11", txtData11.Text.ToString().Trim()),
                    new MySqlParameter("pms12", txtData12.Text.ToString().Trim()),
                    new MySqlParameter("pms13", txtData13.Text.ToString().Trim()),
                    new MySqlParameter("pms14", txtData14.Text.ToString().Trim()),
                    new MySqlParameter("pms15", txtData15.Text.ToString().Trim()),
                    new MySqlParameter("pms16", txtData16.Text.ToString().Trim()),
                    new MySqlParameter("pms17", txtData17.Text.ToString().Trim()),
                    new MySqlParameter("pms18", txtData18.Text.ToString().Trim()),
                    new MySqlParameter("pms19", txtData19.Text.ToString().Trim()),
                    new MySqlParameter("pms20", txtData20.Text.ToString().Trim()),
                    new MySqlParameter("pms21", txtBarcode01.Text.ToString().Trim()),
                    new MySqlParameter("pms22", txtBarcode02.Text.ToString().Trim()),
                    new MySqlParameter("pms23", txtBarcode03.Text.ToString().Trim()),
                    new MySqlParameter("pms24", txtBarcode04.Text.ToString().Trim()),
                    new MySqlParameter("pms25", txtBarcode05.Text.ToString().Trim()),
                    new MySqlParameter("pms26", txtBarcode06.Text.ToString().Trim()),
                    new MySqlParameter("pms27", txtBarcode07.Text.ToString().Trim()),
                    new MySqlParameter("pms28", txtBarcode08.Text.ToString().Trim()),
                    new MySqlParameter("pms29", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms30", comLineStation.Text == "" ? downLineStation.Text : comLineStation.Text)
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    ShowReview();
                    //dataGridReview.Rows[gridRowIndex].Selected = true;
                    //dataGridReview.Rows[gridRowIndex].Cells[gridColIndex].Selected = true;
                    MessageBox.Show(s1);
                }
            }
            else
            {
                sql = "insert  into ReviewData (productName,stationName,data1,data2,data3,data4,data5,data6,data7,data8,data9,";
                sql = sql + " data10,data11,data12,data13,data14,data15,data16,data17,data18,data19,data20,";
                sql = sql + " barcode1,barcode2,barcode3,barcode4,barcode5,barcode6,barcode7,barcode8) values (";
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
                sql = sql + "@pms26" + ",";
                sql = sql + "@pms27" + ",";
                sql = sql + "@pms28" + ",";
                sql = sql + "@pms29" + ",";
                sql = sql + "@pms30";
                sql = sql + ")";
                pms = new MySqlParameter[]
                {
                    new MySqlParameter("pms1", comProduct.Text.ToString().Trim()),
                    new MySqlParameter("pms2", comLineStation.Text == "" ? downLineStation.Text : comLineStation.Text),
                    new MySqlParameter("pms3", txtData01.Text.ToString().Trim()),
                    new MySqlParameter("pms4", txtData02.Text.ToString().Trim()),
                    new MySqlParameter("pms5", txtData03.Text.ToString().Trim()),
                    new MySqlParameter("pms6", txtData04.Text.ToString().Trim()),
                    new MySqlParameter("pms7", txtData05.Text.ToString().Trim()),
                    new MySqlParameter("pms8", txtData06.Text.ToString().Trim()),
                    new MySqlParameter("pms9", txtData07.Text.ToString().Trim()),
                    new MySqlParameter("pms10", txtData08.Text.ToString().Trim()),
                    new MySqlParameter("pms11", txtData09.Text.ToString().Trim()),
                    new MySqlParameter("pms12", txtData10.Text.ToString().Trim()),
                    new MySqlParameter("pms13", txtData11.Text.ToString().Trim()),
                    new MySqlParameter("pms14", txtData12.Text.ToString().Trim()),
                    new MySqlParameter("pms15", txtData13.Text.ToString().Trim()),
                    new MySqlParameter("pms16", txtData14.Text.ToString().Trim()),
                    new MySqlParameter("pms17", txtData15.Text.ToString().Trim()),
                    new MySqlParameter("pms18", txtData16.Text.ToString().Trim()),
                    new MySqlParameter("pms19", txtData17.Text.ToString().Trim()),
                    new MySqlParameter("pms20", txtData18.Text.ToString().Trim()),
                    new MySqlParameter("pms21", txtData19.Text.ToString().Trim()),
                    new MySqlParameter("pms22", txtData20.Text.ToString().Trim()),
                    new MySqlParameter("pms23", txtBarcode01.Text.ToString().Trim()),
                    new MySqlParameter("pms24", txtBarcode02.Text.ToString().Trim()),
                    new MySqlParameter("pms25", txtBarcode03.Text.ToString().Trim()),
                    new MySqlParameter("pms26", txtBarcode04.Text.ToString().Trim()),
                    new MySqlParameter("pms27", txtBarcode05.Text.ToString().Trim()),
                    new MySqlParameter("pms28", txtBarcode06.Text.ToString().Trim()),
                    new MySqlParameter("pms29", txtBarcode07.Text.ToString().Trim()),
                    new MySqlParameter("pms30", txtBarcode08.Text.ToString().Trim())
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                if (i > 0)
                {
                    ShowReview();
                    MyTools.Show(s2);
                }
            }
        }

        private void comProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowReview();
        }

        private void FrmReviewData_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmMain.frmReviewDataWindow = null;
        }

        private void comLineStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comLineStation.SelectedIndex != -1) downLineStation.SelectedIndex = -1;
        }

        private void downLineStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (downLineStation.SelectedIndex != -1) comLineStation.SelectedIndex = -1;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private bool CheckAllTextBoxesEmpty()
        {
            // 获取当前Form上所有的TextBox控件，包括嵌套在容器中的
            var textBoxes = GetAllTextBoxes(this);

            // 判断所有TextBox的Text是否都为空或仅包含空白字符
            bool allEmpty = textBoxes.All(tb => string.IsNullOrWhiteSpace(tb.Text));

            if (allEmpty)
            {
                MessageBox.Show("Все текстовые поля пустые, пожалуйста, заполните соответствующую информацию！");
                return true;
            }
            return false;
        }

        // 递归获取所有TextBox控件的方法
        private static IEnumerable<TextBox> GetAllTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox tb)
                    yield return tb;

                // 如果是容器控件（如Panel、GroupBox等），递归检查其子控件
                foreach (var child in GetAllTextBoxes(control))
                    yield return child;
            }
        }
    }
}
