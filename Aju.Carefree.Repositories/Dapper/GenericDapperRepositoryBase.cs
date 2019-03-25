using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Aju.Carefree.Common.DapperCore;
using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.IRepositories;
using Dapper;

namespace Aju.Carefree.Repositories
{
    public class GenericDapperRepositoryBase<T> : IDapperRepository<T> where T : class, new()
    {
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return await conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public T ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return conn.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return await conn.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public IEnumerable<T> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public T QueryFirstOrDefault(string sql, object param = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return conn.QueryFirstOrDefault<T>(sql, param);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object param = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return conn.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = DapperHelper.GetSqlConnection())
            {
                return await conn.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }
    }
}
