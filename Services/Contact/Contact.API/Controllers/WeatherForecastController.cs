
namespace Contact.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ContactDbContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceProvider service)
        {
            _logger = logger;
            _context = service.GetService<ContactDbContext>();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var contact = new Contact.API.Models.Contact
            {
                FirstName = "Necdet",
                MiddleName = "Nuri",
                LastName = "Inkaya",
                Company = "Kimak"
            };
            var contactList = _context.Contacts.Add(contact);
            _context.SaveChanges();

            var list = _context.Contacts.ToList();

            _logger.LogInformation("WeatherForecast");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}