using Aju.Carefree.Entity;
using Aju.Carefree.IRepositories;
using Aju.Carefree.IServices;
using Aju.Carefree.NetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aju.Carefree.Services
{
    public class ItemDetailsService : IItemDetailsService
    {
        private readonly IItemDetailsRepository _repository;
        public ItemDetailsService(IItemDetailsRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> DeleteFormAsync(string keyValue)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemsDetailEntity>> GetListAsync()
        {
            return await _repository.FindListByClauseAsync(s => s.EnabledMark == true && s.DeleteMark == false);
        }

        public async Task<IEnumerable<ItemsDetailEntity>> GetListAsync(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<ItemsDetailEntity>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.ItemName.Contains(keyword));
                expression = expression.Or(t => t.ItemCode.Contains(keyword));
            }
            expression = expression.And(t => t.DeleteMark == false);
            var list = await _repository.FindListByClauseAsync(expression, (s) => s.SortCode);
            return list.OrderBy(s => s.SortCode);
        }

        public List<ItemsDetailEntity> GetListToSql(string enCode)
        {
            string sql = @"SELECT  d.*
                            FROM    Sys_ItemsDetail d
                                    INNER  JOIN Sys_Items i ON i.Id = d.ItemId
                            WHERE   1 = 1
                                    AND i.EnCode = @enCode
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0
                            ORDER BY SortCode ASC";
            return _repository.FindListBySql(sql, new { EnCode = enCode }).ToList();
        }

        public async Task<bool> SubmitFormAsync(ItemsDetailEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                _repository.ShadowCopy(_repository.FindById(keyValue), entity);
                await entity.Modify(keyValue);
                return await _repository.UpdateAsync(entity);
            }
            await entity.Create();
            entity.DeleteMark = false;
            return await _repository.InsertAsync(entity) >= 1;
        }
        public async Task<IEnumerable<ItemsDetailEntity>> FindListByClauseAsync(Expression<Func<ItemsDetailEntity, bool>> where = null)
        {
            return await _repository.FindListByClauseAsync(where);
        }

        public async Task<IEnumerable<ItemsDetailEntity>> FindListByClauseAsync(string itemId, string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                return await _repository.FindListByClauseAsync(s =>
                s.ItemId.Equals(itemId) && s.DeleteMark == false && s.EnabledMark == true && s.ItemName.Contains(key));
            return await _repository.FindListByClauseAsync(s =>
                s.ItemId.Equals(itemId) && s.DeleteMark == false && s.EnabledMark == true);
        }
    }
}
