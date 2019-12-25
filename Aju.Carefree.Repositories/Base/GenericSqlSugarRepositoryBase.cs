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
    public class GenericSqlSugarRepositoryBase<T, TKey> : ISqlSugarRepository<T, TKey> where T : class, new()
    {
        protected DbOption _dbOption;

        private object SqlSugarExt(Func<SqlSugarClient, object> func)
        {
            using (var db = DbFactory.DB(_dbOption.ConnectionString))
            {
                return func.Invoke(db);
            }
        }

        private async Task<object> SqlSugarExtAsync(Func<SqlSugarClient, Task<object>> func)
        {
            using (var db = DbFactory.DB(_dbOption.ConnectionString))
            {
                return await func.Invoke(db);
            }
        }

        public bool Delete(T entity)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable(entity).ExecuteCommand() > 0);
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable(where).ExecuteCommand());
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return (bool)await SqlSugarExtAsync(async (db) => await db.Deleteable(entity).ExecuteCommandAsync());
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            return (bool)await SqlSugarExtAsync(async (db) => await db.Deleteable(where).ExecuteCommandAsync());
        }

        public bool DeleteById(TKey id)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable<T>(id).ExecuteCommand());
        }

        public async Task<bool> DeleteByIdAsync(TKey id)
        {
            return (bool)await SqlSugarExtAsync(async (db) => await db.Deleteable<T>(id).ExecuteCommandAsync());
        }

        public bool DeleteByIds(object[] ids)
        {
            return (bool)SqlSugarExt((db) => db.Deleteable<T>().In(ids).ExecuteCommand());
        }

        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            return (bool)await SqlSugarExtAsync(async (db) => await db.Deleteable<T>().In(ids).ExecuteCommandAsync());
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

        public async Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate)
        {
            return (T)await SqlSugarExtAsync(async (db) => await db.Queryable<T>().FirstAsync(predicate));
        }

        public T FindById(TKey pkId)
        {
            return (T)SqlSugarExt((db) => db.Queryable<T>().InSingle(pkId));
        }

        public async Task<T> FindByIdAsync(TKey pkId)
        {
            return (T)await SqlSugarExtAsync(async (db) => await db.Queryable<T>().InSingleAsync(pkId));
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            return (List<T>)SqlSugarExt((db) =>
            {
                if (pagination.rows > 0)
                    pagination.rows = pagination.page * pagination.rows;
                return db.Queryable<T>().ToPageList(pagination.rows, pagination.page);
                //var query = db.Queryable<T>().Where(predicate).Skip(pagination.rows).Take(pagination.page);
                //return query.ToList();
            });
        }

        public async Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            return (List<T>)await SqlSugarExtAsync(async (db) =>
           {
               if (pagination.rows > 0)
                   pagination.rows = pagination.page * pagination.rows;
               return await db.Queryable<T>().ToPageListAsync(pagination.rows, pagination.page);
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

        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null)
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
           {
               var query = db.Queryable<T>().Where(where).ToList();
               if (order != null)
                   return query.AsQueryable().OrderBy(order);
               return query;
           });
        }

        public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "")
        {
            return (IEnumerable<T>)await SqlSugarExtAsync(async (db) =>
         {
             var query = db.Queryable<T>().Where(predicate);
             if (!string.IsNullOrEmpty(orderBy))
                 query.OrderBy(orderBy);
             return await query.ToListAsync();
         });
        }

        public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null)
        {
            return (IEnumerable<T>)await SqlSugarExtAsync(async (db) =>
           {
               var query = db.Queryable<T>().Where(where);
               return await query.OrderBy(order, OrderByType.Asc).ToListAsync();
           });

        }

        public IEnumerable<T> FindListBySql(string sql, object dynamic)
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
          {
              return db.Ado.SqlQuery<T>(sql, dynamic);
          });
        }

        public int Insert(T entity)
        {
            return (int)SqlSugarExt((db) => db.Insertable(entity).ExecuteCommand());
        }

        public int Insert(List<T> t)
        {
            return (int)SqlSugarExt((db) => db.Insertable(t).ExecuteCommand());
        }

        public async Task<int> InsertAsync(T entity)
        {
            return (int)await SqlSugarExtAsync(async (db) => await db.Insertable(entity).ExecuteCommandAsync());
        }

        public async Task<int> InsertAsync(List<T> t)
        {
            return (int)await SqlSugarExtAsync(async (db) => await db.Insertable(t).ExecuteCommandAsync());
        }

        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip,
            int pageSize = 15, string orderBy = "")
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
            {
                if (!string.IsNullOrEmpty(orderBy))
                    return db.Queryable<T>().Where(predicate).OrderBy(orderBy).ToPageList(skip, pageSize);
                return db.Queryable<T>().Where(predicate).ToPageList(skip, pageSize);
            });
        }

        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate,
            int skip, int pageSize = 15, Expression<Func<T, object>> order = null)
        {
            return (IEnumerable<T>)SqlSugarExt((db) =>
            {
                return db.Queryable<T>().Where(predicate).OrderBy(order).ToPageList(skip, pageSize);
            });
        }

        public async Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0, int pageSize = 15, string orderBy = "")
        {
            return (IEnumerable<T>)await SqlSugarExtAsync(async (db) =>
            {
                if (!string.IsNullOrEmpty(orderBy))
                    return await db.Queryable<T>().Where(predicate).OrderBy(orderBy).ToPageListAsync(skip, pageSize);
                return await db.Queryable<T>().Where(predicate).ToPageListAsync(skip, pageSize);
            });
        }

        public async Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, object>> order = null)
        {
            return (IEnumerable<T>)await SqlSugarExtAsync(async (db) =>
            {
                return await db.Queryable<T>().Where(predicate).OrderBy(order).ToPageListAsync(skip, pageSize);
            });
        }

        public bool Update(T entity)
        {
            return (bool)SqlSugarExt((db) => db.Updateable(entity).ExecuteCommand());
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return (bool)await SqlSugarExtAsync(async (db) => await db.Updateable(entity).ExecuteCommandAsync() > 0);
        }

        public DbResult<T> UserTran(Func<T> func)
        {
            return (DbResult<T>)SqlSugarExt((db) =>
            {
                return db.UseTran(func);
            });
        }

        public DbResult<bool> UserTran(Action action)
        {
            return (DbResult<bool>)SqlSugarExt((db) =>
            {
                return db.UseTran(action);
            });
        }

        public async Task<DbResult<T>> UserTranAsync(Func<T> func)
        {
            return (DbResult<T>)await SqlSugarExtAsync(async (db) =>
           {
               return await db.UseTranAsync(func);
           });
        }

        public async Task<DbResult<bool>> UserTranAsync(Action action)
        {
            return (DbResult<bool>)await SqlSugarExtAsync(async (db) =>
            {
                return await db.UseTranAsync(action);
            });
        }

        public void ShadowCopy(object a, object b)
        {
            if (a == null) return;
            Type ta = a.GetType();
            Type tb = b.GetType();
            foreach (var propb in tb.GetProperties())
            {
                foreach (var propa in ta.GetProperties())
                {
                    if (propb.Name != propa.Name) continue;
                    if (propb.GetValue(b, null) != null && Convert.ChangeType(propb.GetValue(b, null), typeof(string)).Equals("&nbsp;"))
                        propb.SetValue(b, "", null);
                    if (propb.GetValue(b, null) == null)
                    {
                        var value = propa.GetValue(a, null);
                        propb.SetValue(b, value, null);
                    }
                    break;
                }
            }
        }
    }
}
