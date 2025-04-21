using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; } = default!;

    }
}
