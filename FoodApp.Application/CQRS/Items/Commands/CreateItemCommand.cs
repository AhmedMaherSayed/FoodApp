using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Items.Commands
{
    public record CreateItemCommand(CreateItemDto Dto): IRequest<ResponseViewModel<ItemDto>>;

    public class CreateItemHandler:IRequestHandler<CreateItemCommand, ResponseViewModel<ItemDto>>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public CreateItemHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ResponseViewModel<ItemDto>> Handle(CreateItemCommand req,CancellationToken ct)
        {
            var entity = req.Dto.Map<Item>();
            await _itemRepo.AddAsync(entity);
            var dto = entity.Map<ItemDto>();
            return ResponseViewModel<ItemDto>.Success(dto);
        }
    }

}
