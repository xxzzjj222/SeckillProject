using Microsoft.EntityFrameworkCore;
using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Context
{
    public class SeckillContext:DbContext
    {
        public SeckillContext(DbContextOptions<SeckillContext> options)
            :base(options)
        {

        }

        /// <summary>
        /// 秒杀集合
        /// </summary>
        public DbSet<Seckill> Seckills { get; set; }

        /// 秒杀记录集合
        /// </summary>
        public DbSet<SeckillRecord> SeckillRecords { get; set; }

        /// <summary>
        /// 秒杀时间集合
        /// </summary>
        public DbSet<SeckillTimeModel> SeckillTimeModels { get; set; }
    }
}
