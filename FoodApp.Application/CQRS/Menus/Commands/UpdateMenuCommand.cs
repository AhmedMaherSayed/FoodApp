using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Menus.Commands
{
    public record UpdateMenuCommand(EditMenuDto Dto): IRequest<ResponseViewModel<MenuDto>>;

    public class UpdateMenuHandler: IRequestHandler<UpdateMenuCommand, ResponseViewModel<MenuDto>>
    {
        private readonly IGenericRepository<Menu> _menuRepo;

        public UpdateMenuHandler(IGenericRepository<Menu> menuRepo)
            => _menuRepo = menuRepo;

        public async Task<ResponseViewModel<MenuDto>> Handle(UpdateMenuCommand req,CancellationToken ct)
        {
            var existing = await _menuRepo.GetByIdWithTrackingAsync(req.Dto.Id);
            if (existing is null)
                return ResponseViewModel<MenuDto>.Failure(ErrorCode.NotFound,"Menu not found");

            _menuRepo.UpdateInclude(
                req.Dto.Map<Menu>(),
                nameof(Menu.Title),
                nameof(Menu.Description),
                nameof(Menu.ImageUrl),
                nameof(Menu.CategoryId)
            );

            var dto = existing.Map<MenuDto>();
            return ResponseViewModel<MenuDto>.Success(dto);
        }

    }
}
