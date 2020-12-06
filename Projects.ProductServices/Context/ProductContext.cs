using Microsoft.EntityFrameworkCore;
using Projects.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)

        {

        }


        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
