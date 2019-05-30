using System.Collections.Generic;
using System.Threading.Tasks;
using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;

namespace Aju.Carefree.Services
{
    public class AreaService : IAreaService
    {
        private readonly IBaseRepositroy<Areas, string> _repositroy;
        public AreaService(IBaseRepositroy<Areas, string> repositroy)
        {
            _repositroy = repositroy;
        }
        public Areas FindToPK(string id)
        {
            return _repositroy.FindById(id);
        }

        public Task<IEnumerable<Areas>> List()
        {
            return _repositroy.FindAllAsync();
        }
    }
}
