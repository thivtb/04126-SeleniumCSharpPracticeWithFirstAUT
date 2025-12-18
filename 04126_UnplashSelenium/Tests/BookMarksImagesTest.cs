using _04126_UnplashSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;

namespace _04126_UnplashSelenium;

public class BookMarksImagesTest : BaseTest
{
    [Test]
    public void NavigateToUserProfileFromPhotoPage()
    {
        HomePage homePage = new HomePage(driver, wait);
        LoginPage loginPage = new LoginPage(driver, wait);

        string email = settings.Email;
        string password = settings.Password;

        // Login
        homePage.clickLogin();
        loginPage.Login(email, password);

        // Hover and book mark 3 images
        homePage.HoverAndBookmarkImages(3);

        // Go to Bookmarks page
        homePage.ClickBookmarkNavIcon();
        BookmarksPage bookmarksPage = new BookmarksPage(driver, wait);

        // Assertion
        Assert.That(bookmarksPage.IsImageCountTextDisplayed(), Is.True,
                "The text '3 images' is not displayed");

        int imageCount = bookmarksPage.GetBookmarkedImagesCount();
        Assert.That(imageCount, Is.GreaterThanOrEqualTo(3),
                $"Wrong number of images in bookmarks. Expected: 3, Actual: {imageCount}");

        // Clear images in bookmarks
        bookmarksPage.ClickClearButton();
        bookmarksPage.ConfirmClearBookmarks();
        bookmarksPage.RefreshPage();

        Assert.That(bookmarksPage.verifyImagesAreDeleted(), Is.True,
                "The text 'Bookmark images to view later' is not displayed");
    }
}
