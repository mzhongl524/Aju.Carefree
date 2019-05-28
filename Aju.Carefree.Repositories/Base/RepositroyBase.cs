using Aju.Carefree.Repositories.Dapper;
using Aju.Carefree.Repositories.SqlSugar;
namespace Aju.Carefree.Repositories
{
    public abstract class RepositroyBase<T, TKey> : GenericSqlSugarRepositoryBase<T, TKey> where T : class, new()
    {
        public RepositroyBase(string connName) : base(connName)
        {
        }
    }

    //public abstract class RepositroyBase<T, TKey> : GenericDapperRepositroyBase<T, TKey> where T : class, new()
    //{
    //    public RepositroyBase(string connName) : base(connName)
    //    {
    //    }
    //}
}
