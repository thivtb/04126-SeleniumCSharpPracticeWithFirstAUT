using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class BookmarksPage : BasePage
    {
        public BookmarksPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        By imageCountText = By.XPath("//span[normalize-space()='3 images']");
        By images = By.CssSelector("figure[itemprop='image']");
        By clearButton = By.XPath("//button[contains(text(),'Clear')]");
        By clearBookmarksButton = By.XPath("//button[contains(text(),'Clear bookmarks')]");
        By modal = By.CssSelector("div[role='alertdialog']");
        By emptyStateText = By.CssSelector("h2");

        public bool IsImageCountTextDisplayed()
        {
            try
            {
                return WaitAndFindElement(imageCountText).Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void getImageCountText()
        {
            IWebElement imageCountTxt = WaitAndFindElement(imageCountText);
            string text = imageCountTxt.Text;
        }

        public int GetBookmarkedImagesCount()
        {
            WaitAndFindElement(images);
            return driver.FindElements(images).Count;
        }

        public void ClickClearButton()
        {
            WaitAndFindElement(clearButton).Click();
        }

        public void ConfirmClearBookmarks()
        {
            WaitAndFindElement(clearBookmarksButton).Click();

            wait.Until(driver =>
            {
                try
                {
                    var confirmModal = driver.FindElement(modal);
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });
        }

        public bool verifyImagesAreDeleted()
        {
            try
            {
                return WaitAndFindElement(emptyStateText).Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
