using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniMarket.API.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } // UUID
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")] // NUMERIC wg specyfikacji
        public decimal Price { get; set; }

        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // TIMESTAMPTZ

        // Relacja do User (Sprzedawcy)
        public Guid SellerId { get; set; }
        public User? Seller { get; set; }
    }
}