namespace Contact.API.Infrastructure.Mappers
{
    public class InfoTypeProfile : Profile
    {
        public InfoTypeProfile()
        {
            CreateMap<InfoType, InfoTypeDto>().ReverseMap();
        }
    }
}
