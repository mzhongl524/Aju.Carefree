using System.Collections.Generic;
using System.Threading.Tasks;
using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;

namespace Aju.Carefree.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repositroy;
        public AreaService(IAreaRepository repositroy)
        {
            _repositroy = repositroy;
        }
        public async Task<Areas> FindToPK(string id)
        {
            return await _repositroy.FindByIdAsync(id);
        }

        public Task<IEnumerable<Areas>> List()
        {
            return _repositroy.FindAllAsync();
        }
    }
}
