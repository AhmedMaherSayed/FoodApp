using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.DTOs.Item
{
    public class UpdateItemDto : CreateItemDto
    {
        public int Id { get; set; }
    }
}
