using Newtonsoft.Json;
using Review.Model;
using Review.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Review.Repository
{    
    public  class SqlSugarHelper : DBcontext
    {
        public static int SaveStationData(StationData StationData , string productName ,int productType)
        {
            var dicStationData = new stationModel();
            dicStationData.time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dicStationData.productType = productType;
            dicStationData.productName = productName;
            dicStationData.productCode = StationData.productCode;
            dicStationData.data1 = StationData.result[0].ToString("F3");
            dicStationData.data2 = StationData.result[1].ToString("F3");
            dicStationData.data3 = StationData.result[2].ToString("F3");
            dicStationData.data4 = StationData.result[3].ToString("F3");
            dicStationData.data5 = StationData.result[4].ToString("F3");
            dicStationData.data6 = StationData.result[5].ToString("F3");
            dicStationData.data7 = StationData.result[6].ToString("F3");
            dicStationData.data8 = StationData.result[7].ToString("F3");
            dicStationData.data9 = StationData.result[8].ToString("F3");
            dicStationData.data10 = StationData.result[9].ToString("F3");
            dicStationData.data11 = StationData.result[10].ToString("F3");
            dicStationData.data12 = StationData.result[11].ToString("F3");
            dicStationData.data13 = StationData.result[12].ToString("F3");
            dicStationData.data14 = StationData.result[13].ToString("F3");
            dicStationData.data15 = StationData.result[14].ToString("F3");
            dicStationData.data16 = StationData.result[15].ToString("F3");
            dicStationData.data17 = StationData.result[16].ToString("F3");
            dicStationData.data18 = StationData.result[17].ToString("F3");
            dicStationData.data19 = StationData.result[18].ToString("F3");
            dicStationData.data20 = StationData.result[19].ToString("F3");
            dicStationData.barcode1 = StationData.productCode;
            dicStationData.barcode2 = StationData.barcode2;
            dicStationData.barcode3 = StationData.barcode3;
            dicStationData.barcode4 = StationData.barcode4;
            dicStationData.barcode5 = StationData.barcode5;
            dicStationData.barcode6 = StationData.barcode6;
            dicStationData.barcode7 = StationData.barcode7;
            dicStationData.barcode8 = StationData.barcode8;
            dicStationData.result = StationData.quality;
            return Db.Insertable(dicStationData).AS(StationData.stationName).ExecuteCommand();
        }
        public static List<string> SearchRebackData<TEntity>(string stationName ,
            Expression<Func<TEntity , object>> group,
            Expression<Func<TEntity , string>> selector,
            string WhereClause
            ) {

            var duplicateNames = Db.Queryable<TEntity>().AS(stationName)
                .Where(WhereClause)
                .GroupBy(group)
                .Having("COUNT(*) > 1")
                .Select(selector)
                .ToList();

            return duplicateNames;
        }
        public static List<stationprocode> selectPro(string productName,string productCode) {
            var presult = GetDataList<stationprocode>(s => s.productName == productName && s.productCode == productCode);
            if (presult.Count == 0) {
                var pcode = Db.Queryable<stationModel>().AS("OP20").Where(s => s.barcode4 == productCode && s.productName == productName).Select(s => s.productCode).First();
                presult = GetDataList<stationprocode>(s => s.productName == productName && s.productCode == pcode);
            }
            if (presult.Count == 0)
            {
                var pcode = Db.Queryable<stationModel>().AS("OP30").Where(s => s.barcode3 == productCode && s.productName == productName).Select(s => s.productCode).First();
                presult = GetDataList<stationprocode>(s => s.productName == productName && s.productCode == pcode);
            }
            return presult;
        }
        #region 生成实体
        /// <summary>
        /// 生成带有SqlSugar特性的实体
        /// </summary>
        /// <param name="filePath">生成路径</param>
        /// <param name="namespaceModels">实体命名空间</param>
        /// <returns></returns>
        public static int CreateClassFile(string filePath,string namespaceModels) {
            int result = 0;
            try
            {
                Db.DbFirst
                    .IsCreateAttribute()
                    .FormatClassName(it => it.Replace(" ", "").Replace("-", "_"))
                    .CreateClassFile(filePath, namespaceModels);
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                Console.WriteLine("生成CreateClassFile()异常:" + ex.Message);
            }
            return result;
        }
        #endregion
        #region Sugar扩展方法
        //根据泛型查找指定数据并转换为List
        public static List<TProperty> GetColumnList<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> selector,
            Expression<Func<TEntity, bool>> predicate = null
            ) 
            where TEntity : class, new()
        {
            var query = Db.Queryable<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Select(selector).ToList();
        }
        public static TResult GetColumn<TEntity , TResult>(
            Expression<Func<TEntity , TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null
            ) {
            var query = Db.Queryable<TEntity>();
            
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.Select(selector).First();
        }
        public static List<TEntity> GetDataList<TEntity>(
            Expression<Func<TEntity, bool>> predicate = null)
        where TEntity : class, new()
        {
            var query = Db.Queryable<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query
                .ToList();
        }
        public static bool CheckDataHas<TEntity>(
            Expression<Func<TEntity, bool>> predicate = null)
        where TEntity : class, new()
        {
            var query = Db.Queryable<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query
                .Any();
        }
        public static List<TResult> GetDataList<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> selector,                         // 选择字段（多个）
            Expression<Func<TEntity, bool>> predicate = null,                           // 条件
            Expression<Func<TEntity, object>> orderBy = null,                    // 排序字段（可选）
            bool isAsc = true,                                                   // 是否升序
            int? skip = null,                                                    // 跳过数量（分页）
            int? take = null)                                                     // 获取数量（分页）
        where TEntity : class, new()
        {
            var query = Db.Queryable<TEntity>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = isAsc ? query.OrderBy(orderBy, OrderByType.Asc)
                              : query.OrderBy(orderBy, OrderByType.Desc);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return query.Select(selector).ToList();
        }
        public static DataTable GetDataTable<TEntity>(
            Expression<Func<TEntity, bool>> predicate = null) 
            where TEntity : class, new()
        {
            var query = Db.Queryable<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToDataTable();
        }
        public static DataTable GetDataTable<TResult>(
            Expression<Func<Object, bool>> predicate,                           // 条件
            Expression<Func<Object, TResult>> selector,                         // 选择字段（多个）
            string stationName,                                                  // 选择查询的工位名称
            Expression<Func<Object, object>> orderBy = null,                    // 排序字段（可选）
            bool isAsc = true,                                                   // 是否升序
            int? skip = null,                                                    // 跳过数量（分页）
            int? take = null)                                                     // 获取数量（分页）
        {
            var query = Db.Queryable<Object>().AS(stationName).Where(predicate);

            if (orderBy != null)
            {
                query = isAsc ? query.OrderBy(orderBy, OrderByType.Asc)
                              : query.OrderBy(orderBy, OrderByType.Desc);
            }

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return query.Select(selector).ToDataTable();
        }
        /// <summary>
        /// 执行事务，支持带 SqlSugarClient 参数的操作体
        /// </summary>
        public static void ExecuteTransaction(Action<SqlSugarScope> action)
        {
            try
            {
                Db.Ado.BeginTran();
                action(Db);
                Db.Ado.CommitTran();
            }
            catch
            {
                Db.Ado.RollbackTran();
                throw;
            }
        }
        /// <summary>
        /// 根据条件删除指定数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int DeleteByCondition<TEntity>(Expression<Func<TEntity, bool>> condition)
        where TEntity : class, new()
        {
            return Db.Deleteable<TEntity>()
                     .Where(condition)
                     .ExecuteCommand(); // 返回影响的行数
        }
        /// <summary>
        /// 根据条件删除指定数据，事务传参执行 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int DeleteByCondition<TEntity>(SqlSugarScope db , Expression<Func<TEntity, bool>> condition)
        where TEntity : class, new()
        {
            return db.Deleteable<TEntity>()
                     .Where(condition)
                     .ExecuteCommand(); // 返回影响的行数
        }
        /// <summary>
        /// 根据条件更新指定字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereCondition"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public static int UpdateByCondition<TEntity>(
            Expression<Func<TEntity, bool>> whereCondition,
            Expression<Func<TEntity, TEntity>> updateExpression)
        where TEntity : class, new()
        {
            return Db.Updateable<TEntity>()
                     .SetColumns(updateExpression)
                     .Where(whereCondition)
                     .ExecuteCommand();
        }
        public static int ParaMetersUpOrInEntity<TEntity>(List<TEntity> entities)
        where TEntity : class, ISCode, new()
        {
            int returnCount = 0;
            foreach (var member in entities)
            {
                string pName = member.ProductName;
                string partName = member.PartName;
                string grade = member.Grade;

                bool exists = Db.Queryable<TEntity>()
                    .Any(p => p.ProductName == pName && p.PartName == partName && p.Grade == grade);

                if (exists)
                {
                    returnCount += UpdateByCondition(member, r => r.ProductName == pName && r.PartName == partName && r.Grade == grade);
                }
                else
                {
                    returnCount += InsertEntity(member) ? 1 : 0;
                }
            }
            return returnCount;
        }
        public static int printCodeUpOrInEntity(printCode entitie , bool exists)
        {
            int returnCount = 0;
            if (exists)
            {
                returnCount += UpdateByCondition(entitie, p => p.proName == entitie.proName);
            }
            else
            {
                returnCount += InsertEntity(entitie) ? 1 : 0;
            }
            return returnCount;
        }
        /// <summary>
        /// 根据条件更新，需传入初始化完成后的类
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public static int UpdateByCondition<TEntity>(
            TEntity entity,
            Expression<Func<TEntity, bool>> whereCondition)
        where TEntity : class, new()
        {
            return Db.Updateable(entity)
                     .Where(whereCondition)
                     .IgnoreColumns(ignoreAllNullColumns: true)
                     .ExecuteCommand();
        }
        public static bool UpdateColumns<TEntity>(
            TEntity entity, 
            Expression<Func<TEntity, object>> columns,
            Expression<Func<TEntity, bool>> whereCondition
            )
        where TEntity : class, new()
        {
            var result = Db.Updateable(entity)
                           .Where(whereCondition)
                           .UpdateColumns(columns)
                           .ExecuteCommand();
            return result > 0;
        }
        public static bool InsertEntity<TEntity>(TEntity entity)
        where TEntity : class, new()
        {
            return Db.Insertable(entity).IgnoreColumnsNull(true).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 同时插入多个实体的方法
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int InsertEntities<TEntity>(List<TEntity> entities)
        where TEntity : class, new()
        {
            return Db.Insertable(entities).ExecuteCommand();
        }
        /// <summary>
        /// 同时插入多个实体的方法，事务传参执行
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int InsertEntities<TEntity>(SqlSugarScope db , List<TEntity> entities)
        where TEntity : class, new()
        {
            return db.Insertable(entities).ExecuteCommand();
        }
        #endregion
    }
}
