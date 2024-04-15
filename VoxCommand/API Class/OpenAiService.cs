using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VoxCommand.Speech_Class;

public class OpenAiService
{
    private HttpClient _httpClient;
    private string _apiKey;
    private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";

    public OpenAiService()
    {
        _apiKey = new APIKey().getAPIKey_OpenAI();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> SummarizeNewsAsync(string newsContent)
    {
        Console.WriteLine("Summarizing news...");
        return await SummarizeTextAsync(newsContent, "Could you take this text and format it into a text that I will read as a British man acting as a butler");
    }

    public async Task<string> SummarizeWeatherAsync(string weatherContent)
    {
        Console.WriteLine("Summarizing weather...");
        return await SummarizeTextAsync(weatherContent, "Could you take this weather update provided and format it into a text that will read as British man acting as butler, Doesn't need be long, The time is 12:00am.");//Broken Eng to save Token........
    }

    private async Task<string> SummarizeTextAsync(string content, string instruction)
    {
        Console.WriteLine("Starting text summarization...");
        int inputTokens = CountTokens(instruction + " " + content);
        Console.WriteLine($"Estimated input tokens: {inputTokens}");
        var initialMemory = GC.GetTotalMemory(true);
        Console.WriteLine($"Initial memory usage: {initialMemory} bytes");

        var data = new
        {
            model = "gpt-3.5-turbo-16k-0613",
            messages = new[]
            {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = instruction },
                    new { role = "user", content = content }
                }
        };

        var contentJson = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", contentJson);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);
            var messageContent = (string)responseObject.choices[0].message.content;
            Console.WriteLine("Response received successfully from OpenAI.");
            int outputTokens = CountTokens(messageContent);
            Console.WriteLine($"Estimated output tokens: {outputTokens}");
            var finalMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"Final memory usage: {finalMemory} bytes");
            Console.WriteLine($"Memory used: {finalMemory - initialMemory} bytes");

            Console.WriteLine($"Output: {messageContent.Trim()}");
           await new SpeechRecognition().SynthesizeAudioAsync(messageContent.Trim());

            return messageContent.Trim();
        }
        else
        {
            var errorResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Failed to call OpenAI: {response.StatusCode} - {errorResponse}");
            return "Failed to summarize.";
        }
    }

    private int CountTokens(string text)
    {
        // Simple placeholder for token counting
        return text.Length / 4; // Approximation of token count based on average character count
    }
}


public class OpenAiResponse
{
    public List<OpenAiChoice> choices { get; set; }
}

public class OpenAiChoice
{
    public OpenAiMessage message { get; set; }
}

public class OpenAiMessage
{
    public string content { get; set; }
}

