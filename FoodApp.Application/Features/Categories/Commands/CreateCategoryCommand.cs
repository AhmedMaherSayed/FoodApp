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
    public record CreateCategoryCommand(CreateCategoryViewModel createCategory) : IRequest<ResponseViewModel<CategoryViewModel>>;

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, ResponseViewModel<CategoryViewModel>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CreateCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseViewModel<CategoryViewModel>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.createCategory == null)
            {
                return ResponseViewModel<CategoryViewModel>.Failure(ErrorCode.BadRequest, "Category data is required.");
            }

            var category = AutoMapperHelper.Map<Category>(request.createCategory);

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            var categoryViewModel = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return ResponseViewModel<CategoryViewModel>.Success(categoryViewModel);
        }
    }
}
