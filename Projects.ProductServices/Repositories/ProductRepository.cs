using Projects.ProductServices.Context;
using Projects.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Repositories
{
    public class ProductRepository:IProductRepository
    {
        public ProductContext ProductContext;
        public ProductRepository(ProductContext ProductContext)
        {
            this.ProductContext = ProductContext;
        }
        public void Create(Product Product)
        {
            ProductContext.Products.Add(Product);
            ProductContext.SaveChanges();
        }

        public void Delete(Product Product)
        {
            ProductContext.Products.Remove(Product);
            ProductContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return ProductContext.Products.Find(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductContext.Products.ToList();
        }

        public void Update(Product Product)
        {
            ProductContext.Products.Update(Product);
            ProductContext.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return ProductContext.Products.Any(e => e.Id == id);
        }
    }
}
