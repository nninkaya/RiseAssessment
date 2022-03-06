using Contracts;
using Reporting.API.Infrastructure.Dtos;

namespace Reporting.API.Services
{
    public interface INewReportService
    {
        Task<ServiceItemResult<ReportRequestDto>> ReportCreate();
        Task ReportUpdate(ReportResponseDto response);
    }

    public class NewReportService : INewReportService
    {
        private readonly IReportingRepository _reportingRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public NewReportService(IReportingRepository reportingRepository, IPublishEndpoint publishEndpoint)
        {
            _reportingRepository = reportingRepository;
            _publishEndpoint = publishEndpoint; 
        }
        public async Task<ServiceItemResult<ReportRequestDto>> ReportCreate()
        {
            var result = await _reportingRepository.RequestNewReport();

            if(result.ResultCode != 0) return result;

            var contactReport = new ContactReportMessage
            {
                ReportId = result.Item.ReportRequestId.Value
            };
            await _publishEndpoint.Publish<ContactReportMessage>(new
            {
                ReportId = contactReport.ReportId
            });
           

            return result;
        }

        public async Task ReportUpdate(ReportResponseDto response)
        {
            await _reportingRepository.InsertReportItems(response);
        }   
    }
}
