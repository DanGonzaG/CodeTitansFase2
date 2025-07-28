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
        public async Task<ZoomMeetingResult> CrearReunionProgramadaAsync(string accessToken, DateTime fechaHoraInicio, int duracionMinutos, string tema = "Reunión programada")
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var data = new
            {
                topic = tema,
                type = 2, // Reunión programada
                start_time = fechaHoraInicio.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                duration = duracionMinutos,
                timezone = "America/Costa_Rica", 
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

            var json = JsonConvert.DeserializeObject<ZoomMeetingResult>(result);

            Console.WriteLine($"✅ Reunión creada:");
            Console.WriteLine($"🆔 ID: {json.MeetingId}");
            Console.WriteLine($"🔗 Link: {json.JoinUrl}");

            return json;

        }
    }
    public class ZoomMeetingResult
        {
            [JsonProperty("id")]
            public long MeetingId { get; set; }

            [JsonProperty("join_url")]
            public string JoinUrl { get; set; }
        }
    
}