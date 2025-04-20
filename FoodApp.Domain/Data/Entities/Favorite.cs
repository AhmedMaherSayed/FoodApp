using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
        public int FoodItemId { get; set; }

        public User User { get; set; }
        public FoodItem FoodItem { get; set; }

    }
}
