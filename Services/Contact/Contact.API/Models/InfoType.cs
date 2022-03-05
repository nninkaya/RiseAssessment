namespace Contact.API.Models
{
    public class InfoType
    {
        public InfoType()
        {
            ContactInfos = new List<ContactInfo>();
        }
        public int Id { get; set; }
        public string? Culture { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
