using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Recipe;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Items.Commands
{
    public record AddItemToRecipeCommand(RecipeItemDto Dto): IRequest<ResponseViewModel<RecipeItemDto>>;

    public class AddItemToRecipeHandler: IRequestHandler<AddItemToRecipeCommand, ResponseViewModel<RecipeItemDto>>
    {
        private readonly IGenericRepository<RecipeItem> _riRepo;

        public AddItemToRecipeHandler(IGenericRepository<RecipeItem> riRepo)
            => _riRepo = riRepo;

        public async Task<ResponseViewModel<RecipeItemDto>> Handle(AddItemToRecipeCommand req,CancellationToken ct)
        {
            var entity = req.Dto.Map<RecipeItem>();
            await _riRepo.AddAsync(entity);
            var dto = entity.Map<RecipeItemDto>();
            return ResponseViewModel<RecipeItemDto>.Success(dto);
        }
    }

}
