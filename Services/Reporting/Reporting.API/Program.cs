using Reporting.API.Consumer;
using Reporting.API.Services;

Log.Logger = new LoggerConfiguration() //BITS.128402/A7
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File($"{AppContext.BaseDirectory}\\Logs\\log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "|>{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")//BITS.126506/D
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<INewReportService, NewReportService>();
builder.Services.AddScoped<IReportingRepository, ReportingRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

var connectionString = builder.Configuration["ConnectionStrings:ReportDbConnectionString"];
builder.Services.AddDbContext<ReportDbContext>(p => p.UseNpgsql(connectionString));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportingConsumer>();

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
