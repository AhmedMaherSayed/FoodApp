using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

    }
}
