using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.ViewModel.Item
{
    public class RecipeItemViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
