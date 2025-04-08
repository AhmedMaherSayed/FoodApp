using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Entities
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
