using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.ProductService
{
    public interface IProductImageClient
    {
        /// <summary>
        /// 查询所有商品图片信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GetPath("/Products/{productId}/ProductImages")]
        public List<ProductImage> GetProductImages(int productId);
    }
}
