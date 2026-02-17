using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Review.UI;
namespace Review
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogIn frmLogin = new FrmLogIn();
            frmLogin.ShowDialog();
            if(frmLogin.LoginFlag)
            {
                Application.Run(new FrmMain(frmLogin.userName,frmLogin.level)) ;
                //Application.Run(new FrmVerifySCode()) ;
                //Application.Run(new FrmStandCode()) ;
            }
        }
    }
}
