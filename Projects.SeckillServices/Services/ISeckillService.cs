using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Services
{
    public interface ISeckillService
    {
        IEnumerable<Seckill> GetSeckills();
        IEnumerable<Seckill> GetSeckills(Seckill seckill);
        Seckill GetSeckillById(int id);
        public Seckill GetSeckillByProductId(int ProductId);
        void Create(Seckill Seckill);
        void Update(Seckill Seckill);
        void Delete(Seckill Seckill);
        bool SeckillExists(int id);
    }
}
