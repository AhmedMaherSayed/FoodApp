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
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Menus.Commands
{
    public record RemoveItemFromMenuCommand(int MenuId, int ItemId) : IRequest<ResponseViewModel<ItemForMenuViewModel>>;

    


    public class RemoveItemFromMenuHandler : IRequestHandler<RemoveItemFromMenuCommand, ResponseViewModel<ItemForMenuViewModel>>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public RemoveItemFromMenuHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<ResponseViewModel<ItemForMenuViewModel>> Handle(RemoveItemFromMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdWithTrackingAsync(request.MenuId);
            if (menu == null)
            {
                return ResponseViewModel<ItemForMenuViewModel>.Failure(ErrorCode.NotFound, "Menu not found.");
            }

            var item = menu.Items.FirstOrDefault(i => i.Id == request.ItemId);
            if (item == null)
            {
                return ResponseViewModel<ItemForMenuViewModel>.Failure(ErrorCode.NotFound, "Item not found in the menu.");
            }

            menu.Items.Remove(item);

            await _menuRepository.UpdateAsync(menu);

            var viewModel = item.Map<ItemForMenuViewModel>();

            return ResponseViewModel<ItemForMenuViewModel>.Success(viewModel);
        }

    }
}
