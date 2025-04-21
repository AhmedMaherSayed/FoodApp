using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Menu
{
    public class CreateMenuDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MealTime { get; set; } 
        public List<int> ItemIds { get; set; } 
    }
}
