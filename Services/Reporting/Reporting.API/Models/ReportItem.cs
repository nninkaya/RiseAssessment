namespace Reporting.API.Models
{
    public class ReportItem
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Location { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfPhones { get; set; }
    }
}
