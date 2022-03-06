namespace Contact.API.Infrastructure.Mappers
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Models.Contact, ContactDto>().ReverseMap();
        }
    }
}
