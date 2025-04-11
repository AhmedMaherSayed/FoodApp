using FoodApp.Application.Features.Auth.Commands;
using FoodApp.Application.Interfaces;
using FoodApp.Domain.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenGenerator jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                       ?? throw new Exception("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new Exception("Invalid credentials");

            return _jwt.GenerateToken(user);
        }
    }
}
