using Aju.Carefree.Common.DapperCore;
using Aju.Carefree.Common.Model;
using Aju.Carefree.IRepositories.Dapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aju.Carefree.Repositories.Dapper
{
    public abstract class GenericDapperRepositroyBase<T, TKey> : IDapperRepository<T, TKey> where T : class, new()
    {
        private string _ConnName { get; set; }
        public GenericDapperRepositroyBase(string connName) => _ConnName = connName;


        public bool Delete(T entity)
        {
            using (IDbConnection db = DapperHelper.GetSqlConnection())
            {
                return db.Delete(entity) > 0;
            }
        }

        public bool Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public T FindByClause(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByClauseAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T FindById(TKey pkId)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(TKey pkId)
        {
            throw new NotImplementedException();
        }

        public List<T> FindList(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FindListAsync(Expression<Func<T, bool>> predicate, Pagination pagination)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindListByClause(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindListByClauseAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> order = null)
        {
            throw new NotImplementedException();
        }

        public int Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public int Insert(List<T> t)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(List<T> t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> PageQuery(Expression<Func<T, bool>> predicate, int skip, int pageSize = 15, Expression<Func<T, bool>> order = null)
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
    }
}
