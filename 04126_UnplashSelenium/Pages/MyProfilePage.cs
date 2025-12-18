using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class MyProfilePage : BasePage
    {
        IWebDriver driver;
        WebDriverWait wait;

        public MyProfilePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        By accountSettingsHeader = By.XPath("//h3[contains(text(), 'Account settings')]");
        By usernameTextbox = By.CssSelector("input[id='user_username']");
        By updateAccountBtn = By.CssSelector("input[value='Update account']");
        By updateSuccessNoti = By.XPath("//div[contains(text(), 'Account updated!')]");
        By firstName = By.CssSelector("input#user_first_name");
        By lastName = By.CssSelector("input#user_last_name");


        public bool isAtAccountSettingsPage()
        {
            IWebElement header = WaitAndFindElement(accountSettingsHeader);
            return header.Displayed;
        }

        public string getUsername()
        {
            IWebElement name = WaitAndFindElement(usernameTextbox);
            return name.GetAttribute("value");
        }

        public void enterNewUserName(string newUsername)
        {
            IWebElement usernameTxt = WaitAndFindElement(usernameTextbox);
            usernameTxt.Clear();
            usernameTxt.SendKeys(newUsername);
        }

        public void clickUpdateAccount()
        {
            IWebElement btnUpdate = WaitAndFindElement(updateAccountBtn);
            btnUpdate.Click();
        }

        public bool isUpdateSuccess()
        {
            try
            {
                IWebElement updateSuccessNotification = WaitAndFindElement(updateSuccessNoti);
                return updateSuccessNotification.Displayed;
            }
            catch
            {
                return false;
            }
        }
        public string getFullName()
        {
            IWebElement first = WaitAndFindElement(firstName);
            IWebElement last = WaitAndFindElement(lastName);

            string fName = first.GetAttribute("value");
            string lName = last.GetAttribute("value");

            return $"{fName} {lName}".Trim();
        }
    }
}
