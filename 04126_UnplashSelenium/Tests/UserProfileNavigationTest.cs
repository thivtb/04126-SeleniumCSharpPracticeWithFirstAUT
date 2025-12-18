using _04126_UnplashSelenium.Pages;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace _04126_UnplashSelenium;

public class UserProfileNavigationTest : BaseTest
{
    [Test]
    public void NavigateToUserProfileFromPhotoPage()
    {
        HomePage homePage = new HomePage(driver, wait);
        LoginPage loginPage = new LoginPage(driver, wait);
        ViewProfileModal photoPage = new ViewProfileModal(driver, wait);
        ProfilePage profilePage = new ProfilePage(driver, wait);

        string email = settings.Email;
        string password = settings.Password;

        // Login
        homePage.clickLogin();
        loginPage.Login(email, password);

        // Click the first photo
        homePage.ClickFirstPhoto();

        // Get photographer name
        string expectedName = photoPage.GetPhotographerName();

        // Hover avatar + click View profile
        photoPage.HoverUserAvatar();
        photoPage.ClickViewProfile();

        Assert.That(profilePage.IsAtProfilePage(expectedName),
                $"Not navigated to profile page of {expectedName}");
    }
}
