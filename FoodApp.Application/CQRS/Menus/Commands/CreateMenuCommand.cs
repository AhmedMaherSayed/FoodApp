using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
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
    public record CreateMenuCommand(CreateMenuDto Dto): IRequest<ResponseViewModel<MenuDto>>;

    public class CreateMenuHandler: IRequestHandler<CreateMenuCommand, ResponseViewModel<MenuDto>>
    {
        private readonly IGenericRepository<Menu> _menuRepo;

        public CreateMenuHandler(IGenericRepository<Menu> menuRepo)
            => _menuRepo = menuRepo;

        public async Task<ResponseViewModel<MenuDto>> Handle(CreateMenuCommand req,CancellationToken ct)
        {
            var entity = req.Dto.Map<Menu>();
            await _menuRepo.AddAsync(entity);

            var dto = entity.Map<MenuDto>();
            return ResponseViewModel<MenuDto>.Success(dto);
        }
    }

}
