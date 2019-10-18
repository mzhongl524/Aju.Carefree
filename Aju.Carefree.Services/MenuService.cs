using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aju.Carefree.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        public MenuService(IMenuRepository repository)
            => _repository = repository;

        public async Task<MenusEntity> GetMenusByIdAsync(string id)
        {
            return await _repository.FindByIdAsync(id);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MenusEntity>> GetMenusAsync()
        {
            return await _repository.FindListByClauseAsync(s => s.DeleteMark == false);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MenusEntity>> GetMenusAsync(string keyword)
        {
            return await _repository.FindListByClauseAsync(s => s.DeleteMark == false && s.FullName.Contains(keyword));
        }

        /// <summary>
        /// 增/修
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<bool> SubmitForm(string keyValue)
        {
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<bool> DeleteForm(string keyValue)
        {
            var xx = await _repository.FindListByClauseAsync(s => s.ParentId.Equals(keyValue));
            if (xx.Any())
                return false;
            // throw new System.Exception("删除失败，操作的对象已产生数据！");
            var model = await GetMenusByIdAsync(keyValue);
            await model.Remove();
            return await _repository.UpdateAsync(model);
        }
    }
}
