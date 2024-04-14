using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VoxCommand.Speech_Class;


namespace VoxCommand.API_Class
{
    internal class NewsService
    {
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.thenewsapi.com/v1/news/top";

        public NewsService()
        {
            _apiKey = new APIKey().getAPIKey_NewsAPI();
        }

        public async Task<string> FetchTopHeadlinesAsync()
        {
            string url = $"{BaseUrl}?api_token={_apiKey}&search=world&language=en&limit=3";
            Console.WriteLine($"Constructed URL: {url}");

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Console.WriteLine("Sending HTTP GET request...");
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    Console.WriteLine($"HTTP Status Code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Response received successfully from News API.");
                        Console.WriteLine($"Response: {jsonResponse}");
                        return jsonResponse;
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error in API response: {errorResponse}");
                        return $"Error: {response.StatusCode} - {errorResponse}";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred during API call: {ex.Message}");
                    return $"Exception: {ex.Message}";
                }
            }
        }
    }
}
