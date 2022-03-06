namespace Reporting.API.Infrastructure.Dtos
{
    public class ReportResponseDto
    {
        public int ReportId { get; set; }

        public List<ReportDto> Report { get; set; }

    }
}
