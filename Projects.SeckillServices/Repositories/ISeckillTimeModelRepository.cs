using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Repositories
{
    public interface ISeckillTimeModelRepository
    {
        IEnumerable<SeckillTimeModel> GetSeckillTimeModels();
        SeckillTimeModel GetSeckillTimeModelById(int id);
        void Create(SeckillTimeModel SeckillTimeModel);
        void Update(SeckillTimeModel SeckillTimeModel);
        void Delete(SeckillTimeModel SeckillTimeModel);
        bool SeckillTimeModelExists(int id);
    }
}
