using AutoMapper.QueryableExtensions;
using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Recipe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Recipes.Queries
{
    public record GetRecipesByUserIdQuery(int userId) : IRequest<ResponseViewModel<List<RecipeDto>>>;

    public class GetRecipesByUserIdHandler : IRequestHandler<GetRecipesByUserIdQuery, ResponseViewModel<List<RecipeDto>>>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public GetRecipesByUserIdHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ResponseViewModel<List<RecipeDto>>> Handle(GetRecipesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository
                .Get(x => x.UserId == request.userId)
                .Select(r => r.Map<RecipeDto>())
                .ToListAsync(cancellationToken);

            return ResponseViewModel<List<RecipeDto>>.Success(recipes);
        }
    }
}
