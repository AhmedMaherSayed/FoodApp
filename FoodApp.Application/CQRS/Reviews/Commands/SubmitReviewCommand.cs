using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Review;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.CQRS.Reviews.Commands
{
    public record SubmitReviewCommand(CreateReviewDto Dto) : IRequest<ResponseViewModel<ReviewDto>>;

    public class SubmitReviewHandler
    : IRequestHandler<SubmitReviewCommand, ResponseViewModel<ReviewDto>>
    {
        private readonly IGenericRepository<Review> _repo;

        public SubmitReviewHandler(IGenericRepository<Review> repo)
            => _repo = repo;

        public async Task<ResponseViewModel<ReviewDto>> Handle(SubmitReviewCommand req, CancellationToken ct)
        {
            var exists = await _repo
                             .Get(r => r.UserId == req.Dto.UserId && r.RecipeItemId == req.Dto.RecipeItemId)
                             .AnyAsync(ct);
            if (exists)
                return ResponseViewModel<ReviewDto>
                    .Failure(ErrorCode.BadRequest, "You have already reviewed this item.");

            var review = new Review
            {
                UserId = req.Dto.UserId,
                RecipeItemId = req.Dto.RecipeItemId,
                Rate = req.Dto.Rate,
                Comment = req.Dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(review);

            var dto = review.Map<ReviewDto>();
            return ResponseViewModel<ReviewDto>.Success(dto);
        }
    }

}
