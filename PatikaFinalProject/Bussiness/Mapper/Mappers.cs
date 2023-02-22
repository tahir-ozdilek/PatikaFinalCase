using AutoMapper;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Mapper
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListDTO>().ReverseMap();
            CreateMap<ShoppingList, ShoppingListCreateDTO>().ReverseMap();
            CreateMap<ShoppingListDTO, ShoppingListCreateDTO>().ReverseMap();
        }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<CategoryDTO, CategoryCreateDTO>().ReverseMap();
        }
    }

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<ProductDTO, ProductCreateDTO>().ReverseMap();
        }
    }
}
