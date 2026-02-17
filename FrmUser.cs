using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmUser : Form
    {
        private string rowIndex = "";
        string s1, s2, s3, s4, s5, s6, s7, s8, s9;
        private ResourceManager resManager;
        private CultureInfo currentCulture;
        public FrmUser(ResourceManager resManager, CultureInfo currentCulture)
        {
            InitializeComponent();
            this.resManager = resManager;
            this.currentCulture = currentCulture;

            UpdateLanguage(currentCulture);
        }
        private void UpdateLanguage(CultureInfo culture)
        {
            // 更新当前文化信息
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (currentCulture.Name == "zh-CN")
            {
                label1.Location = new System.Drawing.Point(77, 21);
                label2.Location = new System.Drawing.Point(89, 46);
                label4.Location = new System.Drawing.Point(89, 73);

                txtName.Location = new System.Drawing.Point(152, 17);
                txtPwd.Location = new System.Drawing.Point(152, 43);
                comLevel.Location = new System.Drawing.Point(152, 68);

                s1 = "操作员";
                s2 = "管理员";
                s3 = "删除成功!";
                s4 = "用户名重复，请核对!";
                s5 = "修改成功!";
                s6 = "请选择用户权限！";
                s7 = "请输入用户名！";
                s8 = "请输入密码！";
                s9 = "添加成功!";
            }
            else if (currentCulture.Name == "ru-RU")
            {
                label1.Location = new System.Drawing.Point(13, 22);
                label2.Location = new System.Drawing.Point(127, 46);
                label4.Location = new System.Drawing.Point(115, 72);

                txtName.Location = new System.Drawing.Point(226, 17);
                txtPwd.Location = new System.Drawing.Point(226, 43);
                comLevel.Location = new System.Drawing.Point(227, 68);

                s1 = "Оператор";
                s2 = "Администратор";
                s3 = "Удалено";
                s4 = "Имя пользователя уже используется";
                s5 = "Изменено";
                s6 = "Выберите права доступа";
                s7 = "Введите имя пользователя";
                s8 = "Введите пароль";
                s9 = "Добавлено";
            }
            // 更新窗体文本
            this.Text = resManager.GetString("MenuItemPerson");
            label1.Text = resManager.GetString("Ulabel1") + ":";
            label2.Text = resManager.GetString("Ulabel2") + ":";
            label4.Text = resManager.GetString("Ulabel4") + ":";
            btnAdd.Text = resManager.GetString("UbtnAdd");
            btnEdit.Text = resManager.GetString("UbtnEdit");
            btnDelete.Text = resManager.GetString("UbtnDelete");

            dataGridUser.Columns[0].HeaderText = resManager.GetString("plabel3");
            dataGridUser.Columns[1].HeaderText = resManager.GetString("Ulabel1");
            dataGridUser.Columns[2].HeaderText = resManager.GetString("Ulabel2");
            dataGridUser.Columns[3].HeaderText = resManager.GetString("Ulabel4");
        }
        private void showUser()
        {
            string sql = "select * from UserTable";
            DataTable dt = DBHelper.GetDataTable(sql, 1);
            dataGridUser.AutoGenerateColumns = false;//不允许自动添加列，手动已添加，并且设置DataPropertyName与要显示的字段名称一致，该语句要在指定数据源的语句之前
            dataGridUser.DataSource = dt;
        }

        private bool checkUser(string username)
        {
            bool LogOK=false;
            string sql = "select * from userTable where userName=@name";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("name",username.ToString().Trim()),
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LogOK = true;
            }
            return LogOK;
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            showUser();
            comLevel.Items.Add(s1);
            comLevel.Items.Add(s2);
            comLevel.SelectedIndex = 0;
        }

        private void dataGridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            rowIndex =dataGridUser.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dataGridUser.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPwd.Text = dataGridUser.Rows[e.RowIndex].Cells[2].Value.ToString();
            comLevel.SelectedIndex = int.Parse(dataGridUser.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string sql = "delete  from userTable where Id=@rowindex";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("rowindex",rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i>0)
            {
                MessageBox.Show(s3);
                showUser();
            }
        }

        private void FrmUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.frmUserWindow = null;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string sql;
            bool LogOK;
            LogOK = checkUser(txtName.Text.ToString().Trim());
            if (LogOK)
            {
                MessageBox.Show(s4);
                return;
            }
            sql = "update userTable set ";
            sql = sql + "userName=@name" + ",";
            sql = sql + "userPwd=@pwd" + ",";
            sql = sql + "userLevel=@level";
            sql = sql + " where Id=@rowindex";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("name",txtName.Text.ToString().Trim()),
                new MySqlParameter("pwd", txtPwd.Text.ToString().Trim()),
                new MySqlParameter("level", comLevel.SelectedIndex),
                new MySqlParameter("rowindex", rowIndex)
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MessageBox.Show(s5);
                showUser();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool LogOK = false;
            string sql;
            if (comLevel.SelectedIndex < 0)
            {
                MyTools.Show(s6);
                return;
            }
            if (txtName.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show(s7);
                return;
            }
            if (txtPwd.Text.ToString().Trim().Length <= 0)
            {
                MyTools.Show(s8);
                return;
            }
            LogOK = checkUser(txtName.Text.ToString().Trim());
            if (LogOK)
            {
                MessageBox.Show(s4);
                return;
            }

            sql = "insert  into userTable (userName,userPwd,userLevel) values (";
            sql = sql + "@name" + ",";
            sql = sql + "@pwd" + ",";
            sql = sql + "@level";
            sql = sql + ")";
            MySqlParameter[]  pms = new MySqlParameter[]
            {
                new MySqlParameter("name",txtName.Text.ToString().Trim()),
                new MySqlParameter("pwd", txtPwd.Text.ToString().Trim()),
                new MySqlParameter("level", (int)comLevel.SelectedIndex),
            };
            int i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            if (i > 0)
            {
                MessageBox.Show(s9);
                showUser();
            }
        }
    }
}
