﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Menu : BaseEntity
    {
        //public string Name { get; set; }

        //public ICollection<MenuRecipeItem> MenuRecipeItems { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }

        public List<Item> Items { get; set; }


    }
}
