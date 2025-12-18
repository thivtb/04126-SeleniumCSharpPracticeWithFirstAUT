using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
namespace _04126_UnplashSelenium.Pages
{
    internal class CreateCollectionModalPage : BasePage
    {
        public CreateCollectionModalPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }
        By addToCollectionDialog = By.CssSelector("div[role='dialog']");
        By emptyCollectionText = By.CssSelector("div[role='status']");
        By createANewCollectionBtn = By.XPath("//button[contains(text(),'Create a new collection')]");

        // Dialog Create a new collection
        By creatANewCollectionTitle = By.XPath("//button[contains(@class, 'header') and contains(text(),'Create a new collection')]");
        By nameTextField = By.CssSelector("input[name='title']");
        By descriptionTextArea = By.CssSelector("textarea[name='description']");
        By createCollectionBtn = By.XPath("//button[contains(.,'Create collection')]");
        public void WaitForDialog()
        {
            WaitForModal(addToCollectionDialog);
        }
        public void clickCreateANewCollectionButton()
        {
            WaitAndFindElement(createANewCollectionBtn).Click();
            Thread.Sleep(800);
        }
        public void WaitForCreateCollectionForm()
        {
            WaitAndFindElement(creatANewCollectionTitle);
        }
        public void enterName(string name)
        {
            IWebElement nameTxt = WaitAndFindElement(nameTextField);
            nameTxt.Clear();
            nameTxt.SendKeys(name);
        }
        public void enterDescription(string description)
        {
            IWebElement descriptionTxt = WaitAndFindElement(descriptionTextArea);
            descriptionTxt.Clear();
            descriptionTxt.SendKeys(description);
        }
        public void clickCreateCollection()
        {
            IWebElement btn = WaitAndFindElement(createCollectionBtn);
            btn.Click();
            Thread.Sleep(2000);
        }
        public void createANewCollection(string name, string description)
        {
            WaitForCreateCollectionForm();
            enterName(name);
            enterDescription(description);
            WaitAndFindElement(createCollectionBtn).Click();
        }
    }
}