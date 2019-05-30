using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aju.Carefree.IRepositories.SqlSugar
{
    /// <summary>
    /// 仓储通用接口类
    /// </summary>
    /// <typeparam name="T">泛型实体类</typeparam>
    public interface ISqlSugarRepository<T, TKey> : IBaseRepositroy<T, TKey> where T : class, new()
    {
        #region Sync
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy = "");

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, string orderBy = "");


        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteByIds(object[] ids);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IEnumerable<T> FindListBySql(string sql, object dynamic);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        DbResult<T> UserTran(Func<T> func);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="action"></param>
        DbResult<bool> UserTran(Action action);

        #endregion

        #region Async
        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "");

        Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0, int pageSize = 15, string orderBy = "");
        /// <summary>
        /// DeleteByIdsAsync
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(object[] ids);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<DbResult<T>> UserTranAsync(Func<T> func);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="action"></param>
        Task<DbResult<bool>> UserTranAsync(Action action);
        #endregion
    }
}
