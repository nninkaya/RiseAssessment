namespace Reporting.API.Models
{
    public class Report
    {
        public Report()
        {
            ReportItems = new List<ReportItem>();
        }

        [Key]
        public int Id { get; set; }
        public ReportStatus Status { get; set; }
        public DateTime? RequestTime { get; set; }
        public string? ReportContent { get; set; }
        public DateTime? ReportTime { get; set; }
        public virtual ICollection<ReportItem> ReportItems { get; set; }
    }
}
