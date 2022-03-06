namespace Reporting.API.Infrastructure.Repositories
{
    #region INTERFACE
    public interface IReportingRepository
    {
        Task<ServiceItemResult<ReportRequestDto>> RequestNewReport();
        Task<ServiceResult> UpdateReportResult(ReportRequestDto reportDto);
        Task<ServiceResult> InsertReportItems(ReportResponseDto response);
        Task<ServiceItemResult<ReportRequestDto>> GetReportResult(int id);
    }

    #endregion

    public class ReportingRepository : IReportingRepository
    {
        private readonly ILogger<ReportingRepository> _logger;
        private readonly ReportDbContext _context;
        private readonly IMapper _mapper;

        public ReportingRepository(ILogger<ReportingRepository> logger, IServiceProvider service, IMapper mapper)
        {
            _logger = logger;
            _context = service.GetService<ReportDbContext>();
            _mapper = mapper;
        }

        public async Task<ServiceItemResult<ReportRequestDto>> RequestNewReport()
        {
            var result = new ServiceItemResult<ReportRequestDto>();

            try
            {
                var report = new Report
                {
                    RequestTime = DateTime.UtcNow,
                    Status = (int)ReportStatus.InProgress
                };

                _context.Report.Add(report);

                await _context.SaveChangesAsync();

                var dto = new ReportRequestDto
                {
                    ReportRequestId = report.Id,
                    RequestTime = report.RequestTime,
                };

                result.Item = dto;
            }
            catch (Exception e)
            {
                _logger.LogError($"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = $"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}";
            }

            return result;
        }


        public async Task<ServiceResult> UpdateReportResult(ReportRequestDto reportDto)
        {
            var result = new ServiceResult();

            try
            {
                var report = await _context.Report.FirstOrDefaultAsync(r => r.Id == reportDto.ReportRequestId);

                if (report == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                report.ReportTime = DateTime.UtcNow;
                report.ReportContent = reportDto.ReportContent;
                report.Status = ReportStatus.Completed;

                _context.Entry(report).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = $"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}";
            }

            return result;
        }

        public async Task<ServiceResult> InsertReportItems(ReportResponseDto response)
        {
            var result = new ServiceResult();

            try
            {
                var report = _context.Report.Include("ReportItems").FirstOrDefault(r => r.Id == response.ReportId);

                if(report == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                foreach (var item in response.Report)
                {
                    report.ReportItems.Add(new ReportItem { ReportId = response.ReportId, Location = item.Location, NumberOfContacts = item.NumberOfContacts, NumberOfPhones = item.NumberOfPhones });
                }

                var reqTime = report.RequestTime.Value;
                report.ReportTime = DateTime.UtcNow;
                report.Status = ReportStatus.Completed;

                _context.Entry(report).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = $"RequestNewReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}";
            }

            return result;
        }

        public async Task<ServiceItemResult<ReportRequestDto>> GetReportResult(int id)
        {
            var result = new ServiceItemResult<ReportRequestDto>();

            try
            {
                var report = await _context.Report.Include("ReportItems").FirstOrDefaultAsync(r => r.Id == id);

                if (report == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                var items = new List<ReportDto>();

                foreach (var item in report.ReportItems)
                {
                    items.Add(new ReportDto { Location = item.Location, NumberOfContacts = item.NumberOfContacts, NumberOfPhones = item.NumberOfPhones });
                }

                result.Item = new ReportRequestDto
                {
                    ReportRequestId = report.Id,
                    Status = report.Status,
                    RequestTime = report.RequestTime,
                    ReportContent = report.ReportContent,
                    ReportTime = report.ReportTime,
                    ReportItems = items
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"GetReportResult Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = $"GetReportResult Error: {e.Message} - {e.StackTrace} - {e.InnerException}";
            }

            return result;
        }
    }
}
