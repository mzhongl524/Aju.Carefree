using Aju.Carefree.Dto;
using Aju.Carefree.Dto.ViewModel;
using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aju.Carefree.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IItemDetailsService _itemDetailsService;
        private IMapper _mapper { get; set; }

        public ItemService(IItemRepository repository, IItemDetailsService itemDetailsService, IMapper mapper)
        {
            _repository = repository;
            _itemDetailsService = itemDetailsService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsEntity>> GetListAsync()
        {
            return await _repository.FindListByClauseAsync(s => s.DeleteMark == false && s.EnabledMark == true);
        }

        public async Task<bool> SubmitFormAsync(ItemDto dto, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(dto.ParentId))
                dto.ParentId = "0";
            var entity = _mapper.Map<ItemDto, ItemsEntity>(dto);
            return await SubmitFormAsync(entity, entity.Id);
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
                var viewModel = new LayuiTreeViewModel
                {
                    id = item.Id,
                    title = item.FullName
                };
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
                LayuiTreeViewModel layuiTreeViewModel = new LayuiTreeViewModel
                {
                    id = item.Id,
                    title = item.FullName
                };
                GetItemsEntityByParentId(item.Id, layuiTreeViewModel, list);
                layuiTreeViewModelsList.Add(layuiTreeViewModel);
            });
            viewModel.children = layuiTreeViewModelsList;
            return viewModel;
        }

        public async Task<List<TreeSelectViewModel>> GetTreeSelectViewModel()
        {
            var viewModelList = new List<TreeSelectViewModel>();
            var list = await _repository.FindListByClauseAsync(s => s.EnabledMark == true && s.DeleteMark == false);
            list.Where(s => s.ParentId == "0").ToList().ForEach(item =>
            {
                var viewModel = new TreeSelectViewModel
                {
                    id = item.Id,
                    name = item.FullName
                };
                GetItemsEntityByParentId(item.Id, viewModel, list);
                viewModelList.Add(viewModel);
            });
            return viewModelList;
        }

        private TreeSelectViewModel GetItemsEntityByParentId(string parendId, TreeSelectViewModel viewModel, IEnumerable<ItemsEntity> list)
        {
            var items = list.Where(s => s.ParentId.Equals(parendId));
            if (!items.Any()) return null;
            List<TreeSelectViewModel> layuiTreeViewModelsList = new List<TreeSelectViewModel>();
            items.ToList().ForEach(item =>
            {
                TreeSelectViewModel layuiTreeViewModel = new TreeSelectViewModel
                {
                    id = item.Id,
                    name = item.FullName
                };
                GetItemsEntityByParentId(item.Id, layuiTreeViewModel, list);
                layuiTreeViewModelsList.Add(layuiTreeViewModel);
            });
            viewModel.children = layuiTreeViewModelsList;
            return viewModel;
        }

        public async Task<ItemDto> GetItemsByPKID(string id)
        {
            var entity = await _repository.FindByIdAsync(id);
            return _mapper.Map<ItemsEntity, ItemDto>(entity);
        }
    }
}


