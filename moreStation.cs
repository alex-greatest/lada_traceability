using MySql.Data.MySqlClient;
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

namespace Review
{
    public partial class moreStation : Form
    {
        int column = 0;
        public moreStation()
        {
            InitializeComponent();
            showProduct();
            showAssistantStation();
        }
        private void showmainStation()
        {
            string showpro = "select * from Product where productName = '"+ productComboBox.Text +"'";
            MySqlDataReader dataReader = DBHelper.ExecuteReader(showpro);
            while (dataReader.Read())
            {
                //校验不为NULL和空格
                for (int i = 3; i < dataReader.FieldCount; ++i)
                {
                    if (!dataReader.IsDBNull(i) && !dataReader.GetValue(i).Equals(""))
                    {
                        //初始化主工序
                        mainComboBox.Items.Add(dataReader[i]);
                    }
                }
            }
        }
        private void showProduct()
        {
            string showpro = "select distinct productName from Product";
            MySqlDataReader dataReader = DBHelper.ExecuteReader(showpro);
            while (dataReader.Read())
            {
                //初始化产品
                productComboBox.Items.Add(dataReader[0]);
            }
        }
        private void showAssistantStation()
        {
            //初始化副工序下拉框
            string[] strings = { "OP1010", "OP1020", "OP1030", "OP2010", "OP2020", "OP2030", "OP3010", "OP3020", "OP3030", };
            for (int i = 0; i < strings.Length; ++i)
            {
                otherComboBox.Items.Add(strings[i]);
            }
        }

        private void moreStation_Load(object sender, EventArgs e)
        {
            showAllStation();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string sql = "insert  into moreStation (product , mainStation , otherStation01) values (";
            sql = sql + "@pms1" + ",";
            sql = sql + "@pms2" + ",";
            sql = sql + "@pms3";
            sql = sql + ")";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                    //下拉框数据初始化至sql数组内
                    new MySqlParameter("pms1",productComboBox.Text.Trim()),
                    new MySqlParameter("pms2", mainComboBox.Text.Trim()),
                    new MySqlParameter("pms3", otherComboBox.Text.Trim()),
            };
            MySqlDataReader sqlDataReader = DBHelper.ExecuteReader(sql, 0, pms);
            MessageBox.Show("添加成功");
            MyTools.clearText(this);
            showAllStation();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string sql = "update moreStation set "+ moreDataGridView.Columns[column].Name + " = '"+ otherComboBox.Text +"' where product = '"+ productComboBox.Text +"' and mainStation = '"+ mainComboBox.Text +"'";
            int nu = DBHelper.ExecuteNonQuery(sql);
            if (nu > 0) { MessageBox.Show("修改成功"); showAllStation(); }
        }

        private void moreDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 )
            {
                return;
            }
            //textBox1.Text = moreDataGridView.Columns[e.ColumnIndex].Name;
            if (e.ColumnIndex > 2 && e.ColumnIndex != 8) {
                otherLabel.Text = moreDataGridView.Columns[e.ColumnIndex].HeaderText;
                otherComboBox.Text = moreDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                column = e.ColumnIndex;
            }
            productComboBox.Text = moreDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            mainComboBox.Text = moreDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void showAllStation()
        {
            //初始化工序表至控件
            string sql = "select * from moreStation";
            DataTable dt = DBHelper.GetDataTable(sql);
            moreDataGridView.DataSource = dt;
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainComboBox.Items.Clear();
            showmainStation();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgButton = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("是否确认删除", "提示", msgButton);
            if (dr == DialogResult.Yes)
            {
                string desql = "delete from moreStation where product = '" + productComboBox.Text.Trim() + "' and mainStation = '" + mainComboBox.Text.Trim() + "' ";
                DBHelper.ExecuteReader(desql);
                MessageBox.Show("删除成功");
            }
            MyTools.clearText(this);
            showAllStation();
        }
    }
}
