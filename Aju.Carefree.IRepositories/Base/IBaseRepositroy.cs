using Aju.Carefree.Common;
using Aju.Carefree.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aju.Carefree.IRepositories
{
    /// <summary>
    /// 基础Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepositroy<T,TKey> : IDependency where T : class, new()
    {
        #region Select
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkId"></param>
        /// <returns></returns>
        T FindById(TKey pkId);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="pkId"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(TKey pkId);

        /// <summary>
        /// 查询所有 （无分页 请慎用）
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// 查询所有 （无分页 请慎用）
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T FindByClause(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="where">条件表达式树</param>
        /// <param name="order">排序</param>
        /// <returns></returns>

        IEnumerable<T> FindListByClause(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="where">条件表达式树</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null);

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, bool>> order = null);

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, bool>> order = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate, Pagination pagination);
        #endregion

        #region Update
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(T entity);
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);
        #endregion

        #region Delete
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        bool Delete(T entity);

        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> @where);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Expression<Func<T, bool>> @where);
        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteById(TKey id);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(TKey id);

        #endregion

        #region Insert
        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        Task<int> InsertAsync(T entity);


        /// <summary>
        /// 批量插入 插入失败时 事务会自动回退
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(List<T> t);

        /// <summary>
        /// 批量插入 插入失败时 事务会自动回退
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<int> InsertAsync(List<T> t);
        #endregion

    }
}
