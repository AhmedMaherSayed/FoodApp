using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using MediatR;

namespace FoodApp.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : IRequest<ResponseViewModel<bool>>;

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseViewModel<bool>>
    {
        public async Task<ResponseViewModel<bool>> Handle(DeleteUserCommand req, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
