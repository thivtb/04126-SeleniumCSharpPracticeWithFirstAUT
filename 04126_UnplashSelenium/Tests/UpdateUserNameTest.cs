using _04126_UnplashSelenium.Pages;
using c__basic_SD5858_VoThiBeThi_section1.Configs;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _04126_UnplashSelenium;

public class UpdateUserNameTest : BaseTest
{
    string newUsername;

    [Test]
    public void updateUserNameTest()
    {
        // Login
        HomePage homePage = new HomePage(driver, wait);
        homePage.clickLogin();

        LoginPage loginPage = new LoginPage(driver, wait);
        loginPage.Login(settings.Email, settings.Password);

        // Go to Account Settings
        homePage.OpenAccountSettings();

        MyProfilePage myProfilePage = new MyProfilePage(driver, wait);
        Assert.That(myProfilePage.isAtAccountSettingsPage(), "Not navigated to Account Settings Page!");

        // Get full name
        string fullName = myProfilePage.getFullName();

        // Update username
        string oldUsername = myProfilePage.getUsername();
        newUsername = oldUsername + DateTime.Now.ToString("HHmmss");
        Console.WriteLine("New Username = " + newUsername);

        myProfilePage.enterNewUserName(newUsername);
        myProfilePage.clickUpdateAccount();

        // Verify update username succeed
        Assert.That(myProfilePage.isUpdateSuccess(), "Username update failed!");

        // Go to new username profile
        string profileUrl = $"{settings.BaseUrl}/@{newUsername}";
        Console.WriteLine("Open Profile URL: " + profileUrl);
        driver.Navigate().GoToUrl(profileUrl);

        // Verify fullName is displayed
        By fullNameText = By.XPath("//div[contains(text(),'" + fullName + "')]");
        IWebElement fNameElement = myProfilePage.WaitAndFindElement(fullNameText);
        string currentFullName = fNameElement.Text;
        Assert.That(currentFullName.Contains(fullName),
                $"Full Name mismatch! Expected: {fullName}, Actual: {currentFullName}");
    }
}
