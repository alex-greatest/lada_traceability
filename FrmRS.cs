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
    public partial class FrmRS : Form
    {
        public FrmRS()
        {
            InitializeComponent();
        }

        private void FrmRS_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmRSWindow = null;
        }

        private void txtDataBits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = MyTools.CheckKey(e, "0123456789");
        }

        private void FrmRS_Load(object sender, EventArgs e)
        {
            comName.Items.Clear();
            for (int i = 1; i < 10; i++)
            {
                comName.Items.Add("COM" + i.ToString());
            }
            comBaudRate.Items.Add("1200");
            comBaudRate.Items.Add("2400");
            comBaudRate.Items.Add("9600");
            comBaudRate.Items.Add("19200");
            comBaudRate.Items.Add("38400");
            comBaudRate.Items.Add("57600");
            comBaudRate.Items.Add("115200");
            comBaudRate.Items.Add("230400");

            comStopBits.Items.Add("0");
            comStopBits.Items.Add("1");
            comStopBits.Items.Add("2");
            comStopBits.Items.Add("3");

            comParity.Items.Add("0");
            comParity.Items.Add("1");
            comParity.Items.Add("2");
            comParity.Items.Add("3");
            comParity.Items.Add("4");

            string sql = "select * from RS where Id=@pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1","1")
            };
            DataTable dt = DBHelper.GetDataTable(sql, 0, pms);
            comName.Text = dt.Rows[0].ItemArray[1].ToString();
            comBaudRate.Text = dt.Rows[0].ItemArray[2].ToString();
            txtDataBits.Text = dt.Rows[0].ItemArray[3].ToString();
            comStopBits.Text = dt.Rows[0].ItemArray[4].ToString();
            comParity.Text = dt.Rows[0].ItemArray[5].ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql;
            if (comName.ToString().Trim().Length == 0)
            {
                MyTools.Show("请设置串口名！");
                return;
            }
            if (comBaudRate.ToString().Trim().Length == 0)
            {
                MyTools.Show("请设置串口波特率！");
                return;
            }
            if (txtDataBits.ToString().Trim().Length == 0)
            {
                MyTools.Show("请设置串口数据位！");
                return;
            }
            sql = "update RS set ";
            sql = sql + "name=@pms1" + ",";
            sql = sql + "baudRate=@pms2" + ",";
            sql = sql + "dataBits=@pms3" + ",";
            sql = sql + "stopBits=@pms4" + ",";
            sql = sql + "parity=@pms5";
            sql = sql + " where Id=@pms6";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",comName.Text.ToString().Trim()),
                new MySqlParameter("pms2",comBaudRate.Text.ToString().Trim()),
                new MySqlParameter("pms3",txtDataBits.Text.ToString().Trim()),
                new MySqlParameter("pms4",comStopBits.Text.ToString().Trim()),
                new MySqlParameter("pms5",comParity.Text.ToString().Trim()),
                new MySqlParameter("pms6","1")
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MyTools.Show("修改成功!");
            }
        }
    }
}
