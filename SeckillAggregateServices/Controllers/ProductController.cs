using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeckillAggregateServices.Models.ProductService;
using SeckillAggregateServices.Models.UserService;
using SeckillAggregateServices.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 商品聚合控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductClient productClient;
        private readonly IProductImageClient productImageClient;

        public ProductController(IProductClient productClient,
                                IProductImageClient productImageClient)
        {
            this.productClient = productClient;
            this.productImageClient = productImageClient;
        }

        /// <summary>
        /// 商品详情查询
        /// </summary>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public Product GetProductDetail(User user, int productId)
        {
            // 1、查询商品
            Product product = productClient.GetProduct(productId);

            // 2、查询商品轮播图
            List<ProductImage> productImages = productImageClient.GetProductImages(productId);

            // 3、商品设置图片
            product.Images = productImages;
            return product;
        }
    }
}
