namespace Reporting.API.Infrastructure.Dtos
{
    public class ReportRequestDto
    {
        public ReportRequestDto()
        {
            ReportItems = new List<ReportDto>();
        }

        public int? ReportRequestId { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.InProgress;
        public DateTime? RequestTime { get; set; }
        public string? ReportContent { get; set; }
        public DateTime? ReportTime { get; set; }
        public List<ReportDto> ReportItems { get; set; }

    }
}
