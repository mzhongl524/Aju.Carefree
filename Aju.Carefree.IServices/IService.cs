using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aju.Carefree.Common;
using Aju.Carefree.Common.Model;
using SqlSugar;

namespace Aju.Carefree.IServices
{
    public interface IService<T>: IDependency
    {
        #region Sync
        /// <summary>
        /// 根据主值查询单条数据
        /// </summary>
        /// <param name="pkValue">主键值</param>
        /// <returns>泛型实体</returns>
        T FindById(object pkValue);

        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
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
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <returns></returns>
        T FindByClause(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(object id);

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
        /// <param name="dynamic">参数</param>
        /// <returns></returns>
        IEnumerable<T> FindListBySql(string sql, object dynamic);

        /// <summary>
        /// 批量插入 插入失败时 事务会自动回退
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(List<T> t);

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

        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int DeleteByClause(Expression<Func<T, bool>> predicate);

        // void ShadowCopy(object a, object b);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination);
        #endregion

        #region Async
        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate, string orderBy = "");

        Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0, int pageSize = 15, string orderBy = "");
        Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> @where);
        /// <summary>
        /// DeleteByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(object id);
        /// <summary>
        /// DeleteByIdsAsync
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(object[] ids);
        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<DbResult<Task<int>>> InsertAsync(List<T> t);
        /// <summary>
        /// DeleteByClauseAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> DeleteByClauseAsync(Expression<Func<T, bool>> predicate);

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
