using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Steps { get; set; }
        public decimal PrepTime { get; set; }
        public decimal CookTime { get; set; }
        public int UserId { get; set; }
        public int? CategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }

    }
}
