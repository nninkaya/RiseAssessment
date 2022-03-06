

namespace Contact.API.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactInfos = new List<ContactInfo>();
        }
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }        
        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
