using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace _04126_UnplashSelenium.Pages
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        // Locators
        By loginLink = By.XPath("//a[contains(text(),'Log in')]");
        By firstPhoto = By.CssSelector("figure a[href*='/photos/']");
        By accountAvatar = By.CssSelector("img[alt*='Avatar of user']");
        By accountSettingsLink = By.XPath("//a[contains(text(),'Account settings')]");
        By profilePopup = By.CssSelector("div[role='presentation'][data-side='right']");
        By imageCards = By.CssSelector("figure[itemprop='image']");
        By bookmarkIcons = By.CssSelector("button[aria-label='Bookmark']");
        By bookmarkNavbar = By.XPath("//a[contains(@href, '/bookmarks')]");
        By addCollectionIcon = By.XPath(".//button[@aria-label='Add to Collection']");
        By collectionIcon = By.XPath("//a[@aria-label='Collections']");

        public void clickLogin()
        {
            var photoEle = WaitAndFindElement(loginLink);
            photoEle.Click();
        }

        public void ClickFirstPhoto()
        {
            var photoEle = WaitAndFindElement(firstPhoto);
            photoEle.Click();
        }

        public void OpenAccountSettings()
        {
            WaitAndFindElement(accountAvatar).Click();
            WaitAndFindElement(profilePopup);
            WaitAndFindElement(accountSettingsLink).Click();
        }

        public List<IWebElement> GetAllImages()
        {
            wait.Until(d => d.FindElements(imageCards).Count > 0);
            Thread.Sleep(1000);
            return driver.FindElements(imageCards).ToList();
        }

        public IWebElement GetImageByIndex(int index)
        {
            List<IWebElement> images = GetAllImages();

            if (index >= 0 && index < images.Count)
            {
                return images[index];
            }
            return null;
        }

        public void ClickRandomImage()
        {
            List<IWebElement> images = GetAllImages();

            Random random = new Random();
            int index = random.Next(images.Count);

            var selectedImage = images[index];
            ScrollToElement(selectedImage);
            selectedImage.Click();
        }

        public void HoverAndClickAddIcon(IWebElement image)
        {
            ScrollToElement(image);
            HoverElement(image);
            IWebElement addIcon = wait.Until(d =>
            {
                try
                {
                    var icon = image.FindElement(addCollectionIcon);
                    return (icon.Displayed) ? icon : null;
                }
                catch
                {
                    return null;
                }
            });
            addIcon.Click();
        }


        public void ClickAddIcon(IWebElement image)
        {
            ScrollToElement(image);
            HoverElement(image);
            var addIcon = image.FindElement(addCollectionIcon);
            try
            {
                addIcon.Click();
            }
            catch (ElementNotInteractableException)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", addIcon);
            }

            Thread.Sleep(500);
        }

        public void AddImageToCollection(int index)
        {
            IWebElement image = GetImageByIndex(index);

            ScrollToElement(image);

            IWebElement hoverTarget = image.FindElement(By.CssSelector("a"));
            HoverElement(hoverTarget);

            IWebElement addIcon = image.FindElement(addCollectionIcon);

            addIcon.Click();
        }

        public void AddImagesToCollection(int count)
        {
            List<IWebElement> images = GetAllImages().Take(count).ToList();
            foreach (var image in images)
            {
                try
                {
                    ClickAddIcon(image);
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding image to collection: {ex.Message}");
                    throw;
                }
            }
        }


        public void HoverAndLikeImages(int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    IWebElement image = GetImageByIndex(i);

                    if (image != null)
                    {
                        ScrollToElement(image);
                        HoverElement(image);

                        var bookmarkBtn = image.FindElements(bookmarkIcons).FirstOrDefault();
                        if (bookmarkBtn != null)
                        {
                            bookmarkBtn.Click();
                            Thread.Sleep(700);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error bookmarking image {i}: {ex.Message}");
                }
            }
        }

        public void ClickLikeNavIcon()
        {
            WaitAndFindElement(bookmarkNavbar).Click();
        }

        public void ClickCollectionsNav()
        {
            WaitAndFindElement(collectionIcon).Click();
        }

        public void RemoveImageFromCollection(int index)
        {
            IWebElement image = GetImageByIndex(index);

            ScrollToElement(image);

            IWebElement hoverTarget = image.FindElement(By.CssSelector("a"));
            HoverElement(hoverTarget);

            IWebElement addIcon = image.FindElement(addCollectionIcon);

            addIcon.Click();
        }

        public void GoToCollectionsPage()
        {
            IWebElement iconCollectionsNavBar = WaitAndFindElement(collectionIcon);
            iconCollectionsNavBar.Click();
        }
    }
}