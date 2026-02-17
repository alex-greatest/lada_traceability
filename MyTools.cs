using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review
{
    public enum DateInterval
    {
        Second, Minute, Hour, Day, Week, Month, Quarter, Year
    }
    class MyTools
    {
        public static int Show(string infomation)
        {
           return (int)MessageBox.Show(infomation, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static int Show(string infomation,bool cansel)
        {
            return (int)MessageBox.Show(infomation, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
        public static bool CheckKey(KeyPressEventArgs e, string checkString)
        {
            int ascNumber;
            int charPosition;
            ascNumber = (int)((byte)e.KeyChar);
            charPosition = checkString.IndexOf(e.KeyChar.ToString());
            if (charPosition != -1)
            {
                return false;
            }
            else
            {
                if (ascNumber == 8)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #region 获取调用方法的名称
        /// <summary>
        /// 获取调用方法的名称
        /// </summary>
        /// <param name="layer">要返回的方法的层数(参数从0开始，0就是自己，1就是上一个，2就是上上一个，3就是上上上一个，如果再上面没有了的话，就会返回一个异常字符串)</param>
        /// 
        /// <returns></returns>
        public static string GetMethodName(int layer)
        {
            StackTrace st = new StackTrace();
            //return System.Reflection.MethodBase.GetCurrentMethod().Name.ToString() ;
            return st.GetFrame(layer).GetMethod().Name;
        }
        #endregion
        public static bool CheckColumn(string tableName ,string columnName, string columnValue)
        {
            bool LogOK = false;
            string sql = "select * from " + tableName + " where " + columnName + "= @pms1";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1",columnValue),
            };
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LogOK = true;
            }
            return LogOK;
        }
        #region 查找端口是否可用
        public static bool isPortAvalaible(int myPort)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (var item in ipEndPoints)
            {
                if (item.Port == myPort)
                {
                    return false;
                }
                //  Console.Write(item.ToString() + "/" + item.Port + "/" + ipEndPoints.GetLength(0) + "\r\n");
            }
            return true;
        }
        #endregion
        public static void MyWrite(string msg)
        {
            Console.Write(msg + "\r\n");
        }
        #region 检测输入的字符串是否为NUll
        public static string CheckNull(string inputData)
        {
            if (inputData == null)
            {
                return "";
            }
            else
            {
                return inputData.Trim();
            }
        }
        #endregion
        public static string MytoString(string str)
        {
            return str.ToString().Trim();
        }
        public static string NumberString(int Number)
        {
            if (Number < 10)
            {
                return "0" + Number.ToString();
            }
            else
            {
                return Number.ToString();
            }
        }
        #region 检测字符串是否符合设定的规则，规则字符串
        public static bool checkString(string inputCode, string ruleCode,string ruleChar)
        {
            bool result = false;
            int inputLength = inputCode.Length;
            int ruleLength = ruleCode.Length;
            string tempRuleChar;
            string tempInputChar;
            if (inputLength == ruleLength)
            {
                for (int i = 0; i < ruleLength; i++)
                {
                    tempRuleChar = ruleCode.Substring(i, 1);
                    tempInputChar = inputCode.Substring(i, 1);
                    if (tempRuleChar != ruleChar)  //该位置的字符需要判断
                    {
                        if (tempRuleChar == tempInputChar)  //需要判断的字符不一样
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }

            return result;
        }
        #endregion
        public static long DateDiff(DateInterval Interval, System.DateTime StartDate, System.DateTime EndDate)
        {
            long lngDateDiffValue = 0;
            System.TimeSpan TS = new System.TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (Interval)
            {
                case DateInterval.Second:
                    lngDateDiffValue = (long)TS.TotalSeconds;
                    break;
                case DateInterval.Minute:
                    lngDateDiffValue = (long)TS.TotalMinutes;
                    break;
                case DateInterval.Hour:
                    lngDateDiffValue = (long)TS.TotalHours;
                    break;
                case DateInterval.Day:
                    lngDateDiffValue = (long)TS.Days;
                    break;
                case DateInterval.Week:
                    lngDateDiffValue = (long)(TS.Days / 7);
                    break;
                case DateInterval.Month:
                    lngDateDiffValue = (long)(TS.Days / 30);
                    break;
                case DateInterval.Quarter:
                    lngDateDiffValue = (long)((TS.Days / 30) / 3);
                    break;
                case DateInterval.Year:
                    lngDateDiffValue = (long)(TS.Days / 365);
                    break;
            }
            return (lngDateDiffValue);
        }//end of DateDiff
        public static void clearText(Control ctrlTop)
        {
            if (ctrlTop.GetType() == typeof(ComboBox))
            {
                ctrlTop.Text = "";
            }
            else
            {
                foreach (Control ctrl in ctrlTop.Controls)
                    clearText(ctrl);
            }
        }
    }

}
