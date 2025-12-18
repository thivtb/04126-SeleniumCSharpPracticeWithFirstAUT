using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace _04126_UnplashSelenium.Pages
{
    internal class AddImagesToCollectionModalPage : BasePage
    {
        public AddImagesToCollectionModalPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait) { }

        // Recent collections
        By addToCollectionDialog = By.CssSelector("div[role='dialog']");
        By recentCollections = By.CssSelector("div[role='option']");
        By emptyCollectionText = By.CssSelector("div[role='status']");
        By createANewCollectionBtn = By.CssSelector("button[class*='createCollectionButton']");

        // Icons on collections
        By iconAdd = By.CssSelector("svg[class*='addAction-']");
        By iconTick = By.CssSelector("svg[data-selected]");
        By iconRemove = By.CssSelector("svg[class*='removeAction-']");

        public void WaitForDialog()
        {
            IWebElement dialog = WaitForModal(addToCollectionDialog);
        }

        public bool HasNoCollections()
        {
            try
            {
                var emptyText = driver.FindElement(emptyCollectionText);
                Console.WriteLine("You don’t have any collections.");
                return emptyText.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void ClickCreateNewCollectionButton()
        {
            WaitAndFindElement(createANewCollectionBtn).Click();
        }

        public void HoverAndAddToCollection(IWebElement collection)
        {
            HoverElement(collection);
            collection.FindElement(iconAdd).Click();
        }

        public void WaitForDialogToClose()
        {
            wait.Until(d =>
            {
                try
                {
                    var dialogs = d.FindElements(addToCollectionDialog);
                    return dialogs.Count == 0 || !dialogs[0].Displayed;
                }
                catch
                {
                    return true;
                }
            });
            Thread.Sleep(1000);
            Console.WriteLine("Create collection dialog closed");
        }

        public void AddToFirstRecentCollection()
        {
            WaitForDialog();
            IWebElement collection = wait.Until(d =>
            {
                var els = d.FindElements(recentCollections);
                return els.Count > 0 ? els[0] : null;
            });
            HoverElement(collection);
            IWebElement addIcon = collection.FindElement(iconAdd);
            addIcon.Click();
        }

        public void HoverAndRemoveFromCollection(IWebElement collection)
        {
            HoverElement(collection);
            IWebElement removeIcon = wait.Until(d =>
            {
                try
                {
                    var icon = collection.FindElement(iconRemove);
                    return (icon.Displayed) ? icon : null;
                }
                catch
                {
                    return null;
                }
            });
            removeIcon.Click();
        }


        public void RemoveFromFirstRecentCollection()
        {
            WaitForDialog();
            IWebElement collection = wait.Until(d =>
            {
                var els = d.FindElements(recentCollections);
                return els.Count > 0 ? els[0] : null;
            });
            HoverElement(collection);
            IWebElement addIcon = collection.FindElement(iconRemove);
            addIcon.Click();
        }

        public bool IsImageInCollection(IWebElement collection)
        {
            try
            {
                IWebElement tickIcon = collection.FindElement(iconTick);
                return tickIcon.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public void CloseModal()
        {
            CloseModalByEsc(By.CssSelector("body"));
        }

    }
}
