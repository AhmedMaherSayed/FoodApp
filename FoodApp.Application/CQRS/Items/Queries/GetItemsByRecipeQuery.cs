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

namespace FoodApp.Application.CQRS.Items.Queries
{
    public record GetItemsByRecipeQuery(int RecipeId): IRequest<ResponseViewModel<List<ItemDto>>>;

    public class GetItemsByRecipeHandler: IRequestHandler<GetItemsByRecipeQuery, ResponseViewModel<List<ItemDto>>>
    {
        private readonly IGenericRepository<RecipeItem> _riRepo;
        private readonly IGenericRepository<Item> _itemRepo;

        public GetItemsByRecipeHandler(
            IGenericRepository<RecipeItem> riRepo,
            IGenericRepository<Item> itemRepo)
        {
            _riRepo = riRepo;
            _itemRepo = itemRepo;
        }

        public async Task<ResponseViewModel<List<ItemDto>>> Handle(GetItemsByRecipeQuery req,CancellationToken ct)
        {
            var links = await _riRepo.GetAll()
                .Where(ri => ri.RecipeId == req.RecipeId)
                .ToListAsync(ct);

            var items = new List<ItemDto>();
            foreach (var link in links)
            {
                var item = await _itemRepo.GetByIdAsync(link.ItemId);
                if (item != null)
                    items.Add(item.Map<ItemDto>());
            }

            return ResponseViewModel<List<ItemDto>>.Success(items);
        }
    }

}
