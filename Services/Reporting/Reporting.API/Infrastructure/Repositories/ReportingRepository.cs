namespace Reporting.API.Infrastructure.Repositories
{
    #region INTERFACE
    public interface IReportingRepository
    {

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

        //public async Task<ServiceItemResult<>> RequestNewReport()
        //{
           
        //}

    }
}
