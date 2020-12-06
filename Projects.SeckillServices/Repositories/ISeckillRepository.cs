using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Repositories
{
    public interface ISeckillRepository
    {
        IEnumerable<Seckill> GetSeckills();
        IEnumerable<Seckill> GetSeckills(Seckill seckill);
        Seckill GetSeckillById(int id);
        Seckill GetSeckillByProductId(int ProductId);
        void Create(Seckill Seckill);
        void Update(Seckill Seckill);
        void Delete(Seckill Seckill);
        bool SeckillExists(int id);
    }
}
