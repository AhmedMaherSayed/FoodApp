using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Menu
{
    public class MenuDetailDto : MenuDto
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<MenuItemDto> Items { get; set; } = new();
    }
}
