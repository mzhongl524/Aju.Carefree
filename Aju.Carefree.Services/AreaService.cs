using System.Collections.Generic;
using System.Threading.Tasks;
using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;

namespace Aju.Carefree.Services
{
    public class AreaService : IAreaService
    {
        //  private readonly RepositroyBase repositroy;
        private readonly IBaseRepositroy<Areas, string> _repositroy;
        //private readonly IAreaRepository _repository;
        //public AreaService(IAreaRepository repository)
        //{
        //    _repository = repository;
        //}
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
