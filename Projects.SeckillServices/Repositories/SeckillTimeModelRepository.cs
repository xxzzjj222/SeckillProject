using Projects.SeckillServices.Context;
using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Repositories
{
    /// <summary>
    /// 秒杀时间仓储实现
    /// </summary>
    public class SeckillTimeModelRepository : ISeckillTimeModelRepository
    {
        public SeckillContext SeckillContext;
        public SeckillTimeModelRepository(SeckillContext SeckillContext)
        {
            this.SeckillContext = SeckillContext;
        }
        public void Create(SeckillTimeModel SeckillTimeModel)
        {
            SeckillContext.SeckillTimeModels.Add(SeckillTimeModel);
            SeckillContext.SaveChanges();
        }

        public void Delete(SeckillTimeModel SeckillTimeModel)
        {
            SeckillContext.SeckillTimeModels.Remove(SeckillTimeModel);
            SeckillContext.SaveChanges();
        }

        public SeckillTimeModel GetSeckillTimeModelById(int id)
        {
            return SeckillContext.SeckillTimeModels.Find(id);
        }

        public IEnumerable<SeckillTimeModel> GetSeckillTimeModels()
        {
            return SeckillContext.SeckillTimeModels.ToList();
        }

        public void Update(SeckillTimeModel SeckillTimeModel)
        {
            SeckillContext.SeckillTimeModels.Update(SeckillTimeModel);
            SeckillContext.SaveChanges();
        }
        public bool SeckillTimeModelExists(int id)
        {
            return SeckillContext.SeckillTimeModels.Any(e => e.Id == id);
        }


    }
}
