using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Items.Commands
{
    public record DeleteItemCommand(int Id): IRequest<ResponseViewModel<ItemDto>>;

    public class DeleteItemHandler: IRequestHandler<DeleteItemCommand, ResponseViewModel<ItemDto>>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public DeleteItemHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ResponseViewModel<ItemDto>> Handle(DeleteItemCommand req,CancellationToken ct)
        {
            if (await _itemRepo.Get(i => i.Id == req.Id && /* check order logic */ false)
                              .AnyAsync(ct))
            {
                return ResponseViewModel<ItemDto>.Failure(ErrorCode.BadRequest,"Cannot delete: item part of an active order");
            }

            var entity = await _itemRepo.GetByIdWithTrackingAsync(req.Id);
            if (entity is null)
                return ResponseViewModel<ItemDto>.Failure(ErrorCode.NotFound,"Item not found");

            await _itemRepo.DeleteAsync(req.Id);
            var dto = entity.Map<ItemDto>();
            return ResponseViewModel<ItemDto>.Success(dto);
        }
    }

}
