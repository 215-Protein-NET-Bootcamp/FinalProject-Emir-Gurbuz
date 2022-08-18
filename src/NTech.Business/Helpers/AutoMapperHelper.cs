using AutoMapper;
using Core.Dto.Concrete;
using Core.Entity.Concrete;
using NTech.Dto.Concrete;
using NTech.Dto.Concrete.EmailQueue;
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

            CreateMap<Offer, OfferWriteDto>().ReverseMap();
            CreateMap<Offer, OfferReadDto>().ReverseMap();

            CreateMap<EmailQueue, EmailQueueWriteDto>().ReverseMap();
            CreateMap<EmailQueue, EmailQueueReadDto>().ReverseMap();

            CreateMap<User, UserReadDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
