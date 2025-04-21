using AutoMapper;
using FoodApp.Domain.Data.Entities;
using FoodApp.Shared.DTOs.Item;
using FoodApp.Shared.DTOs.Menu;
using FoodApp.Shared.DTOs.Recipe;
using FoodApp.Shared.DTOs.Review;

namespace FoodApp.Presentation.Helpers
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
           
            CreateMap<CreateItemDto, Item>().ReverseMap();     
            CreateMap<UpdateItemDto, Item>().ReverseMap();     
            CreateMap<Item, ItemDto>().ReverseMap();           

            CreateMap<CreateRecipeDto, Recipe>().ReverseMap();  
            CreateMap<UpdateRecipeDto, Recipe>().ReverseMap();  
            CreateMap<Recipe, RecipeDto>().ReverseMap();       

            CreateMap<CreateMenuDto, Menu>().ReverseMap();      
            CreateMap<EditMenuDto, Menu>()                     
                .ForMember(dest => dest.Items,
                           opt => opt.MapFrom(src =>
                               src.ItemIds.Select(id => new Item { Id = id }))).ReverseMap()
                .ForMember(dest => dest.ItemIds,
                           opt => opt.MapFrom(src =>
                               src.Items.Select(i => i.Id)));
            CreateMap<Menu, MenuDto>().ReverseMap();            
            CreateMap<AddItemToMenuDto, MenuItemDto>().ReverseMap();

            CreateMap<CreateReviewDto, Review>().ReverseMap(); 
            CreateMap<Review, ReviewDto>().ReverseMap();


        }


    }
}
