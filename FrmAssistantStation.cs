using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmAssistantStation : Form
    {
        public FrmAssistantStation()
        {
            InitializeComponent();
            showProduction();
            showAssistantStation();
        }

        private void mainLabel_Click(object sender, EventArgs e)
        {

        }
        private void showProduction() {
            string showpro = "select distinct productName from Product";
            MySqlDataReader dataReader = DBHelper.ExecuteReader(showpro);
            while (dataReader.Read()) {
                //初始化产品
                productionComBox.Items.Add(dataReader[0]);
            }
        }
        private void showmainStation()
        {
            string showpro = "select * from Product where productName = @product";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("product",productionComBox.Text),
            };
            MySqlDataReader dataReader = DBHelper.ExecuteReader(showpro , 0 , pms);
            while (dataReader.Read())
            {
                //校验不为NULL和空格
                for (int i = 3; i < dataReader.FieldCount; ++i) {
                    if (!dataReader.IsDBNull(i) && ! dataReader.GetValue(i).Equals("")) {
                        //初始化主工序
                        mainComboBox.Items.Add(dataReader[i]);
                    }
                }
            }
        }
        private void showAssistantStation()
        {
            //初始化副工序下拉框
            string[] strings = { "OP1010", "OP1020", "OP1030", "OP2010", "OP2020", "OP2030", "OP3010" , "OP3020", "OP3030", };
            for (int i = 0; i < strings.Length; ++i) {
                assisCB1.Items.Add(strings[i]);
                assisCB2.Items.Add(strings[i]);
                assisCB3.Items.Add(strings[i]);
                assisCB4.Items.Add(strings[i]);
                assisCB5.Items.Add(strings[i]);
            }
        }

        private void productionComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainComboBox.Items.Clear();
            showmainStation();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //校验产品主工序
            string sesql = "select * from AssistantStation where production = '"+ productionComBox.Text.Trim() + "' and mainstation = '"+ mainComboBox.Text.Trim() + "' ";
            MySqlDataReader reader = DBHelper.ExecuteReader(sesql);
            //校验主工序是否存在，副工序是否重复
            if (reader.HasRows == false && dataCheck())
            {
                string sql = "insert  into AssistantStation (production , mainstation , assistation1 , assistation2 , assistation3 , assistation4 , assistation5) values (";
                sql = sql + "@pms1" + ",";
                sql = sql + "@pms2" + ",";
                sql = sql + "@pms3" + ",";
                sql = sql + "@pms4" + ",";
                sql = sql + "@pms5" + ",";
                sql = sql + "@pms6" + ",";
                sql = sql + "@pms7";
                sql = sql + ")";
                MySqlParameter[] pms = new MySqlParameter[]
                {
                    //下拉框数据初始化至sql数组内
                    new MySqlParameter("pms1",productionComBox.Text.Trim()),
                    new MySqlParameter("pms2", mainComboBox.Text.Trim()),
                    new MySqlParameter("pms3", assisCB1.Text.Trim()),
                    new MySqlParameter("pms4", assisCB2.Text.Trim()),
                    new MySqlParameter("pms5", assisCB3.Text.Trim()),
                    new MySqlParameter("pms6", assisCB4.Text.Trim()),
                    new MySqlParameter("pms7", assisCB5.Text.Trim()),
                };
                MySqlDataReader sqlDataReader = DBHelper.ExecuteReader(sql, 0, pms);
                MessageBox.Show("添加成功");
                MyTools.clearText(this);
                showAllStation();
            }
            else if(reader.HasRows == true){
                MessageBox.Show("该产品主工序已存在");
            }
            MyTools.clearText(this);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            var redatatable = (DataTable)dataGridViewAssiSta.DataSource;
            DBHelper.updateDataTable(redatatable, productionComBox.Text, mainComboBox.Text);
            MyTools.Show("更新成功");
            showAllStation();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgButton = MessageBoxButtons.YesNo;
            DialogResult dr = MessageBox.Show("是否确认删除", "提示", msgButton);
            if (dr == DialogResult.Yes)
            {
                string desql = "delete from AssistantStation where production = '" + productionComBox.Text.Trim() + "' and mainstation = '" + mainComboBox.Text.Trim() + "' ";
                DBHelper.ExecuteReader(desql);
                MessageBox.Show("删除成功");
            }
            MyTools.clearText(this);
            showAllStation();
        }
        private void showAllStation() {
            //初始化工序表至控件
            string sesql = "select * from AssistantStation";
            DataTable dt = DBHelper.GetDataTable(sesql);
            dataGridViewAssiSta.DataSource = dt;
        }

        private void FrmAssistantStation_Load(object sender, EventArgs e)
        {
            showAllStation();
        }

        private void dataGridViewAssiSta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0){
                return;
            }
            productionComBox.Text = dataGridViewAssiSta.Rows[e.RowIndex].Cells[1].Value.ToString();
            mainComboBox.Text = dataGridViewAssiSta.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private bool dataCheck() {
            string[] s = new string[] {assisCB1.Text , assisCB2.Text , assisCB3.Text , assisCB4.Text , assisCB5.Text };
            Array.Sort(s);
            for (int i = 0; i < s.Length - 1; ++i) {
                if (s[i].Equals(s[i + 1]) && !s[i].Equals(""))
                {
                    MessageBox.Show("副工位不允许重复");
                    return false;
                }
            }
            return true;
        }
    }
}
