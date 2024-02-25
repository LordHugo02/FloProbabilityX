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

            try
            {
                // Navigate to the investing.com website
                driver.Navigate().GoToUrl("https://www.etoro.com/markets/nsdq100/chart/");
                IWebElement chartIndicator = driver.FindElement(By.ClassName("indicators-control"));
                // Cliquer sur l'élément
                await Task.Delay(2000);

                chartIndicator.Click();
                await Task.Delay(5000);
                IWebElement dialog = driver.FindElement(By.CssSelector("body > .dialog"));

                var inputField = driver.FindElements(By.ClassName("dialog"));
                Console.WriteLine(inputField.Count());
                Console.WriteLine(dialog);


                // Utiliser JavaScript pour mettre en focus sur le champ d'entrée
                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                //js.ExecuteScript("arguments[0].focus();", inputField);
                // Cliquer sur le champ d'entrée
                //inputField.SendKeys("RSI");
                await Task.Delay(2000);
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