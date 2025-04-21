using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.ViewModel.Category;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(int categoryId, UpdateCategoryViewModel updateCategory) : IRequest<ResponseViewModel<bool>>;

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, ResponseViewModel<bool>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public UpdateCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseViewModel<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.updateCategory == null)
            {
                return ResponseViewModel<bool>.Failure(ErrorCode.BadRequest, "Update data is required.");
            }

            await _categoryRepository.BulkUpdateAsync(x => x.Id == request.categoryId,
                x => x.SetProperty(x => x.Name, request.updateCategory.Name)
                .SetProperty(x => x.Description, request.updateCategory.Description));

            return ResponseViewModel<bool>.Success(true);
        }
    }
}
