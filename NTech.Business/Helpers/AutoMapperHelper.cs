using AutoMapper;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<Product, ProductWriteDto>().ReverseMap();
            CreateMap<Product, ProductReadDto>().ReverseMap();

            CreateMap<Category, CategoryWriteDto>().ReverseMap();
            CreateMap<Category, CategoryReadDto>().ReverseMap();

            CreateMap<Brand, BrandWriteDto>().ReverseMap();
            CreateMap<Brand, BrandReadDto>().ReverseMap();

            CreateMap<Image, ImageWriteDto>().ReverseMap();
            CreateMap<Image, ImageReadDto>().ReverseMap();

            CreateMap<Color, ColorWriteDto>().ReverseMap();
            CreateMap<Color, ColorReadDto>().ReverseMap();

            CreateMap<UsingStatus, UsingStatusWriteDto>().ReverseMap();
            CreateMap<UsingStatus, UsingStatusReadDto>().ReverseMap();
        }
    }
}
