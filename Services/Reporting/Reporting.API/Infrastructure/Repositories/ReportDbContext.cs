using Microsoft.EntityFrameworkCore;
using Reporting.API.Models;

namespace Reporting.API.Infrastructure.Repositories
{
    public class ReportDbContext : DbContext
    {

        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        { }

        public DbSet<Report> Report { get; set; }
        public DbSet<ReportItem> ReportItem { get; set; }

    }
}
