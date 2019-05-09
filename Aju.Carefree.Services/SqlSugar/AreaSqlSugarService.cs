using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.Entity;

namespace Aju.Carefree.Services
{
    public class AreaSqlSugarService : GenericSqlSugarService<Areas>, IAreaSqlSugarService
    {
        private readonly IAreaSqlSugarRepository _areaRepository;
        public AreaSqlSugarService(IAreaSqlSugarRepository areaRepository) : base(areaRepository) => _areaRepository = areaRepository;
    }
}
