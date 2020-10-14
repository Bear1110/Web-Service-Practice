using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Client.Network
{
    class ConnectionHelper
    {
        public static readonly string errorMessage = "error";
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> SendGetRequest(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught! Message :{0} ", e.Message);
                return errorMessage;
            }
        }

        public static async Task<string> SendPostRequest(string apiUrl, string postData)
        {
            try
            {
                // 將 data 轉為 json
                string json = JsonSerializer.Serialize(postData);
                HttpContent contentPost = new StringContent(postData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, contentPost);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught! Message :{0} ", e.Message);
                return errorMessage;
            }
        }
    }
}
