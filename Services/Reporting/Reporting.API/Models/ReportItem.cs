namespace Reporting.API.Models
{
    public class ReportItem
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public virtual Report? Report { get; set; }
        public string? ReportContent { get; set; }
    }
}
