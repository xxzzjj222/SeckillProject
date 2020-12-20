using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Context
{
    public class SeckillAggregateServicesContext:DbContext
    {
        public SeckillAggregateServicesContext(DbContextOptions<SeckillAggregateServicesContext> options)
            :base(options)
        {

        }
    }
}
