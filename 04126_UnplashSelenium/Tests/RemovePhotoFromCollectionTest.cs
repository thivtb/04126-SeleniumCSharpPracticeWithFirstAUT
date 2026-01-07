using _04126_UnplashSelenium.Helpers;
using _04126_UnplashSelenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace _04126_UnplashSelenium;

public class RemovePhotoFromCollectionTest : BaseTest
{
    [Test]
    public async Task TestAddAndRemoveImagesFromCollection()
    {
        HomePage homePage = new HomePage(driver, wait);
        LoginPage loginPage = new LoginPage(driver, wait);

        string email = settings.Email;
        string password = settings.Password;

        // Login
        homePage.clickLogin();
        loginPage.Login(email, password);

        homePage.AddImagesToCollection(1);

        CreateCollectionModalPage createCollectionModalPage = new CreateCollectionModalPage(driver, wait);
        AddImagesToCollectionModalPage addImagesToCollectionModalPage = new AddImagesToCollectionModalPage(driver, wait);

        string collectionName = "My Test Collection " + DateTime.Now.ToString("HHmmss");
        string collectionDesc = "This is a test collection created by automation";

        addImagesToCollectionModalPage.WaitForDialog();
        if (addImagesToCollectionModalPage.HasNoCollections())
        {
            addImagesToCollectionModalPage.ClickCreateNewCollectionButton();
            createCollectionModalPage.createANewCollection(collectionName, collectionDesc);
        }

        homePage.gotoURL(settings.BaseUrl, 1000);

        // Add the 2nd image
        homePage.AddImageToCollection(1);
        homePage.RefreshPage();
        homePage.AddImageToCollection(1);
        addImagesToCollectionModalPage.WaitForDialog();
        addImagesToCollectionModalPage.AddToFirstRecentCollection();
        homePage.gotoURL(settings.BaseUrl, 1000);

        //Remove 1 image
        homePage.RemoveImageFromCollection(1);
        addImagesToCollectionModalPage.WaitForDialog();
        addImagesToCollectionModalPage.RemoveFromFirstRecentCollection();
        homePage.gotoURL(settings.BaseUrl, 1000);

        // Go to Collections page to verify just 1 image appears
        homePage.GoToCollectionsPage();
        CollectionsPage collectionPage = new CollectionsPage(driver, wait);
        Assert.That(collectionPage.verify1ImageAppears(), Is.True,
                "The text '1 image' is not displayed");

        // Delete collection by API
        await Task.Delay(2000);
        var collectionApi = new CollectionApiHelper(
            settings.UnsplashAccessToken,
            settings.UnsplashApiBaseUrl
        );

        bool deleted = await collectionApi.DeleteCollectionByTitle(
            settings.UnsplashUsername,
            collectionName
        );

        Assert.That(deleted, Is.True,
            $"Failed to delete collection '{collectionName}' via API");

        Console.WriteLine($"Successfully cleaned up test collection: {collectionName}");
    }
}
