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

namespace ProbabilityX_API.Services
{
    public class EarningCalendarService : IEarningCalendarService
    {
        private readonly IEarningCalendarRepository _earningcalendarRepository;
        private readonly ICompanyService _companyService;

        public EarningCalendarService(IEarningCalendarRepository earningcalendarRepository, ICompanyService companyService)
        {
            _earningcalendarRepository = earningcalendarRepository;
            _companyService = companyService;
        }

        public async Task<List<EarningCalendarModel>> ScrapperNextWeekEarningCalendar()
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
                var companyNames = driver.FindElements(By.ClassName("earnCalCompanyName"));
                var companyTags = driver.FindElements(By.CssSelector(".bold.middle"));
                var epsElements = driver.FindElements(By.CssSelector("[class*='eps_actual']"));
                var links = driver.FindElements(By.CssSelector("a.bold.middle"));
                if(companyNames.Count() == links.Count())
                {
                    Console.WriteLine("All Good");
                }
                foreach (var link in links)
                {
                    int index = links.IndexOf(link);
                    var newCompany = new CompanyModel
                    {
                        CompanyName = companyNames[index].Text,
                        StockSymbol = companyTags[index].Text,
                        Id_StockType = 1 // Remplacez la valeur par l'ID approprié du type de stock
                    };

                    var isCompanyAdded = _companyService.AddCompany(newCompany).Result;

                    var principalHandle = driver.CurrentWindowHandle;
                    if (isCompanyAdded != null)
                    {
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
                            var fullName = driver.FindElement(By.CssSelector("[itemprop='name']"));
                            var market = driver.FindElement(By.ClassName("btnTextDropDwn"));
                            string yourData = fullName.Text;
                            Console.WriteLine($"Donnée récupérée : {yourData}");
                            // Boucle tant que le bouton "Voir plus" est présent
                            while (IsElementVisible(driver, By.CssSelector("#showMoreEarningsHistory")))
                            {
                                // Cliquer sur le bouton "Voir plus"
                                IWebElement voirPlusButton = driver.FindElement(By.CssSelector("#showMoreEarningsHistory"));
                                voirPlusButton.Click();

                                // Attendre un court instant (peut être ajusté selon vos besoins)
                                Thread.Sleep(1000);
                            }
                            var instrumentEarningsHistory = driver.FindElements(By.CssSelector("tr[name='instrumentEarningsHistory']"));
                            Console.WriteLine($"Nombre d'historique : {instrumentEarningsHistory.Count()}");
                            foreach (var historyData in instrumentEarningsHistory)
                            {
                                var tds = historyData.FindElements(By.CssSelector("td"));

                                // Assurez-vous que vous avez au moins le nombre attendu de cellules
                                if (tds.Count == 6)
                                {
                                    // Récupérer les données de chaque cellule
                                    var dateOut = tds[0].Text;
                                    var periodDate = tds[1].Text;
                                    var bpa = tds[2].Text;
                                    var bpaPrev = tds[3].Text;
                                    var performance = tds[4].Text;
                                    var performancePrev = tds[5].Text;

                                    // Imprimer les données récupérées dans la console
                                    Console.WriteLine($"Company: {isCompanyAdded.CompanyName} vs RealName: {fullName} Date: {dateOut}, Period Date: {periodDate}, BPA: {bpa}, BPA Previous: {bpaPrev}, Performance: {performance}, Performance Previous: {performancePrev}");
                                }
                                else
                                {
                                    Console.WriteLine("Le nombre de cellules n'est pas celui attendu.");
                                }
                            }
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
                }
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



        // Méthode pour vérifier la visibilité d'un élément (style != 'none')
        static bool IsElementVisible(IWebDriver driver, By by)
        {
            try
            {
                IWebElement element = driver.FindElement(by);
                return element.Displayed && element.GetAttribute("style") != "display: none;";
            }
            catch (NoSuchElementException)
            {
                return false;
            }
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