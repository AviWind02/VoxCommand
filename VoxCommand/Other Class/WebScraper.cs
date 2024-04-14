using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

//This is using CNN if they ever change the HTML layout this is just trash, works for now but just gonna switch to an API
namespace VoxCommand.Other_Class
{
    
    internal class WebScraper
    {


        public List<string> NewsScraper()
        {
            List<string> detailsList = new List<string>();


            Console.WriteLine("Initializing ChromeDriver with headless option...");
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless"); // Run Chrome in headless mode
            options.AddArguments("--disable-gpu"); // Disabling GPU acceleration if not needed
            options.AddArguments("--no-sandbox"); // Bypass OS security model

            try
            {
                using (var driver = new ChromeDriver(options))
                {
                    Console.WriteLine("Navigating to CNN...");
                    driver.Navigate().GoToUrl("https://www.cnn.com/world");
                    Console.WriteLine("Waiting for page to load...");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    Console.WriteLine("Searching for news headlines...");
                    var headlines = driver.FindElements(By.CssSelector("a.container__link"));
                    List<Tuple<string, string>> newsItems = new List<Tuple<string, string>>();
                    foreach (var headline in headlines.Take(10)) // Only process the first 10 headlines
                    {
                        try
                        {
                            var headlineTextElement = headline.FindElement(By.CssSelector("span.container__headline-text"));
                            string headlineText = headlineTextElement?.Text; // Using null-conditional operator to avoid null reference exception
                            string url = headline.GetAttribute("href");

                            if (!string.IsNullOrEmpty(headlineText) && !string.IsNullOrEmpty(url))
                            {
                                newsItems.Add(new Tuple<string, string>(headlineText, url));
                                Console.WriteLine($"Found headline: {headlineText}");
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            // Log if specific element within link is not found
                            Console.WriteLine("Headline text element not found within link, skipping...");
                        }
                    }

                    Console.WriteLine("Displaying all found headlines and URLs:");
                    foreach (var item in newsItems)
                    {
                        Console.WriteLine(item);
                        string modifiedUrl = item.Item2.Contains("live-news") ? item.Item2.Replace("?tab=all", "?tab=Catch%20Up") : item.Item2;
                        Console.WriteLine($"Navigating to URL: {modifiedUrl}");
                        driver.Navigate().GoToUrl(modifiedUrl);

                        Console.WriteLine("Waiting for page content to load...");
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                        try
                        {
                            if (item.Item2.Contains("live-news"))
                            {
                                var paragraphs = driver.FindElements(By.CssSelector("p.sc-gZMcBi.render-stellar-contentstyles__Paragraph-sc-9v7nwy-2.dCwndB")).Select(x => x.Text)
                                .Where(p => !p.Contains("Here are other headlines you should know:"))
                                .Take(5);
                                foreach (var paragraph in paragraphs)
                                {
                                    string detail = $"{{ {item.Item1} : {paragraph} }}";
                                    if (!detailsList.Contains(detail) && !paragraph.Contains("Here are other headlines you should know:"))
                                    {
                                        detailsList.Add(detail);
                                        Console.WriteLine($"Added new detail: {detail}");
                                    }
                                }
                            }
                            else
                            {
                                var paragraphs = driver.FindElement(By.CssSelector("div.article__content[data-editable='content']"))
                                .FindElements(By.CssSelector("p"))
                                .Select(x => x.Text)
                                .Take(10);
                                foreach (var paragraph in paragraphs)
                                {
                                    string detail = $"{{ {item.Item1} : {paragraph} }}";
                                    if (!detailsList.Contains(detail))
                                    {
                                        detailsList.Add(detail);
                                        Console.WriteLine($"Added new detail: {detail}");
                                    }
                                }
                            }
                        }
                        catch (NoSuchElementException e)
                        {
                            Console.WriteLine($"Element not found: {e.Message}");
                        }

                        Console.WriteLine("Details collected, closing the driver...");
                    }
                }
                //Console.WriteLine("Displaying Info collected...");
                //foreach (var detail in detailsList)
                //{
                //    Console.WriteLine(detail);
                //}

            }
            catch (WebDriverException ex)
            {
                Console.WriteLine($"WebDriver exception occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Closing the driver...");
                // Driver is closed in the using block, but explicit close can be added here if not using 'using'
            }

            return detailsList;
        }

        public List<string> NewsDetailsScraper(string headline, string url)
        {
            Console.WriteLine("Initializing ChromeDriver with headless option...");
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless");
            options.AddArguments("--disable-gpu");
            options.AddArguments("--no-sandbox");

            List<string> detailsList = new List<string>();  // To store the unique details

            using (var driver = new ChromeDriver(options))
            {
               
            }

            return detailsList;  // Return the list of collected unique details
        }

    }
}
