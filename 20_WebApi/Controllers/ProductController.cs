using _20_WebApi.DataContext;
using _20_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20_WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        // Get => Bir datayı getirmek için kullanılır
        [HttpGet("get-products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
           return await _context.Products.ToListAsync();
        }

        [HttpGet("get-product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("add-product")] // Http post => servise veri gönderme durumunda kullanılır
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (ProductExists(product.Id))
            {
                return BadRequest("Zaten ürün mevcut");
            }
            if(product is null)
            {
                return BadRequest("Ürün oluşturulamadı.Requestte hata var");
            }

            _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct),new {Id = product.Id},product);
        }

        [HttpPut("update-product")] // Http put => güncelleme için kullanılır.
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok("Ürününüz silindi");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
