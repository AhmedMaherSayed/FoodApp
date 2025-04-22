using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using FoodApp.Shared.DTOs.Menu;
using FoodApp.Shared.ViewModel.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Menus.Commands
{
    //public record AddItemToMenuCommand(AddItemToMenuDto Dto): IRequest<ResponseViewModel<bool>>;
    public record AddItemToMenuCommand(AddItemToMenuDto Dto): IRequest<ResponseViewModel<MenuItemDto>>;

    public class AddItemToMenuHandler: IRequestHandler<AddItemToMenuCommand, ResponseViewModel<MenuItemDto>>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public AddItemToMenuHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ResponseViewModel<MenuItemDto>> Handle(AddItemToMenuCommand req,CancellationToken ct)
        {
            var entity = await _itemRepo.GetByIdWithTrackingAsync(req.Dto.ItemId);
                if(entity is null)
                return ResponseViewModel<MenuItemDto>.Failure(ErrorCode.NotFound, "Item not found");

            entity.MenuId = req.Dto.MenuId;
            await _itemRepo.AddAsync(entity);

            var dto = entity.Map<MenuItemDto>();
            return ResponseViewModel<MenuItemDto>.Success(dto);
        }
    }
}
