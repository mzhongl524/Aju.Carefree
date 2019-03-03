using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace Aju.Carefree.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly RedisCache _redisCache;
        public RedisCacheService(RedisCacheOptions options) => _redisCache = new RedisCache(options);

        #region 获取缓存 Get(string key)
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public string Get(string key)
        {
            string returnStr = null;
            if (string.IsNullOrEmpty(key)) return null;
            if (Exists(key))
                returnStr = Encoding.UTF8.GetString(_redisCache.Get(key));
            return returnStr;

        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            string returnString = null;
            var value = await _redisCache.GetAsync(key);
            if (value != null)
                returnString = Encoding.UTF8.GetString(value);
            return returnString;
        }

        public string GetString(string key)
        {
            return _redisCache.GetString(key);
        }

        public async Task<string> GetStringAsync(string key)
        {
            return await _redisCache.GetStringAsync(key);
        }
        #endregion

        #region 添加缓存 Set
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间(分钟)</param>
        public void Set(string key, string value, int expirationTime = 20)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _redisCache.Set(key, Encoding.UTF8.GetBytes(value),
                    new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
                    {
                        //设置绝对过期时间
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationTime)
                        //设置滑动过期时间
                        // SlidingExpiration=
                    });
                // _redisCache.Refresh(key);
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="expirationTime">绝对过期时间(分钟)</param>
        public async Task SetAsync(string key, string value, int expirationTime = 20)
        {
            if (!string.IsNullOrEmpty(key))
            {
                await _redisCache.SetAsync(key, Encoding.UTF8.GetBytes(value),
                       new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
                       {
                           //设置绝对过期时间
                           AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationTime)
                           //设置滑动过期时间
                           // SlidingExpiration=
                       });
                // _redisCache.Refresh(key);
            }
        }

        public void SetString(string key, string value)
        {
            _redisCache.SetString(key, value);
        }
        public async Task SetStringAsync(string key, string value)
        {
            await _redisCache.SetStringAsync(key, value);
        }

        #endregion

        #region 移除缓存 Remove

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                _redisCache.Remove(key);
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                await _redisCache.RemoveAsync(key);
        }

        #endregion

        #region 更新缓存 Modify
        public void Modify(string key, string value, int expirationTime = 20)
        {
            if (string.IsNullOrEmpty(key)) return;
            Remove(key);
            Set(key, value, expirationTime);
        }

        public async Task ModifyAsync(string key, string value, int expirationTime = 20)
        {
            if (string.IsNullOrEmpty(key)) return;
            await RemoveAsync(key);
            await SetAsync(key, value, expirationTime);
        }

        #endregion

        /// <summary>
        /// 验证是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool Exists(string key)
        {
            var returnBool = true;
            var val = _redisCache.Get(key);
            if (val == null || val.Length == 0)
                returnBool = false;
            return returnBool;
        }
    }
}
