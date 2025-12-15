using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMarket.API.Data;
using MiniMarket.API.Models;

namespace MiniMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MiniMarketContext _context;

        public ProductController(MiniMarketContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            // Opcjonalnie: Jeœli nie podano ID, generujemy nowe
            if (product.Id == Guid.Empty) product.Id = Guid.NewGuid();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        // DELETE: api/Product/{id}  <-- TO JEST KLUCZOWE DLA USUWANIA
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Product/apply-discount (PROCEDURA SK£ADOWANA)
        [HttpPost("apply-discount")]
        public async Task<IActionResult> ApplyDiscount(string category, decimal percentage)
        {
            // Wywo³anie procedury SQL
            await _context.Database.ExecuteSqlRawAsync(
                "CALL \"ApplyDiscount\"({0}, {1})", category, percentage);

            return Ok($"Zastosowano rabat {percentage}% dla kategorii {category}");
        }
    }
}