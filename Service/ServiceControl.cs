using WorkerServiceApi.Models;

namespace WorkerServiceApi.Service
{
    public class ServiceControl
    {
        private readonly IConfiguration config;
        
        public  void useApp(WebApplication app)
        {
            var setting = config.GetSection("Settings").Get<Settings>();
            ILogger logger = app.Logger;
            IHostApplicationLifetime lifetime = app.Lifetime;
            var url = setting.Url;
            var nombre = setting.Username;
            var pass = setting.Password;
            lifetime.ApplicationStarted.Register(() =>
            logger.LogInformation("El servicio de la Api se inició" + url + nombre + pass));


            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("El servicio de la API se está deteniendo.");
            });


            lifetime.ApplicationStopped.Register(() =>
            {
                logger.LogInformation("El servicio de la API se detuvo");
            });
        }
    }
}
