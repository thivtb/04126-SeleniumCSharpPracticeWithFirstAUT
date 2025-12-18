using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
namespace _04126_UnplashSelenium.Pages
{
    internal class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;
        public BasePage(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
            this.actions = new Actions(driver);
        }
        public IWebElement WaitAndFindElement(By locator)
        {
            return wait.Until(driverContext =>
            {
                var elements = driverContext.FindElements(locator);
                if (elements.Count > 0 && elements[0].Displayed)
                {
                    return elements[0];
                }
                return null;
            });
        }
        public IWebElement WaitForElementEnable(By locator)
        {
            return wait.Until(driverContext =>
            {
                var elements = driverContext.FindElements(locator);
                if (elements.Count > 0 && elements[0].Displayed && elements[0].Enabled)
                {
                    return elements[0];
                }
                return null;
            });
        }
        public IWebElement WaitForModal(By modalSelector)
        {
            return wait.Until(driverContext =>
            {
                var modals = driverContext.FindElements(modalSelector);
                if (modals.Count > 0 && modals[0].Displayed)
                {
                    return modals[0];
                }
                return null;
            });
        }
        public void HoverElement(IWebElement element)
        {
            actions.MoveToElement(element).Pause(TimeSpan.FromMilliseconds(300)).Perform();
        }
        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)driver)
                .ExecuteScript("arguments[0].scrollIntoView({block:'center'});", element);
            Thread.Sleep(500);
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
        }
        public void gotoURL(string url, int waitTimeMs)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(waitTimeMs);
        }
        public void CloseModalByEsc(By by)
        {
            driver.FindElement(by).SendKeys(Keys.Escape);
            Thread.Sleep(1000);
        }

        public void ClickElementWithJS(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        protected void WaitForElementClickable(By locator)
        {
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed && element.Enabled;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
    }
}