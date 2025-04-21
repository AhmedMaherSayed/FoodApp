using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.ViewModel.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Categories.Queries
{
    public record GetCategoriesByUserIdQuery(int UserId) : IRequest<ResponseViewModel<List<CategoryViewModel>>>;

    public class GetCategoriesByUserIdHandler : IRequestHandler<GetCategoriesByUserIdQuery, ResponseViewModel<List<CategoryViewModel>>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public GetCategoriesByUserIdHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseViewModel<List<CategoryViewModel>>> Handle(GetCategoriesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository
                .Get(x => x.UserId == request.UserId)
                .Select(x => x.Map<CategoryViewModel>())
                .ToListAsync();

            return ResponseViewModel<List<CategoryViewModel>>.Success(categories);
        }
    }
}
