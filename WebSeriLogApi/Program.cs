using Serilog;
using WebSeriLogApi.Contacts;
using WebSeriLogApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// add DI of service
builder.Services.AddSingleton<IMaskService, MaskService>();
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set minimum log level
    .WriteTo.Console()    // Log to console
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Optional: Log to file
    .Enrich.FromLogContext() // Include contextual information
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog as the logging provider

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware and endpoint configuration
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
