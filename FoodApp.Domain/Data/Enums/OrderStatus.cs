using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Enums
{
    public enum OrderStatus
    {

        Pending = 1,
        Preparing = 2,
        OutForDelivery = 3,
        Delivered = 4,
        Cancelled = 5

    }
}
