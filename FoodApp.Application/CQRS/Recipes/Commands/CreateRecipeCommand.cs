using FoodApp.Application.DTOs;
using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;

//using FoodApp.Presentation.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Recipes.Commands
{
    public record CreateRecipeCommand(CreateRecipeDto Dto) : IRequest<ResponseViewModel<RecipeDto>>;

    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, ResponseViewModel<RecipeDto>>
    {
        private readonly IGenericRepository<Recipe> _repo;

        public CreateRecipeHandler(IGenericRepository<Recipe> repo)
            => _repo = repo;
        public async Task<ResponseViewModel<RecipeDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = request.Dto.Map<Recipe>();
            await _repo.AddAsync(recipe);

            var dto = recipe.Map<RecipeDto>();
            return ResponseViewModel<RecipeDto>.Success(dto);
        }

    }

}
