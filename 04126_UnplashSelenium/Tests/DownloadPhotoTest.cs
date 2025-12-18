using _04126_UnplashSelenium.Pages;

namespace _04126_UnplashSelenium;

using System.IO;

public class DownloadPhotoTest : BaseTest
{
    [Test]
    public void TestDownloadImageSuccessfully()
    {
        HomePage homePage = new HomePage(driver, wait);
        LoginPage loginPage = new LoginPage(driver, wait);

        string email = settings.Email;
        string password = settings.Password;

        // Login
        homePage.clickLogin();
        loginPage.Login(email, password);

        var filesBefore = Directory.GetFiles(settings.DownloadFolder);

        homePage.ClickRandomImage();
        PhotoDetailPage photoDetailPage = new PhotoDetailPage(driver, wait);
        photoDetailPage.ClickDownload();
        Thread.Sleep(5000); // Wait for download file

        // Verify downloaded file
        var filesAfter = Directory.GetFiles(settings.DownloadFolder);
        var newFile = filesAfter.Except(filesBefore).FirstOrDefault();
        Assert.That(newFile, Is.Not.Null, "No file was downloaded");

        FileInfo fileInfo = new FileInfo(newFile);
        Assert.That(fileInfo.Length, Is.GreaterThan(0), "Downloaded file is empty");
        TestContext.WriteLine($"Downloaded file: {fileInfo.Name}");
    }
}
