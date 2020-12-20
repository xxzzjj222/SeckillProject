using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Luas
{
    public class SeckillLuaHostedService : IHostedService
    {
        private readonly IMemoryCache _memoryCache;

        public SeckillLuaHostedService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 加载秒杀库存缓存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("加载执行lua文件到redis中");
                //1.加载lua到redis
                FileStream fileStream = new FileStream(@"Luas/SeckillLua.lua", FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string line = reader.ReadToEnd();
                    string luaSha = RedisHelper.ScriptLoad(@line);
                    //2.保存luaSha到缓存
                    _memoryCache.Set("luaSha", luaSha);
                }

                Console.WriteLine("加载回滚lua文件到redis中");
                //1.加载lua到redis
                FileStream fileStreamCallback = new FileStream(@"Luas/SeckillLuaCallback.lua", FileMode.Open);
                using(StreamReader reader=new StreamReader(fileStreamCallback))
                {
                    string line = reader.ReadToEnd();
                    string luaSha = RedisHelper.ScriptLoad(@line);
                    //2.保存luashaCallback到缓存中
                    _memoryCache.Set("luaShaCallback", luaSha);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"lua文件异常：{e.Message}");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
