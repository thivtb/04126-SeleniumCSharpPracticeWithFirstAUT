using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class ViewProfileModal : BasePage
    {
        public ViewProfileModal(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        By avatar = By.CssSelector("header a[href*='/@'] img");
        By viewProfileButton = By.XPath("//a[contains(text(),'View profile')]");
        By photographerName = By.CssSelector("header a[class^='name']");
        By modal = By.CssSelector("div[role='dialog']");

        public void HoverUserAvatar()
        {
            WaitForModal(modal);
            var avatarEle = WaitAndFindElement(avatar);
            Actions actions = new Actions(driver);
            actions.MoveToElement(avatarEle).Perform();
        }

        public void ClickViewProfile()
        {
            WaitForModal(modal);
            var profileBtn = WaitAndFindElement(viewProfileButton);
            profileBtn.Click();
        }

        public string GetPhotographerName()
        {
            WaitForModal(modal);
            var nameEle = WaitAndFindElement(photographerName);
            return nameEle.Text.Trim();
        }
    }
}
