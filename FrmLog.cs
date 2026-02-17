using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public partial class FrmLog : Form
    {
        public bool LoginFlag; //登录成功
        public string userName;//用户名
        public string userPwd; //密码
        public int level;      //用户等级
        public FrmLog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            userName = txtUser.Text.ToString().Trim();
            userPwd = txtPwd.Text.ToString().Trim();
            LoginFlag = DBHelper.UserlogIn(userName, userPwd, ref level);
            if (LoginFlag)
            {
                System.Console.Write("登录成功" + "\r\n");
                this.Dispose();
                //this.Close();
            }
            else
            {
                MessageBox.Show(this, "登录失败!，请核对用户名和密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
