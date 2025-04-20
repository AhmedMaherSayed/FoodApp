using AutoMapper;
using FoodApp.Application.DTOs;
using FoodApp.Domain.Data.Entities;

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
