using AutoMapper;
using FoodApp.Application.Features.Items.Commands;
using FoodApp.Application.Features.Items.Queries;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Item;
using FoodApp.Shared.DTOs.Recipe;
using FoodApp.Shared.ViewModel.Item;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {


        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var response = await _mediator.Send(new GetItemsByCategoryQuery(categoryId));
            if (!response.IsSucsess)
                return BadRequest(response);

            var viewModels = _mapper.Map<List<ItemViewModel>>(response.Data);
            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var response = await _mediator.Send(new GetItemsByUserQuery(userId));
            if (!response.IsSucsess)
                return BadRequest(response);

            var viewModels = _mapper.Map<List<ItemViewModel>>(response.Data);
            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpGet("by-recipe/{recipeId}")]
        public async Task<IActionResult> GetByRecipe(int recipeId)
        {
            var response = await _mediator.Send(new GetItemsByRecipeQuery(recipeId));
            if (!response.IsSucsess)
                return BadRequest(response);

            var viewModels = _mapper.Map<List<ItemViewModel>>(response.Data);
            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
        {
            var response = await _mediator.Send(new CreateItemCommand(dto));
            if (!response.IsSucsess)
                return BadRequest(response);

            var vm = _mapper.Map<ItemViewModel>(response.Data);
            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ResponseViewModel<ItemViewModel>.Failure(ErrorCode.BadRequest,"ID mismatch"));

            var response = await _mediator.Send(new UpdateItemCommand(dto));
            if (!response.IsSucsess)
                return BadRequest(response);

            var vm = _mapper.Map<ItemViewModel>(response.Data);
            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteItemCommand(id));
            if (!response.IsSucsess)
                return NotFound(response);

            var vm = _mapper.Map<ItemViewModel>(response.Data);
            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpPost("addToRecipe")]
        public async Task<IActionResult> AddToRecipe([FromBody] RecipeItemDto dto)
        {
            var response = await _mediator.Send(new AddItemToRecipeCommand(dto));
            if (!response.IsSucsess)
                return BadRequest(response);

            return Ok(ResponseViewModel<RecipeItemViewModel>.Success(
                _mapper.Map<RecipeItemViewModel>(response.Data)
            ));
        }

        [HttpDelete("removeFromRecipe")]
        public async Task<IActionResult> RemoveFromRecipe([FromQuery] int recipeId, [FromQuery] int itemId)
        {
            var response = await _mediator.Send(new RemoveItemFromRecipeCommand(recipeId, itemId));
            if (!response.IsSucsess)
                return NotFound(response);

            return Ok(ResponseViewModel<RecipeItemViewModel>.Success(
                _mapper.Map<RecipeItemViewModel>(response.Data)
            ));
        }


    }
}
