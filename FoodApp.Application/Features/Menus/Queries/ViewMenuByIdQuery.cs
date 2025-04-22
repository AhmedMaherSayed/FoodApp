using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Menus.Queries
{
    public record ViewMenuByIdQuery(int MenuId) : IRequest<ResponseViewModel<MenuDetailDto>>;

    public class ViewMenuByIdHandler : IRequestHandler<ViewMenuByIdQuery, ResponseViewModel<MenuDetailDto>>
    {
        private readonly IGenericRepository<Menu> _menuRepo;
        private readonly IGenericRepository<Item> _itemRepo;

        public ViewMenuByIdHandler(IGenericRepository<Menu> menuRepo, IGenericRepository<Item> itemRepo)
        {
            _menuRepo = menuRepo;
            _itemRepo = itemRepo;
        }

        public async Task<ResponseViewModel<MenuDetailDto>> Handle(ViewMenuByIdQuery request, CancellationToken ct)
        {
            var menu = await _menuRepo.GetAll()
                .FirstOrDefaultAsync(m => m.Id == request.MenuId, ct);

            if (menu is null)
                return ResponseViewModel<MenuDetailDto>.Failure(ErrorCode.NotFound, $"Menu with Id {request.MenuId} not found.");

            var itemDtos = new List<MenuItemDto>();
            foreach (var item in menu.Items)
            {
                var dbItem = await _itemRepo.GetByIdAsync(item.Id);
                if (dbItem != null)
                    itemDtos.Add(dbItem.Map<MenuItemDto>());
            }

            var detail = menu.Map<MenuDetailDto>();
            detail.Items = itemDtos;

            return ResponseViewModel<MenuDetailDto>.Success(detail);
        }
    }



}
