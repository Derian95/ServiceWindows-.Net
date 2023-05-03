using Newtonsoft.Json;
using System.Text;
using WorkerServiceApi.Controllers;
using WorkerServiceApi.Models;

namespace WorkerServiceApi.Service
{
    public class EnvioDeMensaje
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public EnvioDeMensaje(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public void SendPostMessage()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<double> usoDisco = new List<double>();
            List<Disco> discosCantidad = new List<Disco>();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    double capacidadTotalGB = d.TotalSize / 1073741824;
                    double capacidadLibreGB = d.AvailableFreeSpace / 1073741824;
                    var fechaActual = DateTime.Now;

                    Disco nuevo = new Disco()
                    {

                        nombreDisco = d.Name,
                        capacidadTotal = Convert.ToString(capacidadTotalGB) + "GB",
                        capacidadLibre = Convert.ToString(capacidadLibreGB) + "GB",
                        capacidadEnUso = Convert.ToString(43) + "GB",
                        sistemaDisco = Convert.ToString(d.DriveFormat),
                        tipoDisco = Convert.ToString(d.DriveType),
                        seudonimoDisco = d.VolumeLabel,
                        codSala = 36,
                        fechaRegistro = DateTime.Now.ToString()

                    };

                    usoDisco.Add(capacidadLibreGB);
                    discosCantidad.Add(nuevo);

                }
            }
            _logger.LogInformation("Se esta enviado datos del disco al ias");
                string json = JsonConvert.SerializeObject(discosCantidad);

                var postData = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("http://192.168.1.200/ias/Disco/AgregarDiscosSala", postData).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("El mensaje POST se ha enviado con éxito.");
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error al enviar el mensaje POST.");
            }
        }
    }
}
