using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Review.UI;
using Review.Utils;

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

            try
            {
                DbProfileResolver.ValidateOrThrow();
                LogFile.Operationlog.Info(
                    $"DB profile '{DbProfileResolver.GetActiveProfile()}' resolved via key '{DbProfileResolver.GetConnectionStringKey()}'.");
            }
            catch (Exception ex)
            {
                const string startupError =
                    "Database profile configuration error. Check App.config keys DbProfile/connectMySql_prod/connectMySql_test.";
                try
                {
                    LogFile.Errorlog.Error(startupError, ex);
                }
                catch
                {
                }
                MessageBox.Show(startupError + "\r\n" + ex.Message, "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
