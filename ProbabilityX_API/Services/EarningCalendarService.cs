using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ProbabilityX_API.Services
{
    public class EarningCalendarService : IEarningCalendarService
    {
        private readonly IEarningCalendarRepository _earningcalendarRepository;

        public EarningCalendarService(IEarningCalendarRepository earningcalendarRepository)
        {
            _earningcalendarRepository = earningcalendarRepository;
        }

        public async Task<List<EarningCalendarModel>> GetNextWeekEarningCalendar()
        {
            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArgument("--headless"); // Run Chrome in headless mode (without UI)
            var driver = new ChromeDriver(chromeOptions);

            try
            {
                // Navigate to the investing.com website
                driver.Navigate().GoToUrl("https://fr.investing.com/earnings-calendar/");
                await Task.Delay(2000);
                var acceptButtonCookie = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
                acceptButtonCookie.Click();
                var closeButtonRegister = WaitUntilElementIsVisibleWithRetry(driver, By.ClassName("popupCloseIcon"), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
                closeButtonRegister.Click();
                var filterNexWeek = driver.FindElement(By.Id("timeFrame_nextWeek"));
                filterNexWeek.Click();
                var date = driver.FindElement(By.ClassName("theDay"));
                Console.WriteLine(date);
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
                //driver.Quit();
            }

            return null;
        }

        // Fonction pour attendre qu'un élément soit visible
        IWebElement WaitUntilElementIsVisible(IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        IWebElement WaitUntilElementIsVisibleWithRetry(IWebDriver driver, By by, TimeSpan timeout, TimeSpan retryInterval)
        {
            var wait = new WebDriverWait(driver, timeout);
            IWebElement element = null;
            try
            {
                element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
            }
            catch (WebDriverTimeoutException)
            {
                // Ignorer l'exception et réessayer
            }

            // Si l'élément n'est pas encore visible, réessayer avec un intervalle
            while (element == null)
            {
                Task.Delay(retryInterval).Wait();
                try
                {
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                }
                catch (WebDriverTimeoutException)
                {
                    // Ignorer l'exception et continuer à réessayer
                }
            }

            return element;
        }

        // Fonction pour attendre qu'un élément soit cliquable
        IWebElement WaitUntilElementIsClickable(IWebDriver driver, By by, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }
    }
}