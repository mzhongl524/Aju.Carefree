using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.Entity;

namespace Aju.Carefree.Services
{
    public class AreaSqlSugarService : IAreaSqlSugarService
    {
        private readonly IAreaSqlSugarRepository _areaRepository;
        public AreaSqlSugarService(IAreaSqlSugarRepository areaRepository)
            => _areaRepository = areaRepository;
    }
}
