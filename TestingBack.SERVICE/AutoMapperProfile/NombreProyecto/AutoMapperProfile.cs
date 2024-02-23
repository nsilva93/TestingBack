using AutoMapper;
using TestingBack.CORE.Models.NombreProyecto;
using TestingBack.SERVICE.DTO.NombreProyecto;

namespace TestingBack.SERVICE.AutoMapperProfile.NombreProyecto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<Product, ProductsDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductsCategoryDTO>().ReverseMap();
            CreateMap<ProductSubcategory, ProductsSubcategoryDTO>().ReverseMap();
        }
    }
}
