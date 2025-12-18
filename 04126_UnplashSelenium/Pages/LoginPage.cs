using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class LoginPage : BasePage
    {
        IWebDriver driver;
        WebDriverWait wait;
        public LoginPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        By emailField = By.CssSelector("input[type='email']");
        By passwordField = By.CssSelector("input[type='password']");
        By submitButton = By.XPath("//button[text()='Login']");

        public void EnterEmail(string email)
        {
            var emailEle = WaitAndFindElement(emailField);
            emailEle.Clear();
            emailEle.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            var passwordEle = WaitAndFindElement(passwordField);
            passwordEle.Clear();
            passwordEle.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            var submitBtn = WaitAndFindElement(submitButton);
            submitBtn.Click();
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickLoginButton();
        }
    }
}
