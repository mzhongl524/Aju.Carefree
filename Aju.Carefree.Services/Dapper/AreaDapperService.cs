using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.Models;

namespace Aju.Carefree.Services
{
    public class AreaDapperService : GenericDapperService<Areas>, IAreaDapperService
    {
        private readonly IAreaDapperRepository _areaRepository;
        public AreaDapperService(IAreaDapperRepository areaRepository) : base(areaRepository) => _areaRepository = areaRepository;
    }
}
