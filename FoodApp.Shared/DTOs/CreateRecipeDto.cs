using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.DTOs
{
    public class CreateRecipeDto
    {

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public IEnumerable<string> Steps { get; set; } = Enumerable.Empty<string>();
        public decimal PrepTimeMinutes { get; set; }
        public decimal CookTimeMinutes { get; set; }
    }
}
