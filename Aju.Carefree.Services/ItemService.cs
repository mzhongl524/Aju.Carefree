using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aju.Carefree.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IItemDetailsService _itemDetailsService;
        public ItemService(IItemRepository repository, IItemDetailsService itemDetailsService)
        {
            _repository = repository;
            _itemDetailsService = itemDetailsService;
        }

        public async Task<IEnumerable<ItemsEntity>> GetListAsync()
        {
            return await _repository.FindListByClauseAsync(s => s.DeleteMark == false);
        }

        public async Task<bool> SubmitFormAsync(ItemsEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))//修改
            {
                _repository.ShadowCopy(_repository.FindById(keyValue), entity);
                await entity.Modify(keyValue);
                return await _repository.UpdateAsync(entity);
            }
            //新增
            await entity.Create();
            entity.DeleteMark = false;
            return await _repository.InsertAsync(entity) >= 1;
        }

        public async Task<bool> DeleteFormAsync(string keyValue)
        {
            var listItemDetails = await _itemDetailsService.FindListByClauseAsync(s => s.ItemId.Equals(keyValue));
            if (listItemDetails.Count() > 0)
                throw new System.Exception("删除数据失败，改数据已产生数据！");
            var entity = _repository.FindById(keyValue);
            await entity.Remove();
            return await _repository.UpdateAsync(entity);
        }
    }
}
