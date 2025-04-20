using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Recipe : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public decimal PrepTime { get; set; }
        public decimal CookTime { get; set; }
        public string ImageURL { get; set; }

        public User User { get; set; }
        public ICollection<RecipeItem> Items { get; set; }
        public ICollection<Favorite> Users { get; set; }
    }
}
