using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Categories.Commands
{
    public record DeleteCategoryCommand(int categoryId) : IRequest<ResponseViewModel<bool>>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, ResponseViewModel<bool>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public DeleteCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseViewModel<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository
                .Get(x => x.Id == request.categoryId)
                .AnyAsync();

            if (!categoryExists)
            {
                return ResponseViewModel<bool>.Failure(ErrorCode.NotFound, "Category not found");
            }

            var category = await _categoryRepository
                .Get(x => x.Id == request.categoryId)
                .FirstOrDefaultAsync();

            await _categoryRepository.SoftDeleteAsync(category);

            return ResponseViewModel<bool>.Success(true);
        }
    }
}
