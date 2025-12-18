using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class ProfilePage : BasePage
    {
        IWebDriver driver;
        WebDriverWait wait;

        public ProfilePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        By profileName = By.CssSelector("div[class^='name-']");

        public bool IsAtProfilePage(string expectedName)
        {
            IWebElement nameEle = WaitAndFindElement(profileName);
            return nameEle.Text.Trim().Equals(expectedName, StringComparison.OrdinalIgnoreCase);
        }

    }
}
