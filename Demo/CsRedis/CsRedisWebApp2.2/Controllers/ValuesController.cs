using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsRedisWebApp2._2.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace CsRedisWebApp2._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IDistributedCache _cache;

        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        //使用第三方redis工具直接访问Redis服务
        [HttpGet("Redis")]
        public string GetValueByRedis()
        {
            //初始化
            RedisHelper.Initialization(RedisConfig.Database0);

            string RedisName = "MyName";

            //最简单的使用
            if (RedisHelper.Exists(RedisName))
            {
                return RedisHelper.Get(RedisName);
            }
            else
            {
                var RedisValues = "this is Value by Redis";
                if (!string.IsNullOrEmpty(RedisValues))
                {
                    RedisHelper.Set(RedisName, RedisValues, 3600);
                }

                return RedisValues;
            }
        }

        //使用系统自带cache
        [HttpGet("Cache")]
        public string GetValueByCache()
        {
            var CacheName = "MyCacheName";
            var CacheValue = "This is Value by Cache";

            //配置选项
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetAbsoluteExpiration(TimeSpan.FromSeconds(60));//设置绝对过期时间

            var result = _cache.GetString(CacheName);
            if (result == null)
            {
                result = CacheValue;
                _cache.Set(CacheName, Encoding.UTF8.GetBytes(CacheValue), options);
            }
            return result;
        }
    }
}
