using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Recipe
{
    public class RecipeDto
    {

        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public List<string> Steps { get; set; } = new();
        public decimal PrepTimeMinutes { get; set; }
        public decimal CookTimeMinutes { get; set; }

    }
}
