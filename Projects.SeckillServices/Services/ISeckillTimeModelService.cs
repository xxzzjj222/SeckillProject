using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Services
{
    public interface ISeckillTimeModelService
    {
        IEnumerable<SeckillTimeModel> GetSeckillTimeModels();
        SeckillTimeModel GetSeckillTimeModelById(int id);
        void Create(SeckillTimeModel SeckillTime);
        void Update(SeckillTimeModel SeckillTime);
        void Delete(SeckillTimeModel SeckillTime);
        bool SeckillTimeModelExists(int id);
    }
}
