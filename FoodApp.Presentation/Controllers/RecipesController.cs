using AutoMapper;
using FoodApp.Application.CQRS.Recipes.Commands;
using FoodApp.Application.CQRS.Recipes.Queries;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel.Recipe;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Recipe;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {


        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RecipesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllRecipesQuery());
            if (!response.IsSucsess)
                return BadRequest(response);

            var vms = response.Data
                .Select(dto => _mapper.Map<RecipeListViewModel>(dto))
                .ToList();

            return Ok(ResponseViewModel<List<RecipeListViewModel>>.Success(vms));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetRecipeByIdQuery(id));
            if (!response.IsSucsess)
                return NotFound(response);

            var vm = _mapper.Map<RecipeViewModel>(response.Data);
            return Ok(ResponseViewModel<RecipeViewModel>.Success(vm));
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await _mediator.Send(new DeleteRecipeCommand(id));
        //    if (!response.IsSucsess)
        //        return NotFound(response);

        //    var vm = _mapper.Map<RecipeViewModel>(response.Data);
        //    return Ok(ResponseViewModel<RecipeViewModel>.Success(vm));
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecipeDto dto)
        {
            var response = await _mediator.Send(new CreateRecipeCommand(dto));
            if (!response.IsSucsess)
                return BadRequest(response);

            var vm = _mapper.Map<RecipeViewModel>(response.Data);
            return Ok(ResponseViewModel<RecipeViewModel>.Success(vm));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRecipeDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ResponseViewModel<RecipeViewModel>.Failure(ErrorCode.BadRequest, "ID mismatch"));

            var response = await _mediator.Send(new UpdateRecipeCommand(dto));
            if (!response.IsSucsess)
                return BadRequest(response);

            var vm = _mapper.Map<RecipeViewModel>(response.Data);
            return Ok(ResponseViewModel<RecipeViewModel>.Success(vm));
        }


    }
}
