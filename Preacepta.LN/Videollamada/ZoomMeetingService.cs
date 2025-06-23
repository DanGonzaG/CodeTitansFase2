using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Videollamada
{
    public class ZoomMeetingService
    {
        public async Task<string> CrearReunionProgramadaAsync(string accessToken, DateTime fechaHoraInicio, int duracionMinutos, string tema = "Reunión programada")
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var data = new
            {
                topic = tema,
                type = 2, // Reunión programada
                start_time = fechaHoraInicio.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = duracionMinutos,
                timezone = "America/Cota Rica", 
                settings = new
                {
                    join_before_host = false,
                    participant_video = true,
                    host_video = true,
                    auto_recording = "cloud"
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.zoom.us/v2/users/me/meetings", content);
            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al crear reunión: {response.StatusCode} - {result}");

            dynamic json = JsonConvert.DeserializeObject(result);

            Console.WriteLine($"✅ Reunión creada:");
            Console.WriteLine($"🆔 ID: {json.id}");
            Console.WriteLine($"📅 Inicio: {json.start_time}");
            Console.WriteLine($"🔗 Link: {json.join_url}");

            return json.join_url;
        }
    }
}