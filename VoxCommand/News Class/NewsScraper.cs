using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


namespace VoxCommand.News_Class
{
    internal class NewsScraper
    {
       
        public async void ScrapeCNNNews()
        {
            try
            {
                //ChromeOptions options = new ChromeOptions();
                //options.AddArguments("headless");
                //options.AddArguments("--ignore-certificate-errors");
                //options.AddArguments("--log-level=3");
                //options.AddArguments("--disable-features=SameSiteByDefaultCookies,CookiesWithoutSameSiteMustBeSecure");
                //options.AddArguments("--disable-web-security"); // Be cautious with this
                //using (var driver = new ChromeDriver(options))
                //{
                //    driver.Navigate().GoToUrl("https://www.cnn.com");
                //    var newsItems = driver.FindElements(By.CssSelector("span.container__headline-text"));
                //    List<(string Title, string Url)> headlines = new List<(string Title, string Url)>();

                //    foreach (var item in newsItems)
                //    {
                //        if (newsItems != null)
                //        {
                //            string title = item.Text;
                //            string link = item.GetAttribute("href");
                //            string summary = await SummarizeTextAsync(title);

                //            if (summary.StartsWith("Error") || summary.StartsWith("Failed"))
                //            {
                //                Console.WriteLine("Skipping due to error in summarization.");
                //                continue;
                //            }

                //            if (!string.IsNullOrWhiteSpace(title) && title.Length > 30) // Filter out short or empty titles
                //            {
                //                headlines.Add((title, link));
                //            }

                //            if (headlines.Count == 5) // Limit to top 5 headlines
                //            {
                //                break;
                //            }




                          
                //        }

                //    }
                //    driver.Quit();

                //    foreach (var headline in headlines)
                //    {
                //        Console.WriteLine($"Title: {headline.Title}");
                //        Console.WriteLine($"URL: {headline.Url}");
                //    }
                //}
            }
            catch (WebDriverException ex)
            {
                Console.WriteLine($"WebDriver error occurred: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error when calling OpenAI: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                
            }
        }
    }
}
