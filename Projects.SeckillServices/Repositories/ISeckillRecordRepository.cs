using Projects.SeckillServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Repositories
{
    public interface ISeckillRecordRepository
    {
        IEnumerable<SeckillRecord> GetSeckillRecords();
        SeckillRecord GetSeckillRecordById(int id);
        void Create(SeckillRecord SeckillRecord);
        void Update(SeckillRecord SeckillRecord);
        void Delete(SeckillRecord SeckillRecord);
        bool SeckillRecordExists(int id);
    }
}
