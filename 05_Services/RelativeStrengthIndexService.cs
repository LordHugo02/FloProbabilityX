using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.RegularExpressions;

namespace ProbabilityX_API.Services
{
    public class RelativeStrengthIndexService : IRelativeStrengthIndexService
    {
        public RelativeStrengthIndexService()
        {
        }

        public async Task<List<RelativeStrengthIndexModel>> GetExtremeRelativeStrengthIndex()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless"); // Run Chrome in headless mode (without UI)
            chromeOptions.AddArguments("--ignore-certificate-errors");
            var driver = new ChromeDriver(chromeOptions);
            string apiUrl = "https://api.taapi.io/rsi";
            string secret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjbHVlIjoiNjVmODFlNzUyYzczYzFlM2ZkOWRkZjRhIiwiaWF0IjoxNzEwNzU5NTQxLCJleHAiOjMzMjE1MjIzNTQxfQ.WiJfUmxx45GN_xLRrtSJmXfGrljav5cCrrF8mfpSeQw";
            string exchange = "binance";
            string symbol = "BTC/USDT";
            string interval = "4h";
            int backtrack = 1;

            string requestUrl = $"{apiUrl}?secret={secret}&exchange={exchange}&symbol={symbol}&interval={interval}&backtrack={backtrack}";

            try
            {
                // Navigate to the investing.com website
                driver.Navigate().GoToUrl(requestUrl);
                string htmlSource = driver.PageSource;

                // Use regex to extract the value from the HTML source
                Regex regex = new Regex("\"value\":(\\d+\\.\\d+)");
                Match match = regex.Match(htmlSource);

                if (match.Success)
                {
                    string value = match.Groups[1].Value;
                    Console.WriteLine("Value: " + value);
                }
                else
                {
                    Console.WriteLine("Value not found in HTML source.");
                };


            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                // Close the WebDriver
                driver.Quit();
            }

            return null;
        }

    }
}