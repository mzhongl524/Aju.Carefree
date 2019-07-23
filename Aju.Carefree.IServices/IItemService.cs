using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IItemService : IService<ItemsEntity, string>
    {
        Task<IEnumerable<ItemsEntity>> GetListAsync();

        Task<bool> SubmitFormAsync(ItemsEntity entity, string keyValue);

        Task<bool> DeleteFormAsync(string keyValue);

        Task<List<LayuiTreeViewModel>> GetViewModel();

        Task<List<TreeSelectViewModel>> GetTreeSelectViewModel();

        Task<ItemsEntity> GetItemsByPKID(string id);
    }
}
