using Projects.SeckillServices.Context;
using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀仓储实现
    /// </summary>
    public class SeckillRepository : ISeckillRepository
    {
        public SeckillContext SeckillContext;
        public SeckillRepository(SeckillContext SeckillContext)
        {
            this.SeckillContext = SeckillContext;
        }
        public void Create(Seckill Seckill)
        {
            SeckillContext.Seckills.Add(Seckill);
            SeckillContext.SaveChanges();
        }

        public void Delete(Seckill Seckill)
        {
            SeckillContext.Seckills.Remove(Seckill);
            SeckillContext.SaveChanges();
        }

        public Seckill GetSeckillById(int id)
        {
            return SeckillContext.Seckills.Find(id);
        }

        public IEnumerable<Seckill> GetSeckills()
        {
            return SeckillContext.Seckills.ToList();
        }

        public void Update(Seckill Seckill)
        {
            SeckillContext.Seckills.Update(Seckill);
            SeckillContext.SaveChanges();
        }
        public bool SeckillExists(int id)
        {
            return SeckillContext.Seckills.Any(e => e.Id == id);
        }

        public IEnumerable<Seckill> GetSeckills(Seckill seckill)
        {
            IQueryable<Seckill> query = SeckillContext.Seckills;
            if (seckill.Id != 0)
            {
                query = query.Where(s => s.Id == seckill.Id);
            }
            if (seckill.SeckillType != 0)
            {
                query = query.Where(s => s.SeckillType == seckill.SeckillType);
            }
            if (seckill.SeckillName != null)
            {
                query = query.Where(s => s.SeckillName == seckill.SeckillName);
            }
            if (seckill.SeckillUrl != null)
            {
                query = query.Where(s => s.SeckillUrl == seckill.SeckillUrl);
            }
            if (seckill.SeckillPrice != 0)
            {
                query = query.Where(s => s.SeckillPrice == seckill.SeckillPrice);
            }
            if (seckill.SeckillStock != 0)
            {
                query = query.Where(s => s.SeckillStock == seckill.SeckillStock);
            }
            if (seckill.SeckillPercent != null)
            {
                query = query.Where(s => s.SeckillPercent == seckill.SeckillPercent);
            }
            if (seckill.TimeId != 0)
            {
                query = query.Where(s => s.TimeId == seckill.TimeId);
            }
            if (seckill.ProductId != 0)
            {
                query = query.Where(s => s.ProductId == seckill.ProductId);
            }
            if (seckill.SeckillLimit != 0)
            {
                query = query.Where(s => s.SeckillLimit == seckill.SeckillLimit);
            }
            if (seckill.SeckillDescription != null)
            {
                query = query.Where(s => s.SeckillDescription == seckill.SeckillDescription);
            }
            if (seckill.SeckillIstop != 0)
            {
                query = query.Where(s => s.SeckillIstop == seckill.SeckillIstop);
            }
            if (seckill.SeckillStatus != 0)
            {
                query = query.Where(s => s.SeckillStatus == seckill.SeckillStatus);
            }
            return query;
        }

        public Seckill GetSeckillByProductId(int ProductId)
        {
            List<Seckill> seckills = SeckillContext.Seckills.Where(s => s.ProductId == ProductId).ToList();

            return seckills[0];
        }
    }
}
