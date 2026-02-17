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
    public partial class FrmStation : Form
    {
        private string rowIndex = "";
        public FrmStation()
        {
            InitializeComponent();
        }
        private void ShowStation()
        {
            string sql = "select * from Station";
            DataTable dt = DBHelper.GetDataTable(sql, 1);
            dataGridStation.AutoGenerateColumns = false;//不允许自动添加列，手动已添加，并且设置DataPropertyName与要显示的字段名称一致，该语句要在指定数据源的语句之前
            dataGridStation.DataSource = dt;
        }
        private bool CheckStation(string stationName)
        {
            bool LogOK = false;
            string sql = "select * from Station where name=@pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",stationName.ToString().Trim()),
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LogOK = true;
            }
            return LogOK;
        }


        private void FrmStation_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmStationWindow = null;
        }

        private void FrmStation_Load(object sender, EventArgs e)
        {
            ShowStation();
        }

        private void dataGridStation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            rowIndex = dataGridStation.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtStaion.Text = dataGridStation.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtIP.Text = dataGridStation.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPort.Text = dataGridStation.Rows[e.RowIndex].Cells[3].Value.ToString();
            if(txtStaion.Text=="Server")
            {
                txtStaion.Enabled = false;
                btnDelete.Enabled = false;
                btnAdd.Enabled = false;
            }
            else
            {
                txtStaion.Enabled = true;
                btnDelete.Enabled = true;
                btnAdd.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MyTools.Show("请注意，删除工位后，以前记录的数据都会删除掉", true) == 2)
            {
                return;
            }
            if (txtStaion.Text == "Server")
            {
                MyTools.Show("不能删除服务器!");
                return;
            }
            if (rowIndex.Length == 0)
            {
                MyTools.Show("请选择要删除的工位！");
                return;
            }
            string sql = "delete  from Station where Id=@rowindex";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("rowindex",rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                if (DBHelper.DeleteTable(txtStaion.Text.ToString().Trim()) == -1)
                {
                    MessageBox.Show("删除成功!");
                    ShowStation();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool LogOK = false;
            string sql;
            if (txtStaion.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show("请输入工位名！");
                return;
            }
            if (txtIP.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show("请输入IP地址！");
                return;
            }
            if (txtPort.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show("请下位机端口号！");
                return;
            }
            LogOK = CheckStation(txtStaion.Text.ToString().Trim());
            if (LogOK)
            {
                MessageBox.Show("该工位已存在，请核对!");
                return;
            }
            if (DBHelper.CheckDataBaseTable(txtStaion.Text.ToString().Trim()))
            {
                MyTools.Show("已经存在数据表:"+ txtStaion.Text.ToString().Trim() + "请确认好后手动删除该数据表");
                return;
            }

            sql = "insert into Station (name,ip,port) values (";
            sql = sql + "@pms1" + ",";
            sql = sql + "@pms2" + ",";
            sql = sql + "@pms3";
            sql = sql + ")";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1", txtStaion.Text.ToString().Trim()),
                new MySqlParameter("pms2", txtIP.Text.ToString().Trim()),
                new MySqlParameter("pms3", txtPort.Text.ToString().Trim()),
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                if (DBHelper.CreatNewTable(txtStaion.Text.ToString().Trim(), 0) == -1)
                {
                    MyTools.Show("添加成功!");
                    ShowStation();
                }
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
            sql = "update Station set ";
            sql = sql + "name=@pms1" + ",";
            sql = sql + "ip=@pms2" + ",";
            sql = sql + "port=@pms3";
            sql = sql + " where Id=@pms4";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",txtStaion.Text.ToString().Trim()),
                new MySqlParameter("pms2",txtIP.Text.ToString().Trim()),
                new MySqlParameter("pms3",txtPort.Text.ToString().Trim()),
                new MySqlParameter("pms4",rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MyTools.Show("修改成功!");
                ShowStation();
            }
        }

        private void txtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = MyTools.CheckKey(e, "0123456789.");
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = MyTools.CheckKey(e, "0123456789");
        }
    }
}
