using c__basic_SD5858_VoThiBeThi_section1.Configs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace _04126_UnplashSelenium;

public class BaseTest
{
    protected IWebDriver driver;
    protected WebDriverWait wait;
    protected TestSettings settings;

    [SetUp]
    public void Setup()
    {
        settings = TestSettings.LoadSettings();

        var options = new ChromeOptions();
        var service = ChromeDriverService.CreateDefaultService();
        driver = new ChromeDriver(service, options);
        driver.Manage().Window.Maximize();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        driver.Navigate().GoToUrl(settings.BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        if (driver != null)
        {
            driver.Dispose();
            driver.Quit();
        }
    }
}
