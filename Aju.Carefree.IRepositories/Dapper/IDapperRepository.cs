using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Aju.Carefree.Common;
using Dapper;

namespace Aju.Carefree.IRepositories
{
    public interface IDapperRepository<T> : IDependency where T : class, new()
    {
        T QueryFirstOrDefault(string sql, object param = null);
        Task<T> QueryFirstOrDefaultAsync(string sql, object param = null);

        IEnumerable<T> Query(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null);
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        T ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        Task<T> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        SqlMapper.GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null, CommandType? commandType = null);

        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);


        //Task Insert(T entity, string insertSql);

        //Task Update(T entity, string updateSql);

        //Task Delete(string deleteSql);

        //Task<List<T>> Select(string selectSql);

        //Task<T> Detail(string detailSql);

        ///// <summary>
        ///// 无参存储过程
        ///// </summary>
        ///// <param name="SPName"></param>
        ///// <param name="args"></param>
        ///// <returns></returns>
        //Task<List<T>> ExecQuerySP(string SPName);
    }
}
