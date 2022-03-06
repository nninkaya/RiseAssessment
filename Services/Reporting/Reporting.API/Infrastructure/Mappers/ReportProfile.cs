namespace Reporting.API.Infrastructure.Mappers
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportRequestDto>().ReverseMap();
        }
    }
}
