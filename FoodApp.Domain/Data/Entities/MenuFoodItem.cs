using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class MenuFoodItem : BaseEntity
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }

        public Menu Menu { get; set; }
        public FoodItem FoodItem { get; set; }

    }
}
