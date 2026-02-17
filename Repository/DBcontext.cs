using SqlSugar;
using System;
using Review.Utils;

namespace Review.Repository
{
    public class DBcontext
    {
        // For fixed multi-DB setup use SqlSugarScope(List<ConnectionConfig>, db => {...}).
        // For dynamic tenant DB setup use Saas split-database mode.
        private static readonly string ConnStr = DbProfileResolver.ResolveConnectionString();

        // Singleton scope.
        public static SqlSugarScope Db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = ConnStr,// Connection string.
            DbType = DbType.MySql,// Database type.
            IsAutoCloseConnection = true // Must be true to avoid manual close calls.
        },
        db => {
            //(A) Global configuration hook.
            // SQL debug event, can be removed if noisy.
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                // Print SQL for diagnostics.
                //Console.WriteLine(sql);

                // Print native SQL with parameters.
                Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

                // Alternative SQL string output (heavier), debug use only.
                //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars));
            };

            // SQL timeout.
            db.Ado.CommandTimeOut = 30;// Default 30 seconds.

            // Connection validation (optional).
            //db.Ado.IsValidConnection();
        });
    }
}
