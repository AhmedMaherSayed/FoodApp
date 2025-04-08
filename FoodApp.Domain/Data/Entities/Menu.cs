using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<MenuFoodItem> MenuFoodItems { get; set; }

    }
}
