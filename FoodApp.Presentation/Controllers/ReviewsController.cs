using FoodApp.Application;
using FoodApp.Application.Features.Reviews.Commands;
using FoodApp.Application.Features.Reviews.Queries;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Review;
using FoodApp.Shared.ViewModel.Review;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {


        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(
            [FromBody] CreateReviewDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new SubmitReviewCommand(dto),
                cancellationToken);

            if (!response.IsSucsess)
                return BadRequest(response);

            var vm = response.Data.Map<ReviewViewModel>();
            return Ok(ResponseViewModel<ReviewViewModel>.Success(vm));
        }

        [HttpGet("by-item")]
        public async Task<IActionResult> GetByItem(
            [FromQuery] GetReviewsByItemDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetReviewsByItemQuery(dto),
                cancellationToken);

            if (!response.IsSucsess)
                return BadRequest(response);

            var vms = response.Data
                              .Select(r => r.Map<ReviewViewModel>())
                              .ToList();

            return Ok(ResponseViewModel<List<ReviewViewModel>>.Success(vms));
        }


    }
}
