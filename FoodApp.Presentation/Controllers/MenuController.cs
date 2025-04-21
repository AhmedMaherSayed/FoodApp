using FoodApp.Application;
using FoodApp.Application.CQRS.Menus.Commands;
using FoodApp.Application.CQRS.Menus.Queries;
using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;
using FoodApp.Shared.DTOs.Menu;
using FoodApp.Shared.ViewModel.Menu;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetMenusQuery(), cancellationToken);
            if (!response.IsSucsess)
                return BadRequest(response);

            var viewModels = response.Data.Select(menu => menu.Map<MenuViewModel>()).ToList();
            return Ok(ResponseViewModel<List<MenuViewModel>>.Success(viewModels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ViewMenuByIdQuery(id), cancellationToken);
            if (!response.IsSucsess)
                return NotFound(response);

            var viewModel = response.Data.Map<MenuDetailViewModel>();
            return Ok(ResponseViewModel<MenuDetailViewModel>.Success(viewModel));
        }


        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDto dto, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateMenuCommand(dto), cancellationToken);
            if (!response.IsSucsess)
                return BadRequest(response);

            var viewModel = response.Data.Map<MenuViewModel>();
            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] EditMenuDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch between URL and payload.");

            var response = await _mediator.Send(new UpdateMenuCommand(dto), cancellationToken);
            if (!response.IsSucsess)
                return NotFound(response);

            var viewModel = response.Data.Map<MenuViewModel>();
            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteMenuCommand(id), cancellationToken);
            if (!response.IsSucsess)
                return NotFound(response);

            var viewModel = response.Data.Map<MenuViewModel>();
            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        // POST: api/menu/{menuId}/items
        [HttpPost("{menuId}/items")]
        public async Task<IActionResult> AddItemToMenu(int menuId, [FromBody] AddItemToMenuDto dto, CancellationToken cancellationToken)
        {
            if (menuId != dto.MenuId)
                return BadRequest(ResponseViewModel<string>.Failure(ErrorCode.BadRequest,"Menu ID in URL and payload do not match."));

            var command = new AddItemToMenuCommand(dto);
            var response = await _mediator.Send(command, cancellationToken);

            if (!response.IsSucsess)
                return BadRequest(ResponseViewModel<string>.Failure(ErrorCode.BadRequest,response.Message));

            var viewModel = response.Data.Map<ItemForMenuViewModel>();
            return Ok(ResponseViewModel<ItemForMenuViewModel>.Success(viewModel));
        }

        [HttpDelete("{menuId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromMenu(int menuId, int itemId, CancellationToken cancellationToken)
        {
            var command = new RemoveItemFromMenuCommand(menuId, itemId);
            var response = await _mediator.Send(command, cancellationToken);

            if (!response.IsSucsess)
                return NotFound(ResponseViewModel<string>.Failure(ErrorCode.BadRequest, response.Message));

            var viewModel = response.Data.Map<ItemForMenuViewModel>();
            return Ok(ResponseViewModel<ItemForMenuViewModel>.Success(viewModel));
        }



    }
}
