using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Review;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Reviews.Queries
{
    public record GetReviewsByItemQuery(GetReviewsByItemDto Dto): IRequest<ResponseViewModel<List<ReviewDto>>>;

    public class GetReviewsByItemHandler
    : IRequestHandler<GetReviewsByItemQuery, ResponseViewModel<List<ReviewDto>>>
    {
        private readonly IGenericRepository<Review> _repo;

        public GetReviewsByItemHandler(IGenericRepository<Review> repo)
            => _repo = repo;

        public async Task<ResponseViewModel<List<ReviewDto>>> Handle(GetReviewsByItemQuery req,CancellationToken ct)
        {
            var query = _repo
                .Get(r => r.RecipeItemId == req.Dto.RecipeItemId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt);

            var list = await query
                .Skip((req.Dto.PageIndex - 1) * req.Dto.PageSize)
                .Take(req.Dto.PageSize)
                .ToListAsync(ct);

            var dtos = list.Select(r => r.Map<ReviewDto>()).ToList();
            return ResponseViewModel<List<ReviewDto>>.Success(dtos);
        }
    }

}
