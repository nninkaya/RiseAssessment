namespace Contact.API.Infrastructure.Repositories
{
    public interface IContactRepository
    {

    }


    public class ContactRepository : IContactRepository
    {
        private readonly ILogger<ContactRepository> _logger;
        private readonly ContactDbContext _context;

        public ContactRepository(ILogger<ContactRepository> logger, IServiceProvider service)
        {
            _logger = logger;
            _context = service.GetService<ContactDbContext>();
        }

        
    }
}
