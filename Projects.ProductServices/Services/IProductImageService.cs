using Projects.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Services
{
    public interface IProductImageService
    {
        IEnumerable<ProductImage> GetProductImages();
        IEnumerable<ProductImage> GetProductImages(ProductImage productImage);
        ProductImage GetProductImageById(int id);
        void Create(ProductImage ProductImage);
        void Update(ProductImage ProductImage);
        void Delete(ProductImage ProductImage);
        bool ProductImageExists(int id);
    }
}
