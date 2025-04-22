using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Items.Queries
{
    public record GetItemsByCategoryQuery(int CategoryId): IRequest<ResponseViewModel<List<ItemDto>>>;

    public class GetItemsByCategoryHandler: IRequestHandler<GetItemsByCategoryQuery, ResponseViewModel<List<ItemDto>>>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public GetItemsByCategoryHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ResponseViewModel<List<ItemDto>>> Handle(GetItemsByCategoryQuery req,CancellationToken ct)
        {
            var items = await _itemRepo.Get(i => i.CategoryId == req.CategoryId)
                .ToListAsync(ct);

            var dtos = items.Select(i => i.Map<ItemDto>()).ToList();
            return ResponseViewModel<List<ItemDto>>.Success(dtos);
        }
    }

}
