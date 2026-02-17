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
    public partial class FrmOptionSkip : Form
    {
        public string option;
        string productName;
        public FrmOptionSkip()
        {
            InitializeComponent();
        }
        public FrmOptionSkip(string productName)
        {
            InitializeComponent();
            this.productName= productName;
            FillStation();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FillStation()
        {
            string sql = "select * from Product where productName= '"+productName+"'";
            MySqlDataReader dr = DBHelper.ExecuteReader(sql);
            while (dr.Read())
            {
                for (int i = 3; i < dr.FieldCount; i++)
                {
                    if (dr[i].ToString().Length == 0)
                    {
                        break;
                    }
                    comStation.Items.Add(dr[i]);
                }
            }
        }
    }
}
