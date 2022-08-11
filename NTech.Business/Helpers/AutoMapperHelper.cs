using AutoMapper;
using NTech.Dto.Concrete.Product;
using NTech.Entity.Concrete;

namespace NTech.Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<Product, ProductWriteDto>().ReverseMap();
            CreateMap<Product, ProductReadDto>().ReverseMap();
        }
    }
}
