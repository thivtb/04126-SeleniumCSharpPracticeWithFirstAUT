using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class CollectionsPage : BasePage
    {
        public CollectionsPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait) { }

        By collectionImages = By.CssSelector("div[class*='imagesContainer']");
        By imageCountText = By.CssSelector("div[class*='description']");

        public void VerifyOnCollectionsPageByUrl()
        {
            wait.Until(driver =>
                driver.Url.Contains("/collections")
            );

            if (!driver.Url.Contains("/collections"))
            {
                throw new Exception(
                    $"Not on Collections page. Current URL: {driver.Url}"
                );
            }
        }

        public bool verify1ImageAppears()
        {
            IWebElement numberOfImages = WaitAndFindElement(imageCountText);
            try
            {
                return numberOfImages.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public string GetCollectionIdFromUrl()
        {
            var url = driver.Url;
            var parts = url.Split('/');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "collections" && i + 1 < parts.Length)
                {
                    return parts[i + 1];
                }
            }
            return null;
        }
    }
}
