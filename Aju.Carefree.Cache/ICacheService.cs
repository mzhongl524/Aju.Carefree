using System.Threading.Tasks;

namespace Aju.Carefree.Cache
{
    public interface ICacheService
    {
        #region Get

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(string key);
        #endregion

        #region Set
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetString(string key, string value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task SetStringAsync(string key, string value);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间(分钟)</param>
        void Set(string key, string value, int expirationTime = 20);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间(分钟)</param>
        Task SetAsync(string key, string value, int expirationTime = 20);
        #endregion

        #region Remove
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);


        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        Task RemoveAsync(string key);
        #endregion

        #region Modify
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        void Modify(string key, string value, int expirationTime = 20);

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        Task ModifyAsync(string key, string value, int expirationTime = 20);
        #endregion
    }
}
