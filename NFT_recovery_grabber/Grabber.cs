using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFT_recovery_grabber
{
    internal class Grabber
    {
        string NftRecoveryUrl { get; set; }
        string LauncherID {get; set; }

        public Grabber(string nftRecoveryUrl, string launcherID)
        {
            
            NftRecoveryUrl = nftRecoveryUrl;
            LauncherID = launcherID;
        }
        public void GetCoins(int secondsBeforeClose, bool hidden = false)
        {
            FileLogger.Log("Collecting coins");

            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            //options.AddArguments("--incognito");

            if(hidden)
            {
                options.AddArguments("--headless");
            }

            IWebDriver Driver = null;

            try
            {
                Driver = new ChromeDriver(chromeDriverService, options);

                FileLogger.Log("Browser started");

                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(1));
                Driver.Navigate().GoToUrl(NftRecoveryUrl);

                IWebElement launcherIdInput = null;

                try
                {
                    launcherIdInput = Driver.FindElement(By.Id("__BVID__186"));
                }
                catch
                {
                    launcherIdInput = Driver.FindElement(By.XPath("//input[@placeholder = '0x...']"));
                }

                launcherIdInput.Clear();
                launcherIdInput.SendKeys(LauncherID);

                Driver.FindElement(By.XPath("//button[text() = 'CONTINUE']")).Click();

                IWebElement SelectAllCoins = new WebDriverWait(Driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.XPath("//label[text() = 'select all']")));
                SelectAllCoins.Click();

                Driver.FindElement(By.XPath("//button[text() = 'START RECOVERY']")).Click();

                WebDriverWait otherWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(15));
                Thread.Sleep(15000);

                IWebElement closeButton = new WebDriverWait(Driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//footer/button")));

                IWebElement DialogContainer = Driver.FindElement(By.XPath("//div[@role=\"dialog\"]"));
                DialogContainer.Click();

                closeButton.Click();

                Thread.Sleep(secondsBeforeClose * 1000);
                Driver.Close();
                FileLogger.Log("Browser closed");
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Error: {ex.Message}");

                if (Driver != null)
                {
                    Driver.Close();
                    FileLogger.Log("Browser closed");
                }
                
            }

        }
    }
}
