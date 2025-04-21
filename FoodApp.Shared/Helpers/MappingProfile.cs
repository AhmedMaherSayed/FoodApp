using AutoMapper;
using FoodApp.Domain.Data.Entities;
using FoodApp.Shared.DTOs.Recipe;

namespace FoodApp.Presentation.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();

            CreateMap<Recipe, RecipeDto>();
        }


    }
}
