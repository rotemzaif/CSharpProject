using OpenQA.Selenium;
using NUnit.Framework;
using System;
using WebDriverManager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using System.IO;
using FrameworkHW2_SwagLabs.Tools;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using NUnit.Framework.Interfaces;
using System.Net.Mime;
using Allure.Commons;

namespace FrameworkHW2_SwagLabs.Tests
{
    class BaseTest
    {
        public IWebDriver Driver { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            // browser selection
            // checking if browser type is received from command line: if not then fall back from config file
            var browserName = TestContext.Parameters["browserName"] ?? Utils.appConfig["browserName"];
            switch (browserName)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    Driver = new EdgeDriver();
                    break;
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Driver = new FirefoxDriver();
                    break;
                default:
                    throw new NotSupportedException($"No such browser {browserName}");
            }            
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            Driver.Navigate().GoToUrl(Utils.appConfig["baseUrl"]);
        }

        [TearDown]
        public void TakeScreenShotOnFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                AllureLifecycle.Instance.AddAttachment("Full page screenshot", MediaTypeNames.Image.Jpeg, ((ITakesScreenshot)Driver).GetScreenshot().AsByteArray);
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {            
            Driver.Quit();
        }
    }
}
