using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Aju.Carefree.Common.Model;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using SqlSugar;

namespace Aju.Carefree.Services
{
    public class GenericSqlSugarService<T> : ISqlSugarService<T> where T : class, new()
    {
        private readonly ISqlSugarRepository<T> _repository;
        public GenericSqlSugarService(ISqlSugarRepository<T> repository) => _repository = repository;

        #region Sync

        /// <summary>
        /// 根据主值查询单条数据
        /// </summary>
        /// <param name="pkValue">主键值</param>
        /// <returns>泛型实体</returns>
        public T FindById(object pkValue) => _repository.FindById(pkValue);

        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> FindAll() => _repository.FindAll();

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> predicate, string orderBy = "") =>
            _repository.FindListByClause(predicate, orderBy);

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15,
            string orderBy = "") => _repository.PageQuery(predicate, skip, pageSize, orderBy);

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <returns></returns>
        public T FindByClause(Expression<Func<T, bool>> predicate) => _repository.FindByClause(predicate);

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int Insert(T entity) => _repository.Insert(entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity) => _repository.Update(entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Delete(T entity) => _repository.Delete(entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> @where) => _repository.Delete(@where);

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(object id) => _repository.DeleteById(id);

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteByIds(object[] ids) => _repository.DeleteByIds(ids);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <param name="dynamic">参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindListBySql(string sql, object dynamic) => _repository.FindListBySql(sql, dynamic);

        /// <summary>
        /// 批量插入 插入失败时 事务会自动回退
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Insert(List<T> t) => _repository.Insert(t);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public DbResult<T> UserTran(Func<T> func) => _repository.UserTran(func);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="action"></param>
        public DbResult<bool> UserTran(Action action) => _repository.UserTran(action);

        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int DeleteByClause(Expression<Func<T, bool>> predicate) => _repository.DeleteByClause(predicate);

        // void ShadowCopy(object a, object b);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination) =>
            _repository.FindList(predicate, pagination);
        #endregion

        #region Async

        /// <summary>
        /// 查询所有数据(无分页,请慎用)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync() => await _repository.FindAllAsync();

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <param name="orderBy">排序</param>
        /// <returns>泛型实体集合</returns>
        public async Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> predicate,
            string orderBy = "") => await _repository.FindListByClauseAsync(predicate, orderBy);

        public async Task<IEnumerable<T>> PageQueryAsync(Expression<Func<T, bool>> predicate, int skip = 0,
            int pageSize = 15, string orderBy = "") =>
            await _repository.PageQueryAsync(predicate, skip, pageSize, orderBy);

        public async Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate) =>
            await _repository.FindByClauseAsync(predicate);

        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(T entity) => await _repository.InsertAsync(entity);

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entity) => await _repository.DeleteAsync(entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="where">条件表达式</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> @where) => await _repository.DeleteAsync(@where);

        /// <summary>
        /// DeleteByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(object id) => await _repository.DeleteByIdAsync(id);

        /// <summary>
        /// DeleteByIdsAsync
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(object[] ids) => await _repository.DeleteByIdsAsync(ids);

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<DbResult<Task<int>>> InsertAsync(List<T> t) => await _repository.InsertAsync(t);

        /// <summary>
        /// DeleteByClauseAsync
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> DeleteByClauseAsync(Expression<Func<T, bool>> predicate) =>
            await _repository.DeleteByClauseAsync(predicate);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<DbResult<T>> UserTranAsync(Func<T> func) => await _repository.UserTranAsync(func);

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="action"></param>
        public async Task<DbResult<bool>> UserTranAsync(Action action) => await _repository.UserTranAsync(action);

        #endregion
    }
}
