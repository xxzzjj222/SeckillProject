using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.ProductServices.Models;
using Projects.ProductServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Projects.ProductServices.Controllers
{
    [Route("Products/{productId}/ProductImages")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }

        // GET: api/ProductImages
        [HttpGet]
        public ActionResult<IEnumerable<ProductImage>> GetProductImages(int ProductId)
        {
            ProductImage productImage = new ProductImage();
            productImage.ProductId = ProductId;
            return productImageService.GetProductImages(productImage).ToList();
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public ActionResult<ProductImage> GetProductImage(int id)
        {
            var productImage = productImageService.GetProductImageById(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage;
        }

        // PUT: api/ProductImages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return BadRequest();
            }

            try
            {
                productImageService.Update(productImage);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
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

        // POST: api/ProductImages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<ProductImage> PostProductImage(ProductImage productImage)
        {
            try
            {
                productImageService.Create(productImage);
            }
            catch (DbUpdateException)
            {
                if (ProductImageExists(productImage.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductImage", new { id = productImage.Id }, productImage);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public ActionResult<ProductImage> DeleteProductImage(int id)
        {
            var productImage = productImageService.GetProductImageById(id);
            if (productImage == null)
            {
                return NotFound();
            }

            productImageService.Delete(productImage);
            return productImage;
        }

        private bool ProductImageExists(int id)
        {
            return productImageService.ProductImageExists(id);
        }
    }
}
