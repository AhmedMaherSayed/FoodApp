using AutoMapper;
using FoodApp.Domain.Data.Entities;
using FoodApp.Presentation.ViewModel.Recipe;
using FoodApp.Shared.DTOs.Item;
using FoodApp.Shared.DTOs.Menu;
using FoodApp.Shared.DTOs.Recipe;
using FoodApp.Shared.DTOs.Review;
using FoodApp.Shared.ViewModel.Item;
using FoodApp.Shared.ViewModel.Menu;
using FoodApp.Shared.ViewModel.Review;

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


            // ViewModels
            CreateMap<ItemDto, ItemViewModel>().ReverseMap();    

            CreateMap<RecipeDto, RecipeViewModel>().ReverseMap();        
            CreateMap<RecipeDto, RecipeListViewModel>().ReverseMap();  

            CreateMap<MenuDto, MenuViewModel>().ReverseMap();            
            CreateMap<MenuDto, MenuDetailViewModel>().ReverseMap();      

            CreateMap<MenuItemDto, ItemForMenuViewModel>().ReverseMap();    

            CreateMap<ReviewDto, ReviewViewModel>().ReverseMap();        






        }


    }
}
