using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmReview : Form
    {
        int productType = 1;
        string searchStr = "";
        string searchTimeColumn = "";
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        string s1, s2, s3, s4, s5, s6, s7;
        public FrmReview(ResourceManager resManager, CultureInfo currentCulture)
        {
            InitializeComponent();
            hideTheControl();
            this.resManager = resManager;
            this.currentCulture = currentCulture;
            UpdateLanguage(currentCulture);
            FillProduct();
            FillStation();
            //searchStr = sql();
            comResult.Items.Add(s1);
            comResult.Items.Add(s2);
            comResult.Items.Add(s3);
            comResult.Text = s1;
            //comStation.Hide();
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN")
            {
                s1 = "全部";
                s2 = "合格";
                s3 = "不合格";
                s4 = "数据追溯尚未设置，请前往数据管理中设置!";
                s5 = "导出数据成功";
                s6 = "导出数据失败";
                s7 = "未启用追溯请查询单站数据";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                s1 = "Все";
                s2 = "OK";
                s3 = "NOK";
                s4 = "Отслеживание данных не настроено. Пожалуйста, настройте";
                s5 = "Вывод данных выполнен";
                s6 = "Ошибка вывода данных";
                s7 = "Отслеживание не включено. Пожалуйста, запросите данные по одной станции.";
            }
            // 更新窗体文本
            this.Text = resManager.GetString("MenuItemReview");
            label1.Text = resManager.GetString("plabel1") + ":";
            label5.Text = resManager.GetString("plabel4");
            label2.Text = resManager.GetString("Rlabel2");
            btnSearch.Text = resManager.GetString("RbtnSearch");
            btnToExcel.Text = resManager.GetString("RbtnToExcel");
            group1.Text = resManager.GetString("Rgroup1");
            label4.Text = resManager.GetString("Rlabel4");
            button1.Text = resManager.GetString("Rbutton1");
            checkBox1.Text = resManager.GetString("RcheckBox1");
        }
            private void FillProduct()
        {
            string sql = "select * from Product";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                comProduct.Items.Add(dr[1]);
            }
        }
        private void FillStation()
        {
            string sql = "select stationName from reviewdata where productName = '"+comProduct.Text+"'";
            List<string> list = DBHelper.selectData(sql);
            comStation.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                comStation.Items.Add(list[i]);
            }
            comStation.Items.Add("all");
            comStation.Text = "all";
        }
        private string SearchString(string productName, string stationName)
        {
            string sql;
            string s = "";
            MySqlParameter[] pms;
            sql = "select * from ReviewData where productName=@pms1 and stationName=@pms2";
            pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",MyTools.MytoString(productName)),
                new MySqlParameter("pms2",MyTools.MytoString(stationName))
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                for (int i = 2; i < dr.FieldCount; i++)
                {
                    
                    if (dr[i].ToString().Trim().Length != 0)
                    {
                        if (i == 2)
                        {
                            s = s + " " + stationName + "." + dr.GetName(i).ToString() + " as 工位名称 ,";
                            continue;
                        }
                        else {
                            s = s + "" + stationName + "." + dr.GetName(i).ToString() + " as '" + dr[i].ToString().Trim() + "',";
                        }
                        
                    }
                }
                s = " "+stationName+".time as 时间," + s;
               // searchTimeColumn = "dateAtime" + Id;
            }
            return s;
        }

        private string sql()
        {
            int a;
            string s="";
            searchTimeColumn = "" + comStation.Items[0] +".time";
            if (comStation.Text.ToString().Trim() == "all")
            {
                for(int i=0;i< comStation.Items.Count - 1; i++)
                {
                    s = s + SearchString(comProduct.Text, comStation.Items[i].ToString());
                }
            }
            else
            {
                s = SearchString(comProduct.Text, comStation.Text);
            }
            return s;
        }
        private void FrmReview_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmReviewWindow = null;
        }

        private void comProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStation();
        }

        private void Search_Click(object sender, EventArgs e) {
            string selectSql = null;
            if (comProduct.Text == "" || comStation.Text == "") {
                return;
            }
            if (!checkBox1.Checked)
            {
                if (comStation.Text.ToString().Trim() == "all")
                {
                    MessageBox.Show(s7);
                    return;
                }
                else {
                    selectSql = searchAsSingle(comStation.Text, "productCode", "time");
                    if (selectSql == null) { return; }
                    if (comResult.Text == s2)
                    {
                        selectSql = selectSql + " and " + comStation.Text + ".result != 2";
                    }
                    else if (comResult.Text == s3)
                    {
                        selectSql = selectSql + " and " + comStation.Text + ".result = 2";
                    }
                }
            }
            else
            {
                if (comStation.Text.ToString().Trim() == "all")
                {
                    if (sender.Equals(btnSearch))
                    {
                        selectSql = searchAllStation("code");
                    }
                    else if (sender.Equals(button1))
                    {
                        selectSql = searchAllStation("time");
                    }
                    if (selectSql == null)
                    {
                        MessageBox.Show(s4);
                        return;
                    }
                }
                else
                {
                    if (comStation.Text.Equals("OP60"))
                    {
                        if (sender.Equals(btnSearch))
                        {
                            selectSql = search(comStation.Text, "fixedEndCode", "");
                        }
                        else if (sender.Equals(button1))
                        {
                            selectSql = search(comStation.Text, "fixedEndCode", "time");
                        }

                    }
                    else if (comStation.Text.Equals("OP70") || comStation.Text.Equals("OP80"))
                    {
                        if (sender.Equals(btnSearch))
                        {
                            selectSql = search(comStation.Text, "innerCode", "");
                        }
                        else if (sender.Equals(button1))
                        {
                            selectSql = search(comStation.Text, "innerCode", "time");
                        }
                    }
                    else if (comStation.Text.Equals("OP40"))
                    {
                        selectSql = search(comStation.Text, "printCode", "");
                    }
                    else
                    {
                        selectSql = search(comStation.Text, "productCode", "");
                    }
                    if (comResult.Text == s2)
                    {
                        selectSql = selectSql + " and " + comStation.Text + ".result != 2";
                    }
                    else if (comResult.Text == s3)
                    {
                        selectSql = selectSql + " and " + comStation.Text + ".result = 2";
                    }
                }
            }
            
            DataTable dt = DBHelper.GetDataTable(selectSql, 1);
            dataGridView.DataSource = dt;
        }
        private string searchAllStation(string searchCondition)
        {
            string s = "select ";
            List<string> list;
            string stationSql = "select stationName from reviewdata where productName = '" + comProduct.Text + "'";
            list = DBHelper.selectData(stationSql);
            if (list.Count == 0) { return null; }
            for (int j = 0; j < list.Count; j++)
            {
                string sql;
                List<string> dataName = new List<string>();
                sql = "select * from reviewdata where productName = '" + comProduct.Text + "' and stationName= '" + list[j] + "'";
                List<string> reviewdata = DBHelper.reviewDataExecuterReader(dataName, sql, 3);
                for (int i = 0; i < reviewdata.Count; i++)
                {
                    if (j == 0 && i == 0) {
                        s = s + "fin.time as 'время' , fin.printCode AS 'Штрихкодпродукта', fin.result AS 'результат' , ";
                    }
                    else if (j == list.Count - 1 && i == reviewdata.Count - 1)
                    {
                        s = s + "" + list[j] + "." + dataName[i] + " as '" + reviewdata[i] + "' ";
                        break;
                    }
                    s = s + "" + list[j] + "." + dataName[i] + " as '" + reviewdata[i] + "',";
                }
            }
            s = s + "from ";
            for (int l = 0; l < list.Count; l++)
            {
                if (l == 0)
                {
                    s = s + "finalprintcode as fin join " + list[l] + " on  fin.productCode = " + list[l] + ".productCode join ";
                }
                else if (l == list.Count - 1)
                {
                    s = s + list[l] + " on " + list[l - 1] + ".productCode = " + list[l] + ".productCode ";
                    break;
                }
                else if (list[l].Equals("OP100"))
                {
                    s = s + list[l] + " on " + list[l] + ".productCode = fin.fixedEndCode join ";
                }
                else if (list[l].Equals("OP110"))
                {
                    s = s + list[l] + " on " + list[l] + ".productCode = fin.innerCode join ";
                }
                else
                {
                    s = s + list[l] + " on " + list[l - 1] + ".productCode = " + list[l] + ".productCode join ";
                }
            }
            if (searchCondition.Equals("time"))
            {
                s = s + "where fin.time between '" + dTP1.Value.AddHours(0).ToString("yyyy-MM-dd-HH:mm") + "' and '" + dTP2.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd-HH:mm") + "' and fin.productName = '" + comProduct.Text + "'";
            }
            else if (searchCondition.Equals("code"))
            {
                s = s + "where fin.printCode = '" + productCode.Text + "' and fin.productName = '" + comProduct.Text + "'";
            }
            return s;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            hideTheControl();
        }
        private void hideTheControl() {
            if (!checkBox1.Checked)
            {
                label2.Visible = false;
                productCode.Visible = false;
                btnSearch.Visible = false;
            }
            else
            {
                label2.Visible = true;
                productCode.Visible = true;
                btnSearch.Visible = true;
            }
        }
        private string search(string stationName , string code , string searchCondition) {
            string selectReviewDataSql = "select * from reviewdata where productName = '" + comProduct.Text + "' and stationName = '" + comStation.Text + "'";
            List<string> dataName = new List<string>();
            List<string> reviewdata = DBHelper.reviewDataExecuterReader(dataName, selectReviewDataSql, 3);
            string selectProDataSql = "select fin.time as 'время' , fin.productName as 'Названиепродукта' , fin.printCode as 'Штрихкодпродукта'";
            for (int i = 0; i < reviewdata.Count; i++)
            {
                if (i == reviewdata.Count - 1)
                {
                    selectProDataSql = selectProDataSql + " , " + stationName + "." + dataName[i] + " as '" + reviewdata[i] + "', " + stationName + ".result as 'результат' " + "from finalprintCode as fin join " + stationName;
                }
                else
                {
                    selectProDataSql = selectProDataSql + " , " + stationName + "." + dataName[i] + " as '" + reviewdata[i] + "'";
                }
            }
            if (searchCondition.Equals("time"))
            {
                selectProDataSql = selectProDataSql + "where fin.time between '" + dTP1.Value.Date.AddHours(0).ToString("yyyy-MM-dd-HH:mm") + "' and '" + dTP2.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd-HH:mm") + "' and fin.productName = '" + comProduct.Text + "'";
            }
            else
            {
                selectProDataSql = selectProDataSql + " on fin." + code + " = " + stationName + ".productCode where fin.printCode = '" + productCode.Text + "'";
            }
            return selectProDataSql;
        }
        private string searchAsSingle(string stationName, string code, string searchCondition)
        {
            string selectReviewDataSql = "select * from reviewdata where productName = '" + comProduct.Text + "' and stationName = '" + comStation.Text + "'";
            List<string> dataName = new List<string>();
            List<string> reviewdata = DBHelper.reviewDataExecuterReader(dataName, selectReviewDataSql, 3);
            if (reviewdata.Count == 0) {
                MessageBox.Show(s4);
                return null;
            }
            string selectProDataSql = "select time as 'время' , productName as 'Названи епродукта' , productCode as 'Штрихкод продукта'";
            for (int i = 0; i < reviewdata.Count; i++)
            {
                if (i == reviewdata.Count - 1)
                {
                    selectProDataSql = selectProDataSql + " , " + stationName + "." + dataName[i] + " as '" + reviewdata[i] + "', result as 'результат' " + "from " + stationName;
                }
                else
                {
                    selectProDataSql = selectProDataSql + " , "+stationName + "." + dataName[i] + " as '" + reviewdata[i]+"'";
                }
            }
            selectProDataSql = selectProDataSql + " where time between '" + dTP1.Value.Date.AddHours(0).ToString("yyyy-MM-dd-HH:mm") + "' and '" + dTP2.Value.Date.AddHours(23).AddMinutes(59).ToString("yyyy-MM-dd-HH:mm") + "' and productName = '" + comProduct.Text + "'";
            
            return selectProDataSql;
        }
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            ExportDGVToExcel exportDataToExcel = new ExportDGVToExcel();
            int ret = exportDataToExcel.ExportExcel("生产数据", dataGridView, dialog);
            if (ret == 0)
            {
                MessageBox.Show(s5);
            }
            else if (ret != 100)
            {
                MessageBox.Show(s6);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dTP1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
