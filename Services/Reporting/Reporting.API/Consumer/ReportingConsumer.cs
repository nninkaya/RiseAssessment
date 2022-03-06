using Contracts;
using Reporting.API.Infrastructure.Dtos;
using Reporting.API.Services;
using ReportDto = Reporting.API.Infrastructure.Dtos.ReportDto;

namespace Reporting.API.Consumer
{
    public class ReportingConsumer : IConsumer<ReportResponseMessage>
    {
        private readonly ILogger<ReportingConsumer> _logger;
        private readonly INewReportService _newReportService;

        public ReportingConsumer(ILogger<ReportingConsumer> logger, INewReportService newReportService)
        {
            _logger = logger;
            _newReportService = newReportService;
        }

        public async Task Consume(ConsumeContext<ReportResponseMessage> context)
        {
            var list = new List<ReportDto>();

            if(context.Message == null)
            {
                return;
            }

            foreach (var item in context.Message.Report)
            {
                list.Add(new ReportDto
                {
                    Location = item.Location,
                    NumberOfContacts = item.NumberOfContacts,
                    NumberOfPhones = item.NumberOfPhones
                });
            }

            var response = new ReportResponseDto();
            response.ReportId = context.Message.ReportId;
            response.Report = list;

            await _newReportService.ReportUpdate(response);
        }
    }
}
