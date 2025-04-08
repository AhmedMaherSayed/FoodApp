using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

    }
}
