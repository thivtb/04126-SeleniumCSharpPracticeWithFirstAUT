using _04126_UnplashSelenium.Helpers;
using _04126_UnplashSelenium.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using static System.Net.Mime.MediaTypeNames;

namespace _04126_UnplashSelenium;

public class LikePhotosTest : BaseTest
{
    [Test]
    public async Task NavigateToUserProfileFromPhotoPage()
    {
        // Login
        HomePage homePage = new HomePage(driver, wait);
        LoginPage loginPage = new LoginPage(driver, wait);

        string email = settings.Email;
        string password = settings.Password;
        homePage.clickLogin();
        loginPage.Login(email, password);

        // Like 3 random photos
        homePage.HoverAndLikeImages(3);

        // Go to https://unsplash.com/@user_name/likes
        homePage.ClickLikeNavIcon();
        LikePhotoPage likePhotoPage = new LikePhotoPage(driver, wait);
        likePhotoPage.RefreshPage();

        // Verify see the number of likes is 3 and 3 photos appear in Likes section
        Assert.That(likePhotoPage.IsImageCountTextDisplayed(), Is.True,
                "The text '3 images' is not displayed");

        int imageCount = likePhotoPage.GetLikedImagesCount();
        Assert.That(imageCount, Is.GreaterThanOrEqualTo(3),
                $"Wrong number of images in Likes section. Expected: 3, Actual: {imageCount}");

        //Use API for unliking photos
        var photoApi = new PhotoApiHelper(settings.UnsplashAccessToken, settings.UnsplashApiBaseUrl);

        var likedPhotoIds = await photoApi.GetLikedPhotoIds(settings.UnsplashUsername);

        foreach (var photoId in likedPhotoIds)
        {
            await photoApi.DeleteLikedPhoto(photoId);
        }
    }
}
