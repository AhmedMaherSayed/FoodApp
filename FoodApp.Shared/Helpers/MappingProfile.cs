using AutoMapper;
using FoodApp.Application.DTOs;
using FoodApp.Domain.Data.Entities;
using FoodApp.Shared.ViewModel.Category;
using FoodApp.Shared.ViewModel.User;

namespace FoodApp.Presentation.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();

            CreateMap<Recipe, RecipeDto>();

            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
        }


    }
}
