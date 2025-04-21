using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeItemId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
