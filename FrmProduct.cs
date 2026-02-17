using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Review
{
    public partial class FrmProduct : Form
    {

        private string rowIndex = "";
        private int gridRowIndex;
        private int gridColIndex;
        private int productType;
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        private List<String> list = new List<string>();
        string s1,s2,s3,s4,s5,s6,s7,s8,s9,s10,s11,s12;
        public FrmProduct(ResourceManager resManager , CultureInfo currentCulture)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;

            UpdateLanguage(currentCulture);
        }
        public FrmProduct(int productType)
        {
            InitializeComponent();
            this.productType = productType;
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN") {
                label1.Location = new System.Drawing.Point(37, 21);
                label3.Location = new System.Drawing.Point(161, 21);
                label2.Location = new System.Drawing.Point(332, 20);
                label4.Location = new System.Drawing.Point(463, 20);
                s1 = "无法更改此项内容!";
                s2 = "请选择要修改产品的工位！";
                s3 = "工序设置不能跳跃且不能重复，请检查工序设置是否合适！";
                s4 = "修改成功!";
                s5 = "请输入生产线名！";
                s6 = "请输入生产线编号！";
                s7 = "生产线名重复，请重新输入！";
                s8 = "生产线编号重复，请重新输入！";
                s9 = "添加成功!";
                s10 = "请注意，删除产品后，以前记录的数据都会删除掉";
                s11 = "只能从最后一个工位删起!";
                s12 = "删除成功!";
            }
            else if(currentCulture.Name == "ru-RU") {
                label1.Location = new System.Drawing.Point(8, 20);
                label3.Location = new System.Drawing.Point(161, 21);
                label2.Location = new System.Drawing.Point(261, 19);
                label4.Location = new System.Drawing.Point(432, 19);
                s1 = "Невозможно изменить";
                s2 = "Выберите продукт какой станции необходимо откорректировать";
                s3 = "Настройки стадии производственного процесса нельзя пропустить и они не должны совпадать с уже имеющимися. Проверьте настройки.";
                s4 = "Изменено";
                s5 = "Введите наименование линии";
                s6 = "Введите код линии";
                s7 = "Данное наименование линии уже используется";
                s8 = "Данный код линии уже используется";
                s9 = "Добавлено";
                s10 = "Внимание, после удаления продукта все связанные с ним данные будут удалены";
                s11 = "Удаление возможно только с последней станции";
                s12 = "Удалено";
            }
            // 更新窗体文本
            this.Text = resManager.GetString("MenuItemProduct");
            label1.Text = resManager.GetString("plabel1") + ":";
            label3.Text = resManager.GetString("plabel3") + ":";
            label2.Text = resManager.GetString("plabel2") + ":";
            label4.Text = resManager.GetString("plabel4");
            btnAdd.Text = resManager.GetString("pbtnAdd");
            btnEdit.Text = resManager.GetString("pbtnEdit");
            btnDelete.Text = resManager.GetString("pbtnDelete");
            checkBox.Text = resManager.GetString("pcheckBox");

            dataGridProduct.Columns[1].HeaderText = resManager.GetString("plabel1");
            dataGridProduct.Columns[2].HeaderText = resManager.GetString("plabel3");
            dataGridProduct.Columns[3].HeaderText = resManager.GetString("plabel2") + "1";
            dataGridProduct.Columns[4].HeaderText = resManager.GetString("plabel2") + "2";
            dataGridProduct.Columns[5].HeaderText = resManager.GetString("plabel2") + "3";
            dataGridProduct.Columns[6].HeaderText = resManager.GetString("plabel2") + "4";
            dataGridProduct.Columns[7].HeaderText = resManager.GetString("plabel2") + "5";
            dataGridProduct.Columns[8].HeaderText = resManager.GetString("plabel2") + "6";
            dataGridProduct.Columns[9].HeaderText = resManager.GetString("plabel2") + "7";
            dataGridProduct.Columns[10].HeaderText = resManager.GetString("plabel2") + "8";
            dataGridProduct.Columns[11].HeaderText = resManager.GetString("plabel2") + "9";
            dataGridProduct.Columns[12].HeaderText = resManager.GetString("plabel2") + "10";
            dataGridProduct.Columns[13].HeaderText = resManager.GetString("plabel2") + "11";
            dataGridProduct.Columns[14].HeaderText = resManager.GetString("plabel2") + "12";
            dataGridProduct.Columns[15].HeaderText = resManager.GetString("plabel2") + "13";
            dataGridProduct.Columns[16].HeaderText = resManager.GetString("plabel2") + "14";
        }   
        private void ShowProduct()
        {
            string sql = "select * from Product";
            DataTable dt = DBHelper.GetDataTable(sql);
            dataGridProduct.AutoGenerateColumns = false;//不允许自动添加列，手动已添加，并且设置DataPropertyName与要显示的字段名称一致，该语句要在指定数据源的语句之前
            dataGridProduct.DataSource = dt;
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
                    if (dr[1].ToString() != "server")
                    {
                        comStation.Items.Add(dr[1]);
                    }
                }
            }
            comStation.SelectedIndex = 0;
        }
        private bool CheckColumn(string rowIndex,string columnName,string columnValue)
        {
            bool LogOK=false; 
            string sql = "select * from Product where Id= @pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",rowIndex),
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                for(int i=0;i<dr.FieldCount;i++)
                {
                    if(dr.GetName(i)== columnName)
                    {
                        break;
                    }
                    else
                    {
                        if(dr[i].ToString() == columnValue)
                        {
                            LogOK = true;
                            System.Console.Write("工位重复"  + "\r\n");
                            break;
                        }
                        else
                        {
                            if (dr[i].ToString().Length == 0)
                            {
                                //if (i != dr.FieldCount-1)
                                //{
                                //    System.Console.Write(dr.GetName(i) + "\r\n");
                                //    if (dr[i + 1].ToString().Length != 0)
                                //    {
                                        LogOK = true;
                                        System.Console.Write("空工位" + i.ToString() + "\r\n");
                                        break;
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
            return LogOK;
        }
        private void FrmProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmProductWindow = null;
        }
        private void FrmProduct_Load(object sender, EventArgs e)
        {
            ShowProduct();
            FillStation();
            for(int i = 1; i < 21; i++)
            {
                comOrder.Items.Add(i);
            }
            comOrder.Text = "1";

        }
        private void dataGridStation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex < 3)
            {
                MyTools.Show(s1);
                return;
            }
            else
            {
                comOrder.Text = (e.ColumnIndex-2).ToString();
            }
            rowIndex = dataGridProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            gridRowIndex = e.RowIndex;
            gridColIndex = e.ColumnIndex;
            txtProduct.Text  = dataGridProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtID.Text = dataGridProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            comStation.Text = dataGridProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = MyTools.CheckKey(e, "0123456789.");
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql;
            if (rowIndex.Length == 0)
            {
                MyTools.Show(s2);
                return;
            }
            if (CheckColumn(rowIndex,"station" +(int.Parse(comOrder.Text.ToString().Trim())).ToString(), comStation.Text.ToString().Trim()))
            {
                MyTools.Show(s3);
                return;
            }

            sql = "update Product set station" + comOrder.Text.ToString() + " = '" + comStation.Text + "' where " +
                "productID = " + txtID.Text + " and productName = '" + txtProduct.Text + "'";
            int i = DBHelper.ExecuteNonQuery(sql);
            if (i > 0)
            {
                MyTools.Show(s4);
                ShowProduct();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtProduct.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show(s5);
                return;
            }
            if (txtID.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show(s6);
                return;
            }
            if (MyTools.CheckColumn("Product", "ProductName", txtProduct.Text.ToString().Trim()))
            {
                MyTools.Show(s7);
                return;
            }
            if (MyTools.CheckColumn("Product", "ProductID", txtID.Text.ToString().Trim()))
            {
                MyTools.Show(s8);
                return;
            }

            sql = "insert  into product (productName,productID,station1) values (";
            sql = sql + "@pms1" + ",";
            sql = sql + "@pms2" + ",";
            sql = sql + "@pms3";
            sql = sql + ")";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",txtProduct.Text.ToString().Trim()),
                new MySqlParameter("pms2", txtID.Text.ToString().Trim()),
                new MySqlParameter("pms3", comStation.Text.ToString().Trim()),
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                //if (DBHelper.CreatNewTable(txtProduct.Text.ToString().Trim(), 14) == -1)
                    MyTools.Show(s9);
                    ShowProduct();
                
            }
        }

        private void findTheOpenStation(string productName)
        {
            string sql = "select * from Product where productName = '" + productName + "'";
            MySqlDataReader sdr = DBHelper.ExecuteReader(sql);

            while (sdr.Read())
            {
                for (int i = 3; i < sdr.FieldCount; i++)
                {
                    if (!sdr.IsDBNull(i) && !sdr.GetValue(i).Equals(""))
                    {
                        list.Add((string)sdr.GetValue(i));
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql;
            MySqlParameter[] pms;
            if (checkBox.Checked)
            {
                if (MyTools.Show(s10, true) == 2)
                {
                    return;
                }
                sql = "delete  from Product where Id=@pms1";
                pms = new MySqlParameter[]
                {
                     new MySqlParameter("pms1",rowIndex)
                };

                //删除产品表后，删除工位表内当前产品所以数据
                findTheOpenStation(txtProduct.Text);
                deleteStationProductData(txtProduct.Text);

            }
            else
            {
                if (dataGridProduct.Columns.Count - dataGridProduct.CurrentCell.ColumnIndex > 1)
                {
                    if (dataGridProduct.Rows[gridRowIndex].Cells[gridColIndex + 1].Value.ToString().Length != 0)
                    {
                        MyTools.Show(s11);
                        return;
                    }
                }
                sql = "update Product set ";
                sql = sql + "Station" + comOrder.Text.ToString().Trim() + "=@pms1";
                sql = sql + " where Id=@pms2";
                pms = new MySqlParameter[]
                {
                     new MySqlParameter("pms1",""),
                     new MySqlParameter("pms2",rowIndex)
                };
            }
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                if (checkBox.Checked == true)
                {
                    if (DBHelper.DeleteTable(txtProduct.Text.ToString().Trim()) == -1)
                    {
                        MessageBox.Show(s12);
                        ShowProduct();
                    }
                }
                else
                {
                    MyTools.Show(s12);
                    ShowProduct();
                }
            }
        }

        //删除工位表内当前产品数据
        private void deleteStationProductData(string productName) {
            string sql = "";
            for (int i=0;i<list.Count ;i++) {
                sql = "delete from " + list[i] + " where productName='"+ productName + "'";
                DBHelper.ExecuteNonQuery(sql,0,null);
            }

        }

        private void txtProduct_TextChanged(object sender, EventArgs e)
        {

        }

        private void comOrder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
