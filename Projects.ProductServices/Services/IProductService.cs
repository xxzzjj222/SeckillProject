using Projects.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(int id);
        void Create(Product Product);
        void Update(Product Product);
        void Delete(Product Product);
        bool ProductExists(int id);
    }
}
