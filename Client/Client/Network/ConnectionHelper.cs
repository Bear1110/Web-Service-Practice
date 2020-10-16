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
                return await EnsureSuccessAndConvertToString(response);
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
                HttpContent contentPost = new StringContent(postData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, contentPost);
                return await EnsureSuccessAndConvertToString(response);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught! Message :{0} ", e.Message);
                return errorMessage;
            }
        }

        private static async Task<string> EnsureSuccessAndConvertToString(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
