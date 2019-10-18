using Aju.Carefree.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aju.Carefree.IServices
{
    public interface IMenuService : IService<MenusEntity, string>
    {
        Task<MenusEntity> GetMenusByIdAsync(string id);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MenusEntity>> GetMenusAsync();

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<IEnumerable<MenusEntity>> GetMenusAsync(string keyword);

        /// <summary>
        /// 增/修
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<bool> SubmitForm(string keyValue);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        Task<bool> DeleteForm(string keyValue);
    }
}
