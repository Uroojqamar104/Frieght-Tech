using FreightTech.App.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreightTech.App.Helpers
{
    public static class WebHelper
    {

        public static async Task<ApiToken> GetTokenAsync(string username, string password, string baseUrl)
        {
            string url = string.Format("{0}login", baseUrl);
            var dict = new Dictionary<string, string>();
            dict.Add("grant_type", "password");
            dict.Add("username", username);
            dict.Add("password", password);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) })
            {
                var response = await client.SendAsync(request);
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiToken>(data);
            }            
        }
    }
}