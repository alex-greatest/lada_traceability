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
    public partial class FrmGroup : Form
    {
        private string rowIndex = "";
        public FrmGroup()
        {
            InitializeComponent();
        }

        private void FillcomGroup()
        {
            comGroup.Items.Clear();
            comGroup.Text = "";
            string sql = "select * from Operator";
            MySqlParameter[] pms = new MySqlParameter[] { };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                if (comGroup.Items.Contains(dr[1]) == false)
                {
                    comGroup.Items.Add(dr[1]);
                    if (comGroup.Text.Length == 0)
                    {
                        comGroup.Text = dr[1].ToString();
                    }
                    
                }
                Console.Write(dr[1] + "\r\n");
            }
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
                    if (dr[1].ToString() != "Server")
                    {
                        comStation.Items.Add(dr[1]);
                    }
                }
            }
            comStation.SelectedIndex = 0;
        }
        private bool checkUser(string username,string station)
        {
            bool LogOK = false;
            string sql = "select * from Operator where leader=@name and station=@st";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("name",username.ToString().Trim()),
                new MySqlParameter("st",station.ToString().Trim()),
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LogOK = true;
            }
            return LogOK;
        }
        private void FrmGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmGroupWindow = null;
        }

        private void showOperator(string leadername)
        {
            string sql = "select * from Operator where leader=@ln";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("ln",leadername)
            };
            DataTable dt = DBHelper.GetDataTable(sql, 0,pms);
            dataGridOperator.AutoGenerateColumns = false;//不允许自动添加列，手动已添加，并且设置DataPropertyName与要显示的字段名称一致，该语句要在指定数据源的语句之前
            dataGridOperator.DataSource = dt;
        }

        private void dataGridOperator_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            rowIndex = dataGridOperator.Rows[e.RowIndex].Cells[0].Value.ToString();
            comStation.Text = dataGridOperator.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtOperator.Text = dataGridOperator.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void FrmGroup_Load(object sender, EventArgs e)
        {
            FillcomGroup();
            FillStation();
            showOperator(comGroup.Text);
        }

        private void comGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            showOperator(comGroup.Text);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "delete  from Operator where Id=@pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MessageBox.Show("删除成功!");
                showOperator(comGroup.Text);
                FillcomGroup();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql;
            if (rowIndex.Length == 0)
            {
                MyTools.Show("请选择要修改的工位！");
                return;
            }
            sql = "update Operator set ";
            sql = sql + "operator=@pms1";
            sql = sql + " where Id=@pms2";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",txtOperator.Text.ToString().Trim()),
                new MySqlParameter("pms2",rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MyTools.Show("修改成功!");
                showOperator(comGroup.Text);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool LogOK;
            string sql;
            if (txtOperator.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show("请输入操作员！");
                return;
            }
            LogOK = checkUser(comGroup.Text.ToString().Trim(),comStation.Text.ToString().Trim());
            if (LogOK)
            {
                MessageBox.Show("该工位已有操作员，请核对!");
                return;
            }

            sql = "insert  into Operator (leader,station,operator) values (";
            sql = sql + "@pms1" + ",";
            sql = sql + "@pms2" + ",";
            sql = sql + "@pms3";
            sql = sql + ")";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comGroup.Text.ToString().Trim()),
                new MySqlParameter("pms2", comStation.Text.ToString().Trim()),
                new MySqlParameter("pms3", txtOperator.Text.ToString().Trim()),
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MyTools.Show("添加成功!");
                showOperator(comGroup.Text);
            }
        }
    }
}
