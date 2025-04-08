using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public int FoodItemId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public FoodItem FoodItem { get; set; }

    }
}
