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

namespace FoodApp.Application.CQRS.Menus.Commands
{
    public record DeleteMenuCommand(int Id): IRequest<ResponseViewModel<MenuDto>>;

    public class DeleteMenuHandler
    : IRequestHandler<DeleteMenuCommand, ResponseViewModel<MenuDto>>
    {
        private readonly IGenericRepository<Menu> _menuRepo;

        public DeleteMenuHandler(IGenericRepository<Menu> menuRepo)
            => _menuRepo = menuRepo;

        public async Task<ResponseViewModel<MenuDto>> Handle(DeleteMenuCommand req,CancellationToken ct)
        {
            if (await _menuRepo
                      .Get(m => m.Id == req.Id)
                      .AnyAsync(ct))
            {
                return ResponseViewModel<MenuDto>.Failure(
                    ErrorCode.BadRequest,
                    "Cannot delete: menu is in active use");
            }

            var entity = await _menuRepo.GetByIdWithTrackingAsync(req.Id);
            if (entity is null)
                return ResponseViewModel<MenuDto>.Failure(
                    ErrorCode.NotFound,
                    "Menu not found");

            await _menuRepo.DeleteAsync(req.Id);
            var dto = entity.Map<MenuDto>();
            return ResponseViewModel<MenuDto>.Success(dto);
        }
    }

}
