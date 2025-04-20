using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Domain.Data.Enums
{
    public enum ErrorCode
    {

        None = 0,

        Ok = 200,

        Unauthorized = 401,
        Forbidden = 403,
        BadRequest = 400,
        NotFound = 404,

    }
}
