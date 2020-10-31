using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillServices.Pos
{
    /// <summary>
    /// 商品值对象、主要接受客户端传来的参数
    /// </summary>
    public class SeckillPo
    {
        // 商品编号
        public int ProductId { set; get; }
        // 商品数量
        public int ProductCount { set; get; }
    }
}
