using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.DTOs
{
    public class UpdateRecipeDto:CreateRecipeDto
    {
        public int Id { get; set; }

    }
}
