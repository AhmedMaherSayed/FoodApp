using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Items.Commands
{
    public record UpdateItemCommand(UpdateItemDto Dto): IRequest<ResponseViewModel<ItemDto>>;

    public class UpdateItemHandler:IRequestHandler<UpdateItemCommand, ResponseViewModel<ItemDto>>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public UpdateItemHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ResponseViewModel<ItemDto>> Handle(UpdateItemCommand req,CancellationToken ct)
        {
            var item = await _itemRepo.GetByIdWithTrackingAsync(req.Dto.Id);
            if (item is null)
                return ResponseViewModel<ItemDto>.Failure(ErrorCode.NotFound,"Item not found");

            var updated = req.Dto.Map<Item>();
            _itemRepo.UpdateInclude(updated,
                nameof(Item.CategoryId),
                nameof(Item.Price),
                nameof(Item.discount),
                nameof(Item.ImageURL)
            );

            var dto = updated.Map<ItemDto>();
            return ResponseViewModel<ItemDto>.Success(dto);
        }
    }


}
