using FoodApp.Application.Features.Auth.Commands;
using FoodApp.Application.Interfaces;
using FoodApp.Domain.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Auth.Handlers
{
    public class RegisterHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwt;

        public RegisterHandler(UserManager<User> userManager, IJwtTokenGenerator jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));


            return _jwt.GenerateToken(user);
        }
    }
}
