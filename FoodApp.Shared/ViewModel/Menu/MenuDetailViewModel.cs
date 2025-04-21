using FoodApp.Shared.DTOs.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.ViewModel.Menu
{
    public class MenuDetailViewModel
    {

        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<MenuItemDto> Items { get; set; } = new();

    }
}
