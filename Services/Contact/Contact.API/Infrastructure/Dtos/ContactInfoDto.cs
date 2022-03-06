namespace Contact.API.Infrastructure.Dtos
{
    public class ContactInfoDto
    {
        public int? Id { get; set; }
        public int? ContactId { get; set; }
        public int InfoTypeId { get; set; }
        //public string? InfoTypeName { get; set; }
        public string? Value { get; set; }
    }
}
