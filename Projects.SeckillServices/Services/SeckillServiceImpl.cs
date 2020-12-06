using Projects.SeckillServices.Models;
using Projects.SeckillServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Services
{
    /// <summary>
    /// 秒杀服务实现
    /// </summary>
    public class SeckillServiceImpl : ISeckillService
    {
        public readonly ISeckillRepository SeckillRepository;

        public SeckillServiceImpl(ISeckillRepository SeckillRepository)
        {
            this.SeckillRepository = SeckillRepository;
        }

        public void Create(Seckill Seckill)
        {
            SeckillRepository.Create(Seckill);
        }

        public void Delete(Seckill Seckill)
        {
            SeckillRepository.Delete(Seckill);
        }

        public Seckill GetSeckillById(int id)
        {
            return SeckillRepository.GetSeckillById(id);
        }

        public IEnumerable<Seckill> GetSeckills()
        {
            return SeckillRepository.GetSeckills();
        }

        public void Update(Seckill Seckill)
        {
            SeckillRepository.Update(Seckill);
        }

        public bool SeckillExists(int id)
        {
            return SeckillRepository.SeckillExists(id);
        }

        public IEnumerable<Seckill> GetSeckills(Seckill seckill)
        {

            return SeckillRepository.GetSeckills(seckill);
        }

        public Seckill GetSeckillByProductId(int ProductId)
        {
            return SeckillRepository.GetSeckillByProductId(ProductId);
        }
    }
}
