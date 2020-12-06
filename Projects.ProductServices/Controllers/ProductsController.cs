using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Common.Exceptions;
using Projects.ProductServices.Models;
using Projects.ProductServices.Pos;
using Projects.ProductServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.ProductServices.Controllers
{
    [Route("Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return productService.GetProducts().ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                productService.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 修改商品库存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}/set-stock")]
        public IActionResult PutProductStock(int id, ProductPo productVo)
        {
            if (id != productVo.ProductId)
            {
                return BadRequest();

            }
            // 1、查询商品
            Product product = productService.GetProductById(productVo.ProductId);

            // 2、判断商品库存是否完成
            if (product.ProductStock <= 0)
            {
                throw new BizException("库存完了");
            }

            // 3、扣减商品库存
            product.ProductStock = product.ProductStock - productVo.ProductCount;

            // 4、更新商品库存
            productService.Update(product);

            return Ok("更新库存成功");
        }

        /// <summary>
        /// 异步更新秒杀库存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productVo"></param>
        /// <returns></returns>
        [NonAction]
        //[CapSubscribe("product.stock")]
        public IActionResult SetProductStock(ProductPo productVo)
        {
            // 1、查询商品
            Product product = productService.GetProductById(productVo.ProductId);

            // 2、判断商品库存是否完成
            if (product.ProductStock <= 0)
            {
                throw new BizException("库存完了");
            }

            // 3、扣减商品库存
            product.ProductStock = product.ProductStock - productVo.ProductCount;

            // 4、更新商品库存
            productService.Update(product);
            return Ok("更新库存成功");
        }

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            productService.Create(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            productService.Delete(product);
            return product;
        }

        private bool ProductExists(int id)
        {
            return productService.ProductExists(id);
        }
    }
}
