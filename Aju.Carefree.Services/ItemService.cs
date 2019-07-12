using Aju.Carefree.Dto.ViewModel;
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
            return await _repository.FindListByClauseAsync(s => s.DeleteMark == false && s.EnabledMark == true);
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

        public async Task<List<LayuiTreeViewModel>> GetViewModel()
        {
            var viewModelList = new List<LayuiTreeViewModel>();
            var list = await _repository.FindListByClauseAsync(s => s.EnabledMark == true && s.DeleteMark == false);
            list.Where(s => s.ParentId == "0").ToList().ForEach(item =>
            {
                var viewModel = new LayuiTreeViewModel();
                viewModel.id = item.Id;
                viewModel.title = item.FullName;
                GetItemsEntityByParentId(item.Id, viewModel, list);
                viewModelList.Add(viewModel);
            });
            return viewModelList;
        }

        private LayuiTreeViewModel GetItemsEntityByParentId(string parendId, LayuiTreeViewModel viewModel, IEnumerable<ItemsEntity> list)
        {
            var items = list.Where(s => s.ParentId.Equals(parendId));
            if (!items.Any()) return null;
            List<LayuiTreeViewModel> layuiTreeViewModelsList = new List<LayuiTreeViewModel>();
            items.ToList().ForEach(item =>
            {
                LayuiTreeViewModel layuiTreeViewModel = new LayuiTreeViewModel();
                layuiTreeViewModel.id = item.Id;
                layuiTreeViewModel.title = item.FullName;
                GetItemsEntityByParentId(item.Id, layuiTreeViewModel, list);
                layuiTreeViewModelsList.Add(layuiTreeViewModel);
            });
            viewModel.children = layuiTreeViewModelsList;
            return viewModel;
        }

        //private async Task<LayuiTreeViewModel> GetItemsEntityByParentId(LayuiTreeViewModel viewModel, string parentId = "0")
        //{
        //    var list = await _repository.FindListByClauseAsync(s => s.ParentId.Equals(parentId) && s.EnabledMark == true && s.DeleteMark == false);
        //    if (!list.Any()) return null;
        //    List<LayuiTreeViewModel> jdList = new List<LayuiTreeViewModel>();

        //    list.ToList().ForEach(async item =>
        //    {
        //        LayuiTreeViewModel layuiTreeViewModel = new LayuiTreeViewModel();
        //        layuiTreeViewModel.id = item.Id;
        //        layuiTreeViewModel.title = item.FullName;
        //        //递归循环
        //        await GetItemsEntityByParentId(layuiTreeViewModel, item.Id);
        //        jdList.Add(layuiTreeViewModel);
        //    });
        //    viewModel.children = jdList;
        //    return viewModel;
        //}

        //private async Task<List<LayuiTreeViewModel>> GetItemsEntityByParentIdExt(string parentId)
        //{
        //    var viewModel = new List<LayuiTreeViewModel>();
        //    var list = await _repository.FindListByClauseAsync(s => s.ParentId.Equals(parentId) && s.EnabledMark == true && s.DeleteMark == false);
        //    list.ToList().ForEach(async item =>
        //    {
        //        viewModel.Add(new LayuiTreeViewModel
        //        {
        //            title = item.FullName,
        //            spread = true,
        //            @checked = false,
        //            id = item.Id,
        //            children =
        //        });
        //    });
        //    return viewModel;
        //}
    }
}


