using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Review
{
    public partial class FrmLogIn : Form
    {
        public bool LoginFlag; //登录成功
        public string userName;//用户名
        public string userPwd; //密码
        public int level;      //用户等级
        public FrmLogIn()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();        // 通知winform消息循环退出。会在所有前台线程退出后，退出应用，比较而言，下面的更狠一些
            //System.Environment.Exit(0);//立即终止当前进程，应用程序即强制退出,参数0:程序正常运行推出;1:程序非正常运行推出

        }
        private void btnLogin_Click(object sender, EventArgs e)
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
                MyTools.Show("登录失败"+ "\r\n");
            }
        }
        private void FrmLogIn_MouseClick(object sender, MouseEventArgs e)
        {
            System.Console.Write(this.Width + "//" + this.Height + "\r\n");
            var primaryScreen = Screen.PrimaryScreen;
            Rectangle rc = primaryScreen.Bounds;
            Point p = e.Location;
            string X = p.X.ToString();
            string y = p.Y.ToString();
         //   System.Console.Write(rc.Width + "//" + rc.Height + "\r\n");
         //   txtUser.Text = this.Width + "//" + this.Height;
         //   txtPwd.Text = X + "//" + y;
        }
    }
}
