namespace Reporting.API.Models
{
    public class Report
    {
        public Report()
        {
            ReportItems = new List<ReportItem>();
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public DateTime? RequestTime { get; set; }
        public virtual ICollection<ReportItem> ReportItems { get; set; }
    }
}
