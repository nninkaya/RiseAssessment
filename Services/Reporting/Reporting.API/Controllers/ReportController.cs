using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reporting.API.Infrastructure.Results;
using Reporting.API.Services;

namespace Reporting.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportingRepository _repository;
        private readonly INewReportService _reportService;

        public ReportController(ILogger<ReportController> logger, IReportingRepository repository, INewReportService reportService)
        {
            _logger = logger;
            _repository = repository;
            _reportService = reportService;
        }

        [HttpGet("RequestNewReport")]
        public async Task<IActionResult> RequestNewReport()
        {
            var result = await _reportService.ReportCreate();

            if (result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }

        [HttpGet("GetReportResult/{id:int}")]
        public async Task<IActionResult> GetReportResult(int id)
        {
            var result = await _repository.GetReportResult(id);

            if(result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }

        [HttpPut("UpdateReportResult")]
        private async Task<IActionResult> UpdateReportResult(ReportRequestDto dto)
        {
            var result = await _repository.UpdateReportResult(dto);

            return Ok(result);
        }
    }
}
