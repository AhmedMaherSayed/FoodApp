using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.ViewModel.Menu
{
    public class CreateMenuViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public List<int> ItemIds { get; set; }
    }
}
