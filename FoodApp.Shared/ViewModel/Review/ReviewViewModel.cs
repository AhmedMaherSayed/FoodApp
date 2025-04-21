using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Shared.ViewModel.Review
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
