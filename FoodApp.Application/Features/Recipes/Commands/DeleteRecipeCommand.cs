using FoodApp.Application.DTOs;
using FoodApp.Application.Repositories;
using FoodApp.Domain.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Application.Features.Recipes.Commands
{
    public record DeleteRecipeCommand(int Id) : IRequest<RecipeDto?>;

    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, RecipeDto?>
    {
        private readonly IGenericRepository<Recipe> _repo;

        public DeleteRecipeHandler(IGenericRepository<Recipe> repo) => _repo = repo;

        public async Task<RecipeDto?> Handle(DeleteRecipeCommand req,CancellationToken ct)
        {
            var entity = await _repo.GetByIdWithTrackingAsync(req.Id);
            if (entity is null)
            {
                return null;
            }

            var dto = entity.Map<RecipeDto>();

            await _repo.DeleteAsync(req.Id);

            return dto;
        }
    }
}
