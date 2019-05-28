using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.Common.Model;
using Aju.Carefree.IRepositories;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aju.Carefree.Repositories.SqlSugar
{
    /// <summary>
    /// GenericRepositoryBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericSqlSugarRepositoryBase<T, TKey> : ISqlSugarRepository<T, TKey> where T : class, new()
    {
        private string _ConnStr { get; set; }
        public GenericSqlSugarRepositoryBase(string connStr)
        {
            this._ConnStr = connStr;
        }

        private object SqlSugarExt(Func<SqlSugarClient, object> func)
        {
            using (var db = DbFactory.DB(_ConnStr))
            {
                return func.Invoke(db);
            }
        }

        private async Task<object> SqlSugarExtAsync(Func<SqlSugarClient, Task<object>> func)
        {
            using (var db = DbFactory.DB(_ConnStr))
            {
                return await func.Invoke(db);
            }
        }

        //#region Sync
        ///// <summary>
        ///// 根据主键查询
        ///// </summary>
        ///// <param name="pkValue">主键</param>
        ///// <returns></returns>
        //public T FindById(object pkValue)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Queryable<T>().InSingle(pkValue);
        //    }
        //}

        ///// <summary>
        ///// 查询所有数据(无分页,请慎用)
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<T> FindAll()
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Queryable<T>().ToList();
        //    }
        //}

        ///// <summary>
        ///// 根据条件查询数据
        ///// </summary>
        ///// <param name="predicate">条件表达式树</param>
        ///// <param name="orderBy">排序</param>
        ///// <returns>泛型实体集合</returns>
        //public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy = "")
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        var query = db.Queryable<T>().Where(predicate);
        //        if (!string.IsNullOrEmpty(orderBy))
        //            query.OrderBy(orderBy);
        //        return query.ToList();
        //    }
        //}

        ///// <summary>
        ///// 根据条件分页查询
        ///// </summary>
        ///// <param name="predicate">条件表达式树</param>
        ///// <param name="skip"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="orderBy"></param>
        ///// <returns></returns>
        //public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15,
        //    string orderBy = "")
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        if (skip > 0)
        //            skip = pageSize * skip;
        //        var query = db.Queryable<T>().Where(predicate).Skip(skip).Take(pageSize);
        //        if (!string.IsNullOrEmpty(orderBy))
        //            query = query.OrderBy(orderBy);
        //        return query.ToList();
        //    }
        //}

        ///// <summary>
        ///// 根据条件查询数据
        ///// </summary>
        ///// <param name="predicate">条件表达式树</param>
        ///// <returns></returns>
        //public T FindByClause(Expression<Func<T, bool>> predicate)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Queryable<T>().First(predicate);
        //    }
        //}

        ///// <summary>
        ///// 写入实体数据
        ///// </summary>
        ///// <param name="entity">实体类</param>
        ///// <returns></returns>
        //public int Insert(T entity)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Insertable(entity).ExecuteCommand();
        //    }
        //}

        ///// <summary>
        ///// 更新实体数据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public bool Update(T entity)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Updateable(entity).ExecuteCommand() > 0;
        //    }
        //}

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="entity">实体类</param>
        ///// <returns></returns>
        //public bool Delete(T entity)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Deleteable(entity).ExecuteCommand() > 0;
        //    }
        //}

        ///// <summary>
        ///// 删除数据
        ///// </summary>
        ///// <param name="where">过滤条件</param>
        ///// <returns></returns>
        //public bool Delete(Expression<Func<T, bool>> @where)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Deleteable<T>(@where).ExecuteCommand() > 0;
        //    }
        //}

        ///// <summary>
        ///// 删除指定ID的数据
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public bool DeleteById(object id)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Deleteable<T>(id).ExecuteCommand() > 0;
        //    }
        //}

        ///// <summary>
        ///// 删除指定ID集合的数据(批量删除)
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public bool DeleteByIds(object[] ids)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Deleteable<T>().In(ids).ExecuteCommand() > 0;
        //    }
        //}

        ///// <summary>
        ///// 执行sql语句
        ///// </summary>
        ///// <param name="sql">sql 语句</param>
        ///// <param name="parameters">参数</param>
        ///// <returns></returns>
        //public IEnumerable<T> FindListBySql(string sql, object dynamic)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Ado.SqlQuery<T>(sql, dynamic);
        //    }
        //}

        ///// <summary>
        ///// 批量插入 插入失败时 事务会自动回退
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public int Insert(List<T> t)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Ado.UseTran(() => db.Insertable(t.ToArray()).ExecuteCommand()).Data;
        //    }
        //}

        ///// <summary>
        ///// 事务
        ///// </summary>
        ///// <param name="func"></param>
        ///// <returns></returns>
        //public DbResult<T> UserTran(Func<T> func)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Ado.UseTran(func.Invoke);
        //    }
        //}

        ///// <summary>
        ///// 事务
        ///// </summary>
        ///// <param name="action"></param>
        //public DbResult<bool> UserTran(Action action)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Ado.UseTran(action.Invoke);
        //    }
        //}

        ///// <summary>
        ///// 根据条件批量删除
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public int DeleteByClause(Expression<Func<T, bool>> predicate)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return db.Deleteable<T>().Where(predicate).ExecuteCommand();
        //    }
        //}

        //// void ShadowCopy(object a, object b);

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <param name="pagination"></param>
        ///// <returns></returns>
        //public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        //{
        //    var isAsc = pagination.sord.ToLower() == "asc";
        //    string[] _order = pagination.sidx.Split(',');
        //    MethodCallExpression resultExp = null;
        //    using (var db = DbFactory.DB)
        //    {
        //        var tempData = db.Queryable<T>().Where(predicate).ToList().AsQueryable();
        //        foreach (string item in _order)
        //        {
        //            string _orderPart = item;
        //            _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
        //            string[] _orderArry = _orderPart.Split(' ');
        //            string _orderField = _orderArry[0];
        //            bool sort = isAsc;
        //            if (_orderArry.Length == 2)
        //            {
        //                isAsc = _orderArry[1].ToUpper() == "ASC";
        //            }
        //            var parameter = Expression.Parameter(typeof(T), "t");
        //            var property = typeof(T).GetProperty(_orderField);
        //            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //            var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //            resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
        //        }
        //        tempData = tempData.Provider.CreateQuery<T>(resultExp);
        //        pagination.records = tempData.Count();
        //        tempData = tempData.Skip<T>(pagination.rows * (pagination.page - 1)).Take<T>(pagination.rows).AsQueryable();
        //        return tempData.ToList();
        //    }
        //}
        //#endregion

        //#region Async
        ///// <summary>
        ///// 查询所有数据(无分页,请慎用)
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IEnumerable<T>> FindAllAsync()
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Queryable<T>().ToListAsync();
        //    }
        //}

        ///// <summary>
        ///// 根据条件查询数据
        ///// </summary>
        ///// <param name="predicate">条件表达式树</param>
        ///// <param name="orderBy">排序</param>
        ///// <returns>泛型实体集合</returns>
        //public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "")
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        var query = db.Queryable<T>().Where(predicate);
        //        if (!string.IsNullOrEmpty(orderBy))
        //        {
        //            query = query.OrderBy(orderBy);
        //        }
        //        return await query.ToListAsync();
        //    }
        //}

        //public async Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0,
        //    int pageSize = 15, string orderBy = "")
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        if (skip > 0)
        //            skip = pageSize * skip;
        //        var query = db.Queryable<T>().Where(predicate).Skip(skip).Take(pageSize);
        //        if (!string.IsNullOrEmpty(orderBy))
        //        {
        //            query = query.OrderBy(orderBy);
        //        }
        //        return await query.ToListAsync();
        //    }
        //}

        //public async Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Queryable<T>().FirstAsync(predicate);
        //    }
        //}

        /////// <summary>
        /////// 插入实体数据
        /////// </summary>
        /////// <param name="entity"></param>
        /////// <returns></returns>
        ////public async Task<int> InsertAsync(T entity)
        ////{
        ////    using (var db = DbFactory.DB)
        ////    {
        ////        return await db.Insertable(entity).ExecuteCommandAsync();
        ////    }
        ////}

        ///// <summary>
        ///// 更新实体数据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public async Task<bool> UpdateAsync(T entity)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Updateable(entity).ExecuteCommandAsync() > 0;
        //    }
        //}

        ///// <summary>
        ///// DeleteAsync
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public async Task<bool> DeleteAsync(T entity)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Deleteable(entity).ExecuteCommandAsync() > 0;
        //    }
        //}

        ///// <summary>
        ///// DeleteAsync
        ///// </summary>
        ///// <param name="where">条件表达式</param>
        ///// <returns></returns>
        //public async Task<bool> DeleteAsync(Expression<Func<T, bool>> @where)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Deleteable(@where).ExecuteCommandAsync() > 0;
        //    }
        //}

        ///// <summary>
        ///// DeleteByIdAsync
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<bool> DeleteByIdAsync(object id)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Deleteable<T>(id).ExecuteCommandAsync() > 0;
        //    }
        //}

        ///// <summary>
        ///// DeleteByIdsAsync
        ///// </summary>
        ///// <param name="ids">ids</param>
        ///// <returns></returns>
        //public async Task<bool> DeleteByIdsAsync(object[] ids)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Deleteable<T>().In(ids).ExecuteCommandAsync() > 0;
        //    }
        //}

        ///// <summary>
        ///// InsertAsync
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //public async Task<DbResult<Task<int>>> InsertAsync(List<T> t)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Ado.UseTranAsync(async () => await db.Insertable(t.ToArray()).ExecuteCommandAsync());
        //    }
        //}

        ///// <summary>
        ///// DeleteByClauseAsync
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //public async Task<int> DeleteByClauseAsync(Expression<Func<T, bool>> predicate)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Deleteable<T>().Where(predicate).ExecuteCommandAsync();
        //    }
        //}

        ///// <summary>
        ///// 事务
        ///// </summary>
        ///// <param name="func"></param>
        ///// <returns></returns>
        //public async Task<DbResult<T>> UserTranAsync(Func<T> func)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Ado.UseTranAsync(func.Invoke);
        //    }
        //}

        ///// <summary>
        ///// 事务
        ///// </summary>
        ///// <param name="action"></param>
        //public async Task<DbResult<bool>> UserTranAsync(Action action)
        //{
        //    using (var db = DbFactory.DB)
        //    {
        //        return await db.Ado.UseTranAsync(action.Invoke);
        //    }
        //}


        //#endregion

        public bool Delete(T entity)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable(entity).ExecuteCommand() > 0);
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable(where).ExecuteCommand());
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
            // return (Task<bool>)SqlSugarExtAsync(async (db) => await db.Deleteable(entity).ExecuteCommandAsync() > 0);
        }

        public Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
            //return (Task<bool>)SqlSugarExtAsync(async (db) => await db.Deleteable(where).ExecuteCommandAsync() > 0);
        }

        public bool DeleteById(TKey id)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable<T>(id).ExecuteCommand());
        }

        public Task<bool> DeleteByIdAsync(TKey id)
        {
            return (Task<bool>)SqlSugarExt((db) => db.Deleteable<T>(id).ExecuteCommandAsync());
        }

        public bool DeleteByIds(object[] ids)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable<T>().In(ids).ExecuteCommand());
        }

        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            throw new NotImplementedException();
            // return await (Task<bool>)SqlSugarExtAsync(async (db) => await db.Deleteable<T>().In(ids).ExecuteCommandAsync());
        }

        public IEnumerable<T> FindAll()
        {
            return (IEnumerable<T>)SqlSugarExt((db) => db.Queryable<T>().ToList());
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return (IEnumerable<T>)await SqlSugarExtAsync(async (db) => await db.Queryable<T>().ToListAsync());
        }

        public T FindByClause(Expression<Func<T, bool>> predicate)
        {
            return (T)SqlSugarExt((db) => db.Queryable<T>().First(predicate));
        }

        public Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate)
        {
            return (Task<T>)SqlSugarExt((db) => db.Queryable<T>().FirstAsync(predicate));
        }

        public T FindById(TKey pkId)
        {
            return (T)SqlSugarExt((db) => db.Queryable<T>().InSingle(pkId));
        }

        public Task<T> FindByIdAsync(TKey pkId)
        {
            throw new NotImplementedException();
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            return (List<T>)SqlSugarExt((db) =>
            {
                if (pagination.rows > 0)
                    pagination.rows = pagination.page * pagination.rows;
                var query = db.Queryable<T>().Where(predicate).Skip(pagination.rows).Take(pagination.page);
                return query.ToList();
            });
        }

        public Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            return (Task<List<T>>)SqlSugarExt((db) =>
           {
               if (pagination.rows > 0)
                   pagination.rows = pagination.page * pagination.rows;
               var query = db.Queryable<T>().Where(predicate).Skip(pagination.rows).Take(pagination.page);
               return query.ToListAsync();
           });
        }

        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy = "")
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
            {
                var query = db.Queryable<T>().Where(predicate);
                if (string.IsNullOrEmpty(orderBy))
                    return query.OrderBy(orderBy);
                return query.ToList();
            });
        }

        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null)
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
           {
               var query = db.Queryable<T>().Where(where).ToList();
               if (order != null)
                   return query.AsQueryable().OrderBy(order);
               return query;
           });
        }

        public Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "")
        {
            return (Task<IEnumerable<T>>)SqlSugarExt((db) =>
           {
               var query = db.Queryable<T>().Where(predicate);
               if (!string.IsNullOrEmpty(orderBy))
                   query.OrderBy(orderBy);
               return query.ToListAsync();
           });
        }

        public Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null)
        {
            throw new NotImplementedException();
            //return (Task<IEnumerable<T>>)SqlSugarExtAsync(async (db) =>
            // {
            //     var query = await db.Queryable<T>().Where(where).ToListAsync();
            //     if (order != null)
            //         return query.AsQueryable().OrderBy(order);
            //     return query;
            // });
        }

        public IEnumerable<T> FindListBySql(string sql, object dynamic)
        {
            throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
            return (int)SqlSugarExt((db) => db.Insertable(entity).ExecuteCommand());
        }

        public int Insert(List<T> t)
        {
            return (int)SqlSugarExt((db) => db.Insertable(t).ExecuteCommand());
        }

        public Task<int> InsertAsync(T entity)
        {
            return (Task<int>)SqlSugarExt((db) => db.Insertable(entity).ExecuteCommandAsync());
        }

        public Task<int> InsertAsync(List<T> t)
        {
            return (Task<int>)SqlSugarExt((db) => db.Insertable(t).ExecuteCommandAsync());
        }

        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, string orderBy = "")
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, bool>> order = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0, int pageSize = 15, string orderBy = "")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, bool>> order = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public DbResult<T> UserTran(Func<T> func)
        {
            throw new NotImplementedException();
        }

        public DbResult<bool> UserTran(Action action)
        {
            throw new NotImplementedException();
        }

        public Task<DbResult<T>> UserTranAsync(Func<T> func)
        {
            throw new NotImplementedException();
        }

        public Task<DbResult<bool>> UserTranAsync(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
