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

            CreateMap<ItemsEntity, ItemDto>()
                .ForMember(s => s.Remark, f => f.MapFrom(src => src.Description))
                .ForMember(s => s.IsEnabled, f => f.MapFrom(src => src.EnabledMark));
            CreateMap<ItemDto, ItemsEntity>()
              .ForMember(s => s.Description, f => f.MapFrom(src => src.Remark))
              .ForMember(s => s.EnabledMark, f => f.MapFrom(src => src.IsEnabled));

        }
    }
}