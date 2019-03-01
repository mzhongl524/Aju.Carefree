using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.Models;

namespace Aju.Carefree.Services
{
    public class AreaService : GenericService<Areas>, IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        public AreaService(IAreaRepository areaRepository) : base(areaRepository) => _areaRepository = areaRepository;
    }
}
