using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.ViewModel.User;
using MediatR;

namespace FoodApp.Application.Features.Users.Queries
{
    public record GetAllUsersQuery : IRequest<ResponseViewModel<List<UserViewModel>>>;

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResponseViewModel<List<UserViewModel>>>
    {
        public async Task<ResponseViewModel<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
