using FoodApp.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);

    }
}
