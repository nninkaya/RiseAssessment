namespace Contact.API.Infrastructure.Dtos
{
    public class ContactDto
    {
        public ContactDto()
        {
            ContactInfos = new List<ContactInfoDto>();
        }
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set;}
    }
}
