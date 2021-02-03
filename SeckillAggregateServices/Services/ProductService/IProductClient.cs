using Projects.Cores.MicroClient.Attributes;
using SeckillAggregateServices.Models.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeckillAggregateServices.Services.ProductService
{
    [MicroClient(urlScheme:"http",serviceName:"ProductServices")]
    public interface IProductClient
    {
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products")]
        public List<Product> GetProductList();


        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products/{productId}")]
        public Product GetProduct(int productId);

        /// <summary>
        /// 扣减商品库存
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCount"></param>
        /// <returns></returns>
        [PutPath("/Products/{ProductId}/set-stock")]
        public void ProductSetStock(int ProductId, int ProductCount);
    }
}
