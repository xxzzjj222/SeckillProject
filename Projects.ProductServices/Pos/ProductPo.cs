using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Pos
{
    /// <summary>
    /// 商品值对象，主要接收客户端传来的参数
    /// </summary>
    public class ProductPo
    {
        // 商品编号
        public int ProductId { set; get; }
        // 商品数量
        public int ProductCount { set; get; }
    }
}
