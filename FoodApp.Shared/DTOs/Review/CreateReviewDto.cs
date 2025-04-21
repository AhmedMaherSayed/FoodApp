using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Review
{
    public class CreateReviewDto
    {
        public int UserId { get; set; }
        public int RecipeItemId { get; set; }
        public int Rate { get; set; }             
        public string Comment { get; set; }
    }
}
