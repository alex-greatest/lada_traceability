using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Repository
{
    public class DBcontext
    {
        //如果是固定多库可以传 new SqlSugarScope(List<ConnectionConfig>,db=>{}) 文档：多租户
        //如果是不固定多库 可以看文档Saas分库
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["connectMySql"].ConnectionString;


        //用单例模式
        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = ConnStr,//连接符字串
            DbType = DbType.MySql,//数据库类型
            IsAutoCloseConnection = true //不设成true要手动close
        },
        db => {
            //(A)全局生效配置点
            //调试SQL事件，可以删掉
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //输出sql,查看执行sql
                //Console.WriteLine(sql);

                //获取原生SQL推荐 5.1.4.63  性能OK
                Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                //5.0.8.2 获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars));
            };

            //Sql超时
            db.Ado.CommandTimeOut = 30;//默认30 单位秒

            //验证连接是否成功
            //db.Ado.IsValidConnection();

        });
    }
}
