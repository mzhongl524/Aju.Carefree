using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Dapper;

namespace Aju.Carefree.Services
{
    public class GenericDapperService<T> : IDapperService<T> where T : class, new()
    {
        private readonly IDapperRepository<T> _repository;
        public GenericDapperService(IDapperRepository<T> repository) => _repository = repository;


        public T QueryFirstOrDefault(string sql, object param = null)
        {
            return _repository.QueryFirstOrDefault(sql, param);
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param = null)
        {
            return await _repository.QueryFirstOrDefaultAsync(sql, param);
        }

        public IEnumerable<T> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return _repository.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await _repository.QueryAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return _repository.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await _repository.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public T ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return _repository.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _repository.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return _repository.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return await _repository.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
