using AutoMapper;
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

namespace FoodApp.Application.CQRS.Recipes.Commands
{
    public record UpdateRecipeCommand(UpdateRecipeDto Dto) : IRequest<ResponseViewModel<RecipeDto>>;

    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, ResponseViewModel<RecipeDto>>
    {
        //private readonly IGenericRepository<Recipe> _repo;
        //private readonly IMapper _mapper;

        //public UpdateRecipeHandler(IGenericRepository<Recipe> repo, IMapper mapper)
        //{
        //    _repo = repo;
        //    _mapper = mapper;
        //}

        //public async Task<RecipeDto> Handle(UpdateRecipeCommand req,CancellationToken ct)
        //{
        //    //var entity = req.Dto.Map<Recipe>(); 

        //    //_repo.Update(entity);              

        //    //var result = entity.Map<RecipeDto>();
        //    //return Task.FromResult(result);    


        //    var existing = await _repo.GetByIdWithTrackingAsync(req.Dto.Id);
        //    if (existing is null)
        //        throw new KeyNotFoundException($"Recipe with Id {req.Dto.Id} not found.");

        //    _mapper.Map(req.Dto, existing);

        //    var modifiedProps = new[]
        //    {
        //    nameof(Recipe.Title),
        //    nameof(Recipe.Description),
        //    nameof(Recipe.ImageUrl),
        //    nameof(Recipe.Steps),
        //    nameof(Recipe.PrepTime),
        //    nameof(Recipe.CookTime)
        //};
        //    _repo.UpdateInclude(existing, modifiedProps);

        //    var resultDto = existing.Map<RecipeDto>();
        //    return resultDto;


        //}

        private readonly IGenericRepository<Recipe> _recipeRepository;

        public UpdateRecipeHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ResponseViewModel<RecipeDto>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository.GetByIdWithTrackingAsync(request.Dto.Id);
            if (recipe == null)
                return ResponseViewModel<RecipeDto>.Failure(ErrorCode.NotFound,"Recipe not found");

            var updated = request.Dto.Map<Recipe>();

            _recipeRepository.UpdateInclude(updated,
                nameof(Recipe.Title),
                nameof(Recipe.Description),
                nameof(Recipe.ImageUrl),
                nameof(Recipe.Steps),
                nameof(Recipe.PrepTime),
                nameof(Recipe.CookTime)
            );

            var dto = updated.Map<RecipeDto>();
            return ResponseViewModel<RecipeDto>.Success(dto);
        }

    }

}
