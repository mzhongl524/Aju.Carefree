using Aju.Carefree.Dto;
using Aju.Carefree.Entity;
using AutoMapper;

namespace Aju.Carefree.AutoMapperConfig
{
    public class CarefreeProfile : Profile, IProfile
    {
        public CarefreeProfile()
        {
            CreateMap<Areas, AreasDto>();
            CreateMap<AreasDto, Areas>();
        }
    }
}
