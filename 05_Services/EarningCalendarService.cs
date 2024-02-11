using Microsoft.AspNetCore.Mvc;
using ProbabilityX_API.IRepositories;
using ProbabilityX_API.IServices;
using ProbabilityX_API.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.Text;

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
            chromeOptions.AddArguments("--ignore-certificate-errors");
            var driver = new ChromeDriver(chromeOptions);

            try
            {
                // Navigate to the investing.com website
                driver.Navigate().GoToUrl("https://fr.investing.com/earnings-calendar/");
                await Task.Delay(2000);
                var acceptButtonCookie = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
                acceptButtonCookie.Click();
                var closeButtonRegister = WaitUntilElementIsVisibleWithRetry(driver, By.ClassName("popupCloseIcon"), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1));
                if (closeButtonRegister != null)
                {
                    closeButtonRegister.Click();
                }
                var filterNexWeek = driver.FindElement(By.Id("timeFrame_nextWeek"));
                filterNexWeek.Click();
              
                for (int i = 0; i < 3; i++)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                    await Task.Delay(2000);
                }

                var links = driver.FindElements(By.CssSelector("a.bold.middle"));

                foreach (var link in links)
                {
                    var principalHandle = driver.CurrentWindowHandle;

                    // Utiliser JavaScript pour cliquer sur le lien
                    IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    executor.ExecuteScript("arguments[0].click();", link);

                    // Attendez que la nouvelle fenêtre soit disponible
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                    wait.Until(driver => driver.WindowHandles.Count > 1);

                    // Basculer vers la nouvelle fenêtre
                    var newHandle = driver.WindowHandles.Except(new List<string> { principalHandle }).Single();
                    driver.SwitchTo().Window(newHandle);

                    try
                    {
                        // Récupérez la donnée souhaitée ici
                        var yourDataElement = driver.FindElement(By.Id("yourDataElementId"));
                        string yourData = yourDataElement.Text;
                        Console.WriteLine($"Donnée récupérée : {yourData}");
                    }
                    catch (NoSuchElementException)
                    {
                        Console.WriteLine("L'élément n'a pas été trouvé.");
                    }
                    finally
                    {
                        // Fermez la nouvelle fenêtre
                        driver.Close();

                        // Revenez à la fenêtre principale
                        driver.SwitchTo().Window(principalHandle);
                    }
                }

                //// Attendre un moment pour permettre le chargement des dates supplémentaires
                //var dateElements = driver.FindElements(By.ClassName("theDay"));
                //Console.WriteLine(dateElements.Count);
                //var companyNames = driver.FindElements(By.ClassName("earnCalCompanyName"));
                //Console.WriteLine(companyNames.Count);
                //var companyTags = driver.FindElements(By.CssSelector(".bold.middle"));
                //Console.WriteLine(companyTags.Count);
                //var epsElements = driver.FindElements(By.CssSelector("[class*='eps_actual']"));
                //Console.WriteLine(epsElements.Count);

                //string filePath = "fichier.txt";

                //// Utilisez StreamWriter pour écrire dans le fichier
                //using (StreamWriter sw = new StreamWriter(filePath))
                //{
                //    for (int i = 0; i < companyNames.Count; i++)
                //    {
                //        string companyNameText = companyNames[i].Text;
                //        string companyTagText = companyTags[i].Text;
                //        string epsText = epsElements[i].Text;

                //        // Écrire les données dans le fichier
                //        sw.WriteLine($"Company Name: {companyNameText}, Company Tag: {companyTagText}, EPS: {epsText}");
                //    }
                //}

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
        IWebElement WaitUntilElementIsVisibleWithRetry(IWebDriver driver, By by, TimeSpan timeout, TimeSpan retryInterval)
        {
            var wait = new WebDriverWait(driver, timeout);
            var retryNumber = 0;
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
            while (element == null || retryNumber < 5)
            {
                Task.Delay(retryInterval).Wait();
                try
                {
                    Console.WriteLine("Retry", retryNumber);
                    retryNumber= retryNumber + 1;
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
                }
                catch (WebDriverTimeoutException)
                {
                    // Ignorer l'exception et continuer à réessayer
                }
            }

            return element;
        }
    }
}