using FoodApp.Application.DTOs;
using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Recipes.Queries
{
    public record GetRecipeByIdQuery(int Id) : IRequest<ResponseViewModel<RecipeDto>>;

    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, ResponseViewModel<RecipeDto>>
    {
        private readonly IGenericRepository<Recipe> _repo;

        public GetRecipeByIdHandler(IGenericRepository<Recipe> repo)
            => _repo = repo;

        public async Task<ResponseViewModel<RecipeDto>> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _repo.GetByIdAsync(request.Id);
            if (recipe == null)
                return ResponseViewModel<RecipeDto>.Failure(ErrorCode.NotFound,"Recipe not found");

            var dto = recipe.Map<RecipeDto>();
            return ResponseViewModel<RecipeDto>.Success(dto);
        }

    }

}
