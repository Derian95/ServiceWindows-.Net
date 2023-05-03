using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Logging;
using WorkerServiceApi.Models;
using WorkerServiceApi.Service;
using Microsoft.Extensions.Logging.EventLog;

var options = new WebApplicationOptions
{
    Args = args,
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
};

var builder = WebApplication.CreateBuilder(options);

// Add services to the container.
builder.Host.UseWindowsService();



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<EventLogSettings>(config =>
{
    config.LogName = string.Empty;
    config.SourceName = "ApiServicioLog";


});




builder.Host.ConfigureLogging(logging =>
{
    var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
    //if (!Directory.Exists(logFolder))
    //{
    //    Directory.CreateDirectory(logFolder);
    //}
    //var logFile = Path.Combine(logFolder, $"gladcon-{DateTime.Now:yyyy-MM-dd}.log");
    //logging.AddFile(logFile);
    //logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddFile( Path.Combine(logFolder,"gladcon-{Date}.log") );
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/***********************************************************************************/
ILogger logger = app.Logger;
IHostApplicationLifetime lifetime = app.Lifetime;
/***********************************************************************************/
var setting = builder.Configuration.GetSection("Settings").Get<Settings>();
var url = setting.Url;
var nombre = setting.Username;
var pass = setting.Password;
/***********************************************************************************/

var httpClient = new HttpClient();
var messageSender = new EnvioDeMensaje(httpClient,logger);


lifetime.ApplicationStarted.Register(() =>
    logger.LogInformation("El servicio de la Api se inició"+ url + nombre + pass)
);

lifetime.ApplicationStopping.Register(() =>
{
    //messageSender.SendPostMessage();
    logger.LogInformation("El servicio de la API se está deteniendo.");


});

lifetime.ApplicationStopped.Register(() =>
{
    messageSender.SendPostMessage();
    logger.LogInformation("El servicio de la API se detuvo");
});


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
//var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

app.UseAuthorization();

app.MapControllers();

app.Run();


