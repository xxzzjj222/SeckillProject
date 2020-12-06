using Projects.ProductServices.Models;
using Projects.ProductServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Services
{
    public class ProductImageServiceImpl: IProductImageService
    {
        public readonly IProductImageRepository ProductImageRepository;

        public ProductImageServiceImpl(IProductImageRepository ProductImageRepository)
        {
            this.ProductImageRepository = ProductImageRepository;
        }

        public void Create(ProductImage ProductImage)
        {
            ProductImageRepository.Create(ProductImage);
        }

        public void Delete(ProductImage ProductImage)
        {
            ProductImageRepository.Delete(ProductImage);
        }

        public ProductImage GetProductImageById(int id)
        {
            return ProductImageRepository.GetProductImageById(id);
        }

        public IEnumerable<ProductImage> GetProductImages()
        {
            return ProductImageRepository.GetProductImages();
        }

        public void Update(ProductImage ProductImage)
        {
            ProductImageRepository.Update(ProductImage);
        }

        public bool ProductImageExists(int id)
        {
            return ProductImageRepository.ProductImageExists(id);
        }

        public IEnumerable<ProductImage> GetProductImages(ProductImage productImage)
        {
            return ProductImageRepository.GetProductImages(productImage);
        }
    }
}
