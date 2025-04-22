using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Recipe;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Items.Commands
{
    public record RemoveItemFromRecipeCommand(int RecipeId, int ItemId): IRequest<ResponseViewModel<RecipeItemDto>>;

    public class RemoveItemFromRecipeCommandHandler: IRequestHandler<RemoveItemFromRecipeCommand, ResponseViewModel<RecipeItemDto>>
    {
        private readonly IGenericRepository<RecipeItem> _riRepo;

        public RemoveItemFromRecipeCommandHandler(IGenericRepository<RecipeItem> riRepo)
            => _riRepo = riRepo;

        public async Task<ResponseViewModel<RecipeItemDto>> Handle(RemoveItemFromRecipeCommand req,CancellationToken ct)
        {
            var entity = await _riRepo.GetAll()
                .FirstOrDefaultAsync(ri => ri.RecipeId == req.RecipeId && ri.ItemId == req.ItemId, ct);

            if (entity is null)
                return ResponseViewModel<RecipeItemDto>.Failure(ErrorCode.NotFound,"Link not found");

            await _riRepo.DeleteAsync(entity.Id);
            var dto = entity.Map<RecipeItemDto>();
            return ResponseViewModel<RecipeItemDto>.Success(dto);
        }
    }

}
