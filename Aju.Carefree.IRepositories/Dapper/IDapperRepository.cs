namespace Aju.Carefree.IRepositories.Dapper
{
    public interface IDapperRepository<T, TKey> : IBaseRepositroy<T, TKey> where T : class, new()
    {

    }
}
