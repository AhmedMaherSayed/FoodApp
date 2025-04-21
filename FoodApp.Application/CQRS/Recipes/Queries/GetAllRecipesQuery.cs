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

namespace FoodApp.Application.CQRS.Recipes.Queries
{
    public record GetAllRecipesQuery() : IRequest<ResponseViewModel<List<RecipeDto>>>;

  
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesQuery, ResponseViewModel<List<RecipeDto>>>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public GetAllRecipesHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ResponseViewModel<List<RecipeDto>>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository.GetAll().ToListAsync(cancellationToken);

            var recipeDtos = recipes.Select(r => r.Map<RecipeDto>()).ToList();

            return ResponseViewModel<List<RecipeDto>>.Success(recipeDtos);
        }
    }



}
