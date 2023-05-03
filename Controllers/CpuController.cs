using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Management;
using System.Net.Sockets;
using System.Net;
//using Windows.Devices.Geolocation;
//using System.Device.Location;
//using CrossPlatformLibrary.Geolocation;
using Xamarin.Essentials;

//using System.Device.Sensors;
namespace WorkerServiceApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CpuController : ControllerBase
    {
        private readonly ILogger<CpuController> _logger;

        public CpuController(ILogger<CpuController> logger)
        {
            _logger = logger;
        }
        //[HttpPost]
        //public async Task<dynamic> CpuPorcentaje()
        //{
        //    dynamic estadoCpu = "";
        //    //    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT LoadPercentage FROM Win32_Processor");
        //    //    ManagementObjectCollection results = searcher.Get();
        //    //    foreach (ManagementObject obj in results)
        //    //    {
        //    //        int usage = Convert.ToInt32(obj["LoadPercentage"]);
        //    //        estadoCpu = usage;
        //    //    }
        //    _logger.LogWarning("Warning ejemplo" + DateTime.Now);
        //    _logger.LogTrace("Trace ejemplo" + DateTime.Now);
        //    _logger.LogInformation("Information ejemplo" + DateTime.Now);

        //    return estadoCpu;
        //}

        [HttpPost]
        public async Task<dynamic> ObtenerUbicacion()
        {
            _logger.LogWarning("Warning ejemplo" + DateTime.Now);
            _logger.LogTrace("Trace ejemplo" + DateTime.Now);
            _logger.LogInformation("Information ejemplo" + DateTime.Now);
            //Location location = await Geolocation.GetLastKnownLocationAsync();
            //var ra = location.Longitude;
            //var ra2 = location.Latitude;
            //var geolocator = new Geolocator();

            //var position = await geolocator.GetGeopositionAsync();
            //var rra = position.Coordinate;

            //Geolocator locator = new Geolocator();
            //var accessStatus = await Geolocator.RequestAccessAsync();
            //if (accessStatus == GeolocationAccessStatus.Allowed)
            //{
            //    return await locator.GetGeopositionAsync();
            //}
            //else
            //{
            //    throw new Exception("Location access denied.");
            //}
            string directoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string ra = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            return new {directoryPath,ra};
        }


    }
}
