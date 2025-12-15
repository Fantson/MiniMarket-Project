using System;

namespace MiniMarket.WPF
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }        // Znak zapytania ? oznacza, ¿e mo¿e byæ pusty
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public Guid SellerId { get; set; }
    }
}