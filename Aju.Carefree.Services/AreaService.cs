using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.Entity;
using System.Threading.Tasks;

namespace Aju.Carefree.Services
{
    public class AreaSqlSugarService : IAreaService
    {
        private readonly IAreaRepository _repository;
        public AreaSqlSugarService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public Areas FindToPK(string id)
        {
            return _repository.FindById(id);
        }
    }
}
