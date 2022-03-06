using Contact.API.Consumer;
using MassTransit;

Log.Logger = new LoggerConfiguration() //BITS.128402/A7
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File($"{AppContext.BaseDirectory}\\Logs\\log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "|>{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")//BITS.126506/D
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly)
    .Where(filePath => Path.GetFileName(filePath).StartsWith("Rise"))
    .Select(Assembly.LoadFrom);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

var connectionString = builder.Configuration["ConnectionStrings:ContactDbConnectionString"];
builder.Services.AddDbContext<ContactDbContext>(p => p.UseNpgsql(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContactReportConsumer>();

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
