namespace Contact.API.Infrastructure.Mappers
{
    public class ContactInfoProfile : Profile
    {
        public ContactInfoProfile()
        {
            CreateMap<ContactInfo, ContactInfoDto>()
                .ForMember(dest => dest.InfoTypeId, opt => opt.MapFrom(src => src.InfoTypeId))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();
        }
    }
}
