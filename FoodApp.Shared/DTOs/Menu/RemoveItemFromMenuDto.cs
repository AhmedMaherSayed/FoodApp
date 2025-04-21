using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Menu
{
    public class RemoveItemFromMenuDto
    {
        public int MenuId { get; set; }
        public int ItemId { get; set; }
    }
}
