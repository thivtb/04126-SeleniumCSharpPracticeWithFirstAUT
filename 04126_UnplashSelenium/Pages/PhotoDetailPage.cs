using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class PhotoDetailPage : BasePage
    {
        public PhotoDetailPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait) { }
        By downloadButton = By.XPath("//a[contains(text(),'Download')]");

        public void ClickDownload()
        {
            var downloadBtn = wait.Until(d =>
            {
                var el = d.FindElement(downloadButton);
                return el.Displayed && el.Enabled ? el : null;
            });

            downloadBtn.Click();
        }
    }
}
