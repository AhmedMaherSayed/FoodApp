using AutoMapper;
using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
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
    public record GetMenusQuery() : IRequest<ResponseViewModel<List<MenuDto>>>;

    public class GetMenusHandler : IRequestHandler<GetMenusQuery, ResponseViewModel<List<MenuDto>>>
    {
        private readonly IGenericRepository<Menu> _menuRepo;

        public GetMenusHandler(IGenericRepository<Menu> menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public async Task<ResponseViewModel<List<MenuDto>>> Handle(GetMenusQuery request, CancellationToken ct)
        {
            var menus = await _menuRepo.GetAll()
                .OrderBy(m => m.Title)
                .ToListAsync(ct);

            var dtoList = menus.Select(m => m.Map<MenuDto>()).ToList();

            return ResponseViewModel<List<MenuDto>>.Success(dtoList);
        }
    }


}
