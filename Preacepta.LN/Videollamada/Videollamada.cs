
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Preacepta.LN.Videollamada
{
    public class ZoomAuthService
    {
        private readonly string clientId = "NdDyikjUSCKaC0DWohKtDQ";
        private readonly string clientSecret = "sR4ojylrXm0GFmhw9lgPoYMXPnjPiJkQ";
        private readonly string accountId = "iciYCrAGTNW1MLwOdqblDg";

        public async Task<string> ObtenerAccessTokenAsync()
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "https://zoom.us/oauth/token");

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var parameters = new Dictionary<string, string>
        {
            { "grant_type", "account_credentials" },
            { "account_id", accountId }
        };

            request.Content = new FormUrlEncodedContent(parameters);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al obtener token: {response.StatusCode} - {responseContent}");

            dynamic json = JsonConvert.DeserializeObject(responseContent);
            return json.access_token;
        }
    }
}
