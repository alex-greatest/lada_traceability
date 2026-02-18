
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Review
{
    public class DBHelper
    {
        //readonly修饰的变量，只能在初始化的时候赋值，以及在构造函数中赋值，其他地方只能读取不能设置。
        private static readonly string ConnStr = Utils.DbProfileResolver.ResolveConnectionString();
        /// <summary>
        /// 执行 T-SQL命令，insert delete updata
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, int cmdType, params MySqlParameter[] pms)
        {
            int count = 0;
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (cmdType == 2)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                if (pms != null && pms.Length > 0)
                {
                    cmd.Parameters.AddRange(pms);
                }
                conn.Open();
                count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            return count;
        }
        public static int ExecuteNonQuery(string sql)
        {
            int count = 0;
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 5000;
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return count;
        }
        /// <summary>
        /// 执行查询，返回结果集第一行第一列的值，忽略其他行或列。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, int cmdType, params MySqlParameter[] pms)
        {
            object o = 0;
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 5000;
                if (cmdType == 2)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                if (pms != null && pms.Length > 0)
                {
                    cmd.Parameters.AddRange(pms);
                }
                conn.Open();
                o = cmd.ExecuteScalar();
                Console.Write(cmd.CommandText.ToString());
                Console.Write(o + "\r\n");
                cmd.Parameters.Clear();
                conn.Close();
            }
            return o;
        }
        public static List<string> selectData(string sql) {
            MySqlDataReader dr;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            List<string> dataList = new List<string>();
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if(dr.GetValue(0) != DBNull.Value && dr.GetValue(0).ToString() != "")
                    dataList.Add(dr.GetValue(0).ToString());
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return dataList;
        }
        public static MySqlDataReader ExecuteReader(string sql, int cmdType, params MySqlParameter[] pms)
        {
            MySqlDataReader dr = null;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (cmdType == 2)
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
            }
            if (pms != null && pms.Length > 0)
            {
                cmd.Parameters.AddRange(pms);
            }
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return dr;
        }
        public static MySqlDataReader ExecuteReader(string sql)
        {
            MySqlDataReader dr = null;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return dr;
        }
        /// <summary>
        /// 查询数据项首个字符串类型数据并返回
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ReadString(string sql)
        {
            string s = null;
            MySqlDataReader dr;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    s = Convert.ToString(dr.GetValue(0));
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return s;
        }

        /// <summary>
        /// 填充DataSet，一个或者多个都有效
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, int cmdType, params MySqlParameter[] pms)
        {
            DataSet ds = new DataSet();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (cmdType == 2)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                if (pms != null && pms.Length > 0)
                {
                    cmd.Parameters.AddRange(pms);
                }
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                conn.Open();
                da.Fill(ds);
                conn.Close();
            }
            return ds;
        }
        /// <summary>
        /// 填充DataTable  一个结果集。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, int cmdType, params MySqlParameter[] pms)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (cmdType == 2)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                if (pms != null && pms.Length > 0)
                {
                    cmd.Parameters.AddRange(pms);
                }
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            return dt;
        }
        public static List<string> stationExecuterReader(string sql , int start)
        {
            List<string> values = new List<string>();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = start; i < reader.FieldCount; i++) {
                    if (reader.GetValue(i) != null && reader.GetValue(i).ToString() != "")
                    {
                        values.Add(reader.GetValue(i).ToString());
                    }
                }
            }
            conn.Close();
            return values;
        }
        public static List<string> printCodeReader(string sql)
        {
            List<string> values = null;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                values = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++) {
                    values.Add(reader.GetValue(i).ToString());
                }
            }
            conn.Close();
            return values;
        }
        public static List<string> reviewDataExecuterReader(List<string>dataName, string sql, int start)
        {
            List<string> values = new List<string>();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = start; i < reader.FieldCount; i++)
                {
                    if (reader.GetValue(i) != null && reader.GetValue(i).ToString() != "")
                    {
                        dataName.Add(reader.GetName(i).ToString());
                        values.Add(reader.GetValue(i).ToString());
                    }
                }
            }
            conn.Close();
            return values;
        }
        public static Task<DataTable> GetDataTableAsync(string sql, int cmdType, params MySqlParameter[] pms)
        {
            DataTable dt = null;
            Task<DataTable> t = Task.Run(() =>
            {
                if (dt == null)
                {
                    dt = new DataTable();
                }

                using (MySqlConnection conn = new MySqlConnection(ConnStr))
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    if (cmdType == 2)
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    if (pms != null && pms.Length > 0)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
                return dt;
            });
            return t;
        }

        public static DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            conn.Open();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
        public static void updateDataTable(DataTable table, string pro, string main)
        {
            string sql = "select * from AssistantStation where production = '" + pro + "' and mainstation =  '" + main + "'";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            new MySqlCommandBuilder(da);
            da.Update(table);
        }
        public static bool UserlogIn(string userName, string userPwd, ref int userLevel)
        {
            bool LogOK = false;
            string sql = "select * from userTable where userName=@name and userPwd=@pwd";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                //new SqlParameter("name",SqlDbType.VarChar,50){Value="admin"},
                //new SqlParameter("pwd",SqlDbType.VarChar,50){Value="admin"}
                new MySqlParameter("name",userName),
                new MySqlParameter("pwd",userPwd)
            };
            //int s1 = (int)DBHelper.ExecuteScalar(sql, 0, pms);
            MySqlDataReader dr = DBHelper.ExecuteReader(sql, 0, pms);
            while (dr.Read())
            {
                LogOK = true;
                userLevel = int.Parse(dr[3].ToString());
            }
            return LogOK;
        }

        public static int CreatNewTable(string tableName, int Id)//该方法可能在本系统有点通用
        {
            int rows;
            string sql = "CREATE TABLE " + tableName + "("
            + "ID bigint auto_increment PRIMARY KEY,"  // 主键，且标示增量为1，字段名Id和数据类型bigint可以用[]括起来也可以不括
            + "productName varchar(20),productCode varchar(20),";    //不管是否括起来，字段名和数据类型之间都要用空格隔开
            if (Id == 0)
            {
                sql = sql + TableString(0, false);
            }
            else
            {
                for (int i = 1; i <= Id; i++)
                {
                    sql = sql + TableString(i, true);
                }
            }
            sql = sql + "result int)ENGINE=myisam";
            MyTools.MyWrite(sql);
            using (MySqlConnection conn = new MySqlConnection(ConnStr))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                rows = cmd.ExecuteNonQuery();//对于 UPDATE、INSERT 和 DELETE 语句，返回值为该命令所影响的行数。对于所有其他类型的语句，返回值为 -1
                conn.Close();
            }
            return rows;
        }
        
        public static string TableString(int Id, bool enableId)
        {
            string sql;
            string s = "";
            if (enableId)  //工位表需要加ID号了。
            {
                if (Id < 10)
                {
                    s = "0" + Id.ToString();
                }
                else
                {
                    s = Id.ToString();
                }
            }
            sql = "time" + s + " datetime,";
            for (int i = 1; i < 21; i++)
            {
                if (i < 10)
                {
                    sql = sql + "data" + "0" + i.ToString() + s + " varchar(10),";
                }
                else
                {
                    sql = sql + "data" + i.ToString() + s + " varchar(10),";
                }
            }
            for (int i = 1; i < 9; i++)
            {
                sql = sql + "barcode" + "0" + i.ToString() + s + " varchar(20),";
            }
            return sql;
        }
        public static bool CheckDataBaseTable(string tableName)
        {
            MySqlDataReader dr = GetDataBaseTables();
            bool tableOK = false;
            while (dr.Read())
            {
                if (dr.GetString(0) == tableName)
                {
                    tableOK = true;
                    break;
                }
            }
            return tableOK;
        }
        public static MySqlDataReader GetDataBaseTables()
        {
            MySqlDataReader dr = null;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            String sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询异常", ex);
            }
            return dr;
        }
        public static int DeleteTable(string tableName)
        {
            int i;
            if (CheckDataBaseTable(tableName))
            {
                string sql = "DROP TABLE " + tableName;
                using (MySqlConnection conn = new MySqlConnection(ConnStr))
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return i;
            }
            else
            {
                return -1;  //没有要删除的表，直接返回删除成功
            }
        }
        #region 俄罗斯LADA
        public static string selectPCode(string stationName , string Adress , string productCode) {
            string selectSql = "select productCode from " + stationName + " where " + Adress + " = '" + productCode + "'";
            string pCode = ReadString( selectSql );
            return pCode;
        }
        public static string selectStationProcode(string productCode, string productName , int productType) {
            string selectSql = "select productCode from stationprocode where productCode = '" + productCode + "' and productName = '" + productName + "' and productType = " + productType + "";
            string pCode = ReadString(selectSql);
            return pCode;
        }
        public static string selectFinalPrintCode(string productName , int productType , string printCode) {
            string selectSql = "select printCode from finalprintcode where productName = '" + productName + "' and productType = " + productType + " and printCode = '"+printCode+"'";
            return ReadString( selectSql );
        }
        public static int selectStationRecord(string stationName, string productCode, int productType) {
            string selectSql = "select * from " + stationName + " where productCode = '" + productCode + "' and productType = " + productType + "";
            MySqlDataReader reader = ExecuteReader(selectSql);
            return reader.FieldCount;
        }
        public static string selectScanCode(string productCode , string productName , int productType) {
            string scanCode = null;
            string selectSql = "select scanCode from finalprintCode where productCode = '" + productCode + "' and productName = '"+productName+"' and productType = "+productType+"";
            scanCode = ReadString(selectSql);
            return scanCode;
        }
        //更新产品总结果
        public static int updateProCode(int productType , string productCode , int result , string productName , string stationName) {
            string updateSql = "update stationprocode set productResult = " + result + " , stationName = '"+stationName+"' , time = '"+DateTime.Now+"' where productType = "+ productType + " and productCode = '"+ productCode + "' and productName = '"+productName+"'";
            Console.WriteLine(updateSql);
            return ExecuteNonQuery(updateSql);
        }
        //将最终总成条码，箱码存储
        public static int saveToFinalTable(int quality , string productCode, string printCode , string productName, int productType)
        {
            int result = quality == 2 ? 2 : 1;
            var codes = new List<string>();
            try
            {
                string sql1 = "select op20.productCode , op20.barcode4 , op30.barcode3 from op20 join op30 on op20.productCode = op30.productCode where op30.barcode3 = '" + productCode + "' and op30.productName = '" + productName + "' and op30.productType = " + productType + "";
                string sql2 = "select op20.productCode , op20.barcode4 , op30.barcode3 from op20 join op30 on op20.productCode = op30.productCode where op20.barcode4= '" + productCode + "' and op30.productName = '" + productName + "' and op30.productType = " + productType + "";

                codes = stationExecuterReader(sql1, 0);

                if (codes.Count == 0) {
                    codes = stationExecuterReader(sql2, 0);
                }
                if (codes.Count == 3)
                {
                    string insertSql = "insert into finalprintcode (time , productName , productType ,scanCode , printCode , productCode , fixedEndCode , innerCode , result)" +
                        "values('" + DateTime.Now + "' ,'" + productName + "' , " + productType + " , '"+""+"' , '" + printCode + "' ,'" + codes[0] + "' , '" + codes[1] + "' , '" + codes[2] + "' , " + result + ")";
                    int count = ExecuteNonQuery(insertSql);
                    //Console.WriteLine(count + "  " + insertSql);
                    return count;
                }
                else
                {
                    return -1;
                }
            }
            catch (MySqlException msqEx)
            {
                Console.WriteLine(msqEx.Message);
                return -1;
            }
        }
        public static void updateScanCode(string productCode , string productName , int productType) {
            string updateSql = "update finalprintcode set scanCode = '" + productCode + "' where printCode = '" + productCode + "' and productName = '" + productName + "' and productType = " + productType + "";
            ExecuteNonQuery(updateSql);
        }
        public static int insertStationProcode(string stationName, string productName, int productType, string productCode)
        {
            string insertSql = "insert into stationprocode (time , stationName , productType , productName , productCode , productResult) values('" + DateTime.Now + "' , '" + stationName + "' , " + productType + " , '" + productName + "', '" + productCode + "' , 0)";
            int result = ExecuteNonQuery(insertSql);
            return result;
        }
        //查找产品总结果
        public static int selectProResult(string productName , int productType , string productCode , string stationName , string codeAdress) {
            string selectSql = "select productResult from stationprocode where productName = '"+productName+"' and productType = " + productType + " and productCode = (select productCode from op20 where barcode4 = '" + productCode + "' and productName = '" + productName + "' and productType = " + productType + ") ";
            //string selectSql = "select productResult from stationprocode where productName = '"+productName+"' and productType = " + productType + " and productCode = '"+productCode+"'";
            int result = Convert.ToInt32(ReadString(selectSql));
            if (result == 0 && productCode != "") { 
            string selectCodeSql = "select productResult from stationprocode where productName = '" + productName + "' and productType = " + productType + " and productCode = (select productCode from " + stationName + " where " + codeAdress + " = '" + productCode + "' and productName = '"+productName+"' and productType = " + productType + ") ";
                result = Convert.ToInt32(ReadString(selectCodeSql));
            }
            return result;
        }
        public static int selectProResult(string productName, int productType, string productCode)
        {
            string selectSql = "select productResult from stationprocode where productName = '"+productName+"' and productType = " + productType + " and productCode = '"+productCode+"'";
            int result = Convert.ToInt32(ReadString(selectSql));
            if (result == 0 && productCode != "")
            {
                string selectCodeSql = "select productResult from stationprocode where productName = '" + productName + "' and productType = " + productType + " and productCode = (select productCode from op20 where barcode4 = '" + productCode + "' and productName = '" + productName + "' and productType = " + productType + ") ";
                result = Convert.ToInt32(ReadString(selectCodeSql));
            }
            return result;
        }
        //查找当前产品合格记录的工位信息
        public static string selectStationName(string productName, int productType, string productCode, string stationName, string codeAdress)
        {
            string selectSql = "select stationName from stationprocode where productName = '" + productName + "' and productType = " + productType + " and productCode = '" + productCode + "' ";
            var result = ReadString(selectSql);
            if (result == null)
            {
                string selectCodeSql = "select stationName from stationprocode where productName = '" + productName + "' and productType = " + productType + " and productCode = (select productCode from " + stationName + " where " + codeAdress + " = '" + productCode + "' and productName = '" + productName + "' and productType = " + productType + ") ";
                result = ReadString(selectCodeSql);
            }
            return result;
        }
        public static string Transaction(string RFID)
        {
            string procode = "null";
            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();
            //在当前数据库连接下开启事务
            MySqlTransaction transaction = conn.BeginTransaction();
            MySqlCommand cmd = conn.CreateCommand();
            cmd.Transaction = transaction;
            try
            {
                cmd.CommandText = "select productCode from stationprocode where RFID = '" + RFID + "'";
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                procode = dr.GetString(0);
                cmd.CommandText = "update stationprocode set RFID = null where RFID = '"+RFID+"'";
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return procode;
            }
            catch (Exception)
            {

                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    throw;
                }
                return "error";
            }
            finally { conn.Close(); }
        }
        public static int SaveStationData(StationData sd , int productType , string productName)
        {
            int i;
            string sql;
            sql = "insert  into " + sd.stationName + " (time,productType,productName,productCode ,data1,data2,data3,data4,data5,data6,data7,data8,data9,";
            sql = sql + " data10,data11,data12,data13,data14,data15,data16,data17,data18,data19,data20,";
            sql = sql + " barcode2,barcode3,barcode4,barcode5,barcode6,barcode7,barcode8 , result) values (";
            sql = sql + "@pms1" + ",";
            sql = sql + "@pms2" + ",";
            sql = sql + "@pms3" + ",";
            sql = sql + "@pms4" + ",";                                                                              
            sql = sql + "@pms5" + ",";
            sql = sql + "@pms6" + ",";
            sql = sql + "@pms7" + ",";
            sql = sql + "@pms8" + ",";
            sql = sql + "@pms9" + ",";
            sql = sql + "@pms10" + ",";
            sql = sql + "@pms11" + ",";
            sql = sql + "@pms12" + ",";
            sql = sql + "@pms13" + ",";
            sql = sql + "@pms14" + ",";
            sql = sql + "@pms15" + ",";
            sql = sql + "@pms16" + ",";
            sql = sql + "@pms17" + ",";
            sql = sql + "@pms18" + ",";
            sql = sql + "@pms19" + ",";
            sql = sql + "@pms20" + ",";
            sql = sql + "@pms21" + ",";
            sql = sql + "@pms22" + ",";
            sql = sql + "@pms23" + ",";
            sql = sql + "@pms24" + ",";
            sql = sql + "@pms25" + ",";
            sql = sql + "@pms26" + ",";
            sql = sql + "@pms27" + ",";
            sql = sql + "@pms28" + ",";
            sql = sql + "@pms29" + ",";
            sql = sql + "@pms30" + ",";
            sql = sql + "@pms31" + ",";
            sql = sql + "@pms32";
            sql = sql + ")";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1", DateTime.Now),
                new MySqlParameter("pms2", productType),
                new MySqlParameter("pms3", productName),
                new MySqlParameter("pms4", sd.productCode),
                new MySqlParameter("pms5", sd.result[0]),
                new MySqlParameter("pms6", sd.result[1]),
                new MySqlParameter("pms7", sd.result[2]),
                new MySqlParameter("pms8", sd.result[3]),
                new MySqlParameter("pms9", sd.result[4]),
                new MySqlParameter("pms10", sd.result[5]),
                new MySqlParameter("pms11", sd.result[6]),
                new MySqlParameter("pms12", sd.result[7]),
                new MySqlParameter("pms13", sd.result[8]),
                new MySqlParameter("pms14", sd.result[9]),
                new MySqlParameter("pms15", sd.result[10]),
                new MySqlParameter("pms16", sd.result[11]),
                new MySqlParameter("pms17", sd.result[12]),
                new MySqlParameter("pms18", sd.result[13]),
                new MySqlParameter("pms19", sd.result[14]),
                new MySqlParameter("pms20", sd.result[15]),
                new MySqlParameter("pms21", sd.result[16]),
                new MySqlParameter("pms22", sd.result[17]),
                new MySqlParameter("pms23", sd.result[18]),
                new MySqlParameter("pms24", sd.result[19]),
                new MySqlParameter("pms25", sd.barcode2),
                new MySqlParameter("pms26", sd.barcode3),
                new MySqlParameter("pms27", sd.barcode4),
                new MySqlParameter("pms28", sd.barcode5),
                new MySqlParameter("pms29", sd.barcode6),
                new MySqlParameter("pms30", sd.barcode7),
                new MySqlParameter("pms31", sd.barcode8),
                new MySqlParameter("pms32", sd.quality <= 1 ? 1 : sd.quality)
            };
            i = DBHelper.ExecuteNonQuery(sql, 0, pms);
            return i;
        }
        public static List<string> checkSingleStation(string stationName , string productCode) {
            string selectSql = "select result from " + stationName + " where productCode = '" + productCode + "'";
            return stationExecuterReader(selectSql , 0);
        }
        public static void updateStationData(StationData sd, int productType, string productName) {
            int i;
            string updateSql = "update " + sd.stationName + " set time = @pms1,productType = @pms2,productName = @pms3," +
                    "data1 = @pms5,data2 = @pms6,data3 = @pms7,data4 = @pms8,data5 = @pms9,data6 = @pms10,data7 = @pms11," +
                    "data8 = @pms12,data9 = @pms13,data10 = @pms14,data11 = @pms15,data12 = @pms16,data13 = @pms17,data14 = @pms18," +
                    "data15 = @pms19,data16 = @pms20,data17 = @pms21,data18 = @pms22,data19 = @pms23,data20 = @pms24, barcode2 = @pms25," +
                    "barcode3 = @pms26,barcode4 = @pms27,barcode5 = @pms28,barcode6 = @pms29,barcode7 = @pms30,barcode8 = @pms31 , result = @pms32 where productCode = @pms33";
            MySqlParameter[] pms = new MySqlParameter[]
            {
                new MySqlParameter("pms1", DateTime.Now),
                new MySqlParameter("pms2", productType),
                new MySqlParameter("pms3", productName),
                new MySqlParameter("pms5", sd.result[0]),
                new MySqlParameter("pms6", sd.result[1]),
                new MySqlParameter("pms7", sd.result[2]),
                new MySqlParameter("pms8", sd.result[3]),
                new MySqlParameter("pms9", sd.result[4]),
                new MySqlParameter("pms10", sd.result[5]),
                new MySqlParameter("pms11", sd.result[6]),
                new MySqlParameter("pms12", sd.result[7]),
                new MySqlParameter("pms13", sd.result[8]),
                new MySqlParameter("pms14", sd.result[9]),
                new MySqlParameter("pms15", sd.result[10]),
                new MySqlParameter("pms16", sd.result[11]),
                new MySqlParameter("pms17", sd.result[12]),
                new MySqlParameter("pms18", sd.result[13]),
                new MySqlParameter("pms19", sd.result[14]),
                new MySqlParameter("pms20", sd.result[15]),
                new MySqlParameter("pms21", sd.result[16]),
                new MySqlParameter("pms22", sd.result[17]),
                new MySqlParameter("pms23", sd.result[18]),
                new MySqlParameter("pms24", sd.result[19]),
                new MySqlParameter("pms25", sd.barcode2),
                new MySqlParameter("pms26", sd.barcode3),
                new MySqlParameter("pms27", sd.barcode4),
                new MySqlParameter("pms28", sd.barcode5),
                new MySqlParameter("pms29", sd.barcode6),
                new MySqlParameter("pms30", sd.barcode7),
                new MySqlParameter("pms31", sd.barcode8),
                new MySqlParameter("pms32", sd.quality <= 1 ? 1 : sd.quality),
                new MySqlParameter("pms33", sd.productCode)
            };
            i = DBHelper.ExecuteNonQuery(updateSql, 0, pms);
        }
        public static int updateStationData_20(StationData sd , int productType , string productName) {
            string updateSql = "update " + sd.stationName + " set data5 = '" + sd.result[4] + "' , data6 = '" + sd.result[5] + "' , data7 = '" + sd.result[6] + "' , data8 = '" + sd.result[7] + "' , data9 = '" + sd.result[8] + "' , data10 = '" + sd.result[9] + "', " +
                "barcode4  = '" + sd.barcode4 +"' , barcode5 = '" + sd.barcode5 +"', result = '"+sd.quality+"'" +
                " where productType = " + productType + " and productName = '" + productName + "' and productCode = '" + sd.productCode + "'";
            int count = DBHelper.ExecuteNonQuery(updateSql);
            return count;
        }
        public static int updateStationData_30(StationData sd, int productType, string productName)
        {
            string updateSql = "update " + sd.stationName + " set data4 = '" + sd.result[3] +"' , data5 = '" + sd.result[4] + "' , data6 = '" + sd.result[5] + "' , data7 = '" + sd.result[6] + "' , data8 = '" + sd.result[7] + "' , data9 = '" + sd.result[8] + "' , data10 = '" + sd.result[9] + "' ," +
                "barcode3 = '" + sd.barcode3 +"' , barcode4 = '" + sd.barcode4 +"' , barcode5 = '" + sd.barcode5 +"' , result = '" + sd.quality + "'" +
                " where productType = " + productType + " and productName = '" + productName + "' and productCode = '" + sd.productCode + "'";
            int count = DBHelper.ExecuteNonQuery(updateSql);
            return count;
        }
        //将plc发送的产品条码及RFID绑定
        public static int RFIDBind(int productType, string productCode, string RFID , string station) {
            List<string> RFIDList = selectRFIDList(productType , station);
            if (RFIDList.Contains(RFID)) {
                string updateSql = "update rfidbind set productCode = '" + productCode + "' where productType = " + productType + " and RFID = '" + RFID + "' and station = '"+station+"'";
                int result = ExecuteNonQuery(updateSql);
                return result;
            }
            else {
                string insertSql = "insert into rfidbind  (productType , last , productCode , RFID , station) values(" + productType + " ,'"+""+"', '" + productCode + "' , '" + RFID + "' , '"+station+"')";
                int result = ExecuteNonQuery(insertSql);
                return result;
            }
        }
        //将plc发送的RFID对应得产品条码下发
        public static string clearRFIDBind(string productCode , int productType, string RFID, string station) {
            if (productCode != null && productCode != "")
            {
                //当查找条码不为空,清空rfid对应条码
                string updateSql = "update rfidbind set last = '" + productCode + "' , productCode = '" + "" + "' where productType = " + productType + " and RFID = '" + RFID + "' and station = '" + station + "'";
                int result = ExecuteNonQuery(updateSql);
                //Console.WriteLine(result + "   " + updateSql);
            }
            return productCode;
        }
        public static string SelectRFIDBindingCode(int productType, string RFID , string station) {
            string selectSql = "select productCode from rfidbind where productType = "+productType+" and RFID = '"+RFID+"' and station = '"+station+"'";
            string productCode = ReadString(selectSql);
            return productCode;
        }
        //查找RFID列表
        public static List<string> selectRFIDList(int productType , string station) {
            string selectSql = "select RFID from rfidbind where productType = " + productType + " and station = '"+station+"'";
            List<string> list = selectData(selectSql) ;
            return list ;
        }
        //查找station列表
        public static List<string> selectStationList(int productType , string productName , int start)
        {
            string selectSql = "select * from product where productID = " + productType + " and productName = '"+productName+"'";
            List<string> list = stationExecuterReader(selectSql , start);
            return list;
        }
        //查找OP60_2是否有注油
        public static string check60HasOil(string productCode , int productType) {
            string selectSql = "select data1 from op60_2 where productType = "+productType+" and productCode = '" + productCode + "'";
            string oilData = ReadString(selectSql);
            return oilData;
        }
        public static int save80_2Data(StationData sd , string tableName , int productType ,string productName) {
                int i;
                string sql;
                sql = "insert  into " + tableName + " (time,productType,productName,productCode ,data1,data2,data3,data4,data5,data6,data7,data8,data9,";
                sql = sql + " data10,data11,data12,data13,data14,data15,data16,data17,data18,data19,data20,";
                sql = sql + " barcode2,barcode3,barcode4,barcode5,barcode6,barcode7,barcode8 , result) values (";
                sql = sql + "@pms1" + ",";
                sql = sql + "@pms2" + ",";
                sql = sql + "@pms32" + ",";
                sql = sql + "@pms3" + ",";
                sql = sql + "@pms4" + ",";
                sql = sql + "@pms5" + ",";
                sql = sql + "@pms6" + ",";
                sql = sql + "@pms7" + ",";
                sql = sql + "@pms8" + ",";
                sql = sql + "@pms9" + ",";
                sql = sql + "@pms10" + ",";
                sql = sql + "@pms11" + ",";
                sql = sql + "@pms12" + ",";
                sql = sql + "@pms13" + ",";
                sql = sql + "@pms14" + ",";
                sql = sql + "@pms15" + ",";
                sql = sql + "@pms16" + ",";
                sql = sql + "@pms17" + ",";
                sql = sql + "@pms18" + ",";
                sql = sql + "@pms19" + ",";
                sql = sql + "@pms20" + ",";
                sql = sql + "@pms21" + ",";
                sql = sql + "@pms22" + ",";
                sql = sql + "@pms23" + ",";
                sql = sql + "@pms24" + ",";
                sql = sql + "@pms25" + ",";
                sql = sql + "@pms26" + ",";
                sql = sql + "@pms27" + ",";
                sql = sql + "@pms28" + ",";
                sql = sql + "@pms29" + ",";
                sql = sql + "@pms30" + ",";
                sql = sql + "@pms31";
                sql = sql + ")";
                MySqlParameter[] pms = new MySqlParameter[]
                {
                new MySqlParameter("pms1", DateTime.Now),
                new MySqlParameter("pms2", productType),
                new MySqlParameter("pms3", sd.productCode),
                new MySqlParameter("pms4", sd.result[0]),
                new MySqlParameter("pms5", sd.result[1]),
                new MySqlParameter("pms6", sd.result[2]),
                new MySqlParameter("pms7", sd.result[3]),
                new MySqlParameter("pms8", sd.result[4]),
                new MySqlParameter("pms9", sd.result[5]),
                new MySqlParameter("pms10", sd.result[6]),
                new MySqlParameter("pms11", sd.result[7]),
                new MySqlParameter("pms12", sd.result[8]),
                new MySqlParameter("pms13", sd.result[9]),
                new MySqlParameter("pms14", sd.result[10]),
                new MySqlParameter("pms15", sd.result[11]),
                new MySqlParameter("pms16", sd.result[12]),
                new MySqlParameter("pms17", sd.result[13]),
                new MySqlParameter("pms18", sd.result[14]),
                new MySqlParameter("pms19", sd.result[15]),
                new MySqlParameter("pms20", sd.result[16]),
                new MySqlParameter("pms21", sd.result[17]),
                new MySqlParameter("pms22", sd.result[18]),
                new MySqlParameter("pms23", sd.result[19]),
                new MySqlParameter("pms24", sd.barcode2),
                new MySqlParameter("pms25", sd.barcode3),
                new MySqlParameter("pms26", sd.barcode4),
                new MySqlParameter("pms27", sd.barcode5),
                new MySqlParameter("pms28", sd.barcode6),
                new MySqlParameter("pms29", sd.barcode7),
                new MySqlParameter("pms30", sd.barcode8),
                new MySqlParameter("pms32", productName),
                new MySqlParameter("pms31", sd.quality <= 1 ? 1 : sd.quality)
                };
                i = DBHelper.ExecuteNonQuery(sql, 0, pms);
                return i;
        }
        public static List<string> GetDataList(string sql, int start)
        {
            List<string> values = null;
            MySqlConnection conn = new MySqlConnection(ConnStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(values == null) values = new List<string>();
                for (int i = start; i < reader.FieldCount; i++)
                {
                    if (!string.IsNullOrEmpty(reader.GetValue(i)?.ToString()))
                    {
                        values.Add(reader.GetValue(i).ToString());
                    }
                }
            }
            conn.Close();
            return values;
        }
        #endregion
        #region 打印条码设置
        public static List<string> selectPrintCode(string proName) {
            string selectSql = "select * from printcode where proName = '"+proName+"' ";
            var printCode = printCodeReader(selectSql); 
            return printCode;
        }
        public static List<string> initProName() {
            var selectSql = "select productName from product";
            var productIDList = selectData(selectSql);
            return productIDList;
        }
        public static void updatePrintCode(string productCode , string proName)
        {
            //productCode = (Convert.ToDouble(productCode) + 1).ToString();
            var updateSql = "update printcode set productCode = '" + productCode + "' where proName = '"+proName+"'";
            ExecuteNonQuery(updateSql);
        }
        #endregion
        #region
        public static bool checkRepetition(string rfid)
        {
            MySqlDataReader dr = null;
            string sql = "select innerId from RandomInnerId where RFID = '" + rfid + "'";
            dr = DBHelper.ExecuteReader(sql);
            bool back = dr.Read();
            return back;
        }
        public static void insertRandomId(string rfid, string random, DateTime now, string product, int productType, int quality, string stationName)
        {
            string sql = "insert into RandomInnerId (RFID , typeNum ,innerId ,product, dateAtime,qualified,stationName)  values ('" + rfid + "' ," + productType + ", '" + random + "','" + product + "', '" + now + "'," + quality + " ,'" + stationName + "')";
            DBHelper.ExecuteNonQuery(sql);
        }
        public static void updateRandomId(string rfid, string random, DateTime now, string product, int productType, int quality)
        {
            string sql = "update RandomInnerId set innerId = '" + random + "' , product = '" + product + "' ,dateAtime = '" + now + "' , typeNum = " + productType + " , qualified = " + quality + " where RFID = '" + rfid + "'";
            DBHelper.ExecuteNonQuery(sql);
        }
        public static void updateRandomIdQuality(string rfid, int quality, string stationName)
        {
            string sql = "update RandomInnerId set qualified = " + quality + " , stationName = '" + stationName + "' where RFID = '" + rfid + "'";
            DBHelper.ExecuteNonQuery(sql);
        }

        public static string selectRandomId(string rfid)
        {
            MySqlDataReader dr;
            string back = "";
            string sql = "select innerId from RandomInnerId where RFID = '" + rfid + "'";
            dr = DBHelper.ExecuteReader(sql);
            if (dr.Read())
            {
                back = dr.GetString(0);
            }
            return back;
        }
        public static int ExecuteNumber(string sql)
        {
            MySqlDataReader dr = null;
            int count = 0;
            dr = DBHelper.ExecuteReader(sql);
            while (dr.Read()) { count++; }
            return count;
        }
        public static void assistantSaveToMain(MySqlDataReader dr, string mainStation)
        {
            int count = dr.FieldCount;
            while (dr.Read())
            {
                string sql = "insert  into " + mainStation + " (production,productionId,operator,dateATime,data01,data02,data03,data04,data05,data06,data07,data08,data09,";
                sql = sql + " data10,data11,data12,data13,data14,data15,data16,data17,data18,data19,data20,data21,data22,data23,data24,data25,data26,data27,data28,data29,data30";
                sql = sql + " barcode01,barcode02,barcode03,barcode04,barcode05,barcode06,barcode07,barcode08,result) values(";
                for (int i = 1; i < count - 1; i++)
                {
                    sql += "'" + dr[i];
                    if (i != count - 2)
                    {
                        sql += "',";
                    }
                }
                sql += "')";
                DBHelper.ExecuteNonQuery(sql);
            }
        }
        public static void assistantUpdateToMain(MySqlDataReader dr, string mainStation, string pd, string productId)
        {
            int count = dr.FieldCount;
            while (dr.Read())
            {
                string sql = "update " + mainStation + " set data03 = '" + dr.GetString(5) + "',data04 = '" + dr.GetString(6) + "',data05 = '" + dr.GetString(7) + "',data06 = '" + dr.GetString(8) + "',data07 = '" + dr.GetString(9) + "',data08 = '" + dr.GetString(10) + "' where dateAtime = (select MAX(dateAtime) from " + mainStation + " where production = '" + pd + "') ";
                DBHelper.ExecuteNonQuery(sql);
            }
            string update = "update OP20 set productionId = '" + productId + "', upLine = '1' where dateAtime = (select MAX(dateAtime) from OP20 where result = 1 and upLine IS NULL and production = '" + pd + "')";
            DBHelper.ExecuteNonQuery(update);
        }
        public static void UpdateDefeat(string productionName, string proId, int num)
        {
            string order;
            if (num < 10) { order = "0" + num; } else { order = num.ToString(); }
            string sql = "update " + productionName + " set result = 2 , dateAtime" + order + " = '" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "' where productionId = '" + proId + "'";
            DBHelper.ExecuteNonQuery(sql);
        }

        public static void UpdateProductResult(string productionName, string proId)
        {
            string sql = "update " + productionName + " set result = 1  where productionId = '" + proId + "'";
            DBHelper.ExecuteNonQuery(sql);
        }
        public static void InsertProductResult(string productionName, string proId , DateTime now)
        {
            string sql = "insert into "+ productionName +" (productionId , result , dateAtime01) values('"+proId+"' , 1 , '"+now+"')";
            DBHelper.ExecuteNonQuery(sql);
        }
        #endregion

    }
}
