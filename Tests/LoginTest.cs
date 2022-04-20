using FrameworkHW2_SwagLabs.PageObjects;
using NUnit.Framework;
using System;
using FrameworkHW2_SwagLabs.Tools;
using NUnit.Allure.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FrameworkHW2_SwagLabs.PageObjects.AllPagesTopLayer;
using NUnit.Allure.Attributes;
using Allure.Commons;
using System.Net.Mime;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace FrameworkHW2_SwagLabs.Tests
{
    [AllureNUnit]
    [AllureEpic("Login Page")]
    [Parallelizable]
    class LoginTest:BaseTest
    {
        LoginPage lp;
        ProductsPage pp;

        [Test, Description("login to the app with valid credentials")]
        [AllureSeverity(SeverityLevel.blocker)]
        [AllureFeature("login")]
        public void TC01_Login_With_Valid_Credentials()
        {
            lp = new LoginPage(driver);
            //lp.Login("standard_user", "secret_sauceee");
            lp.Login("standard_user", "secret_sauce");
            //AllureLifecycle.Instance.AddAttachment("Full page screenshot", MediaTypeNames.Image.Jpeg, ((ITakesScreenshot)driver).GetScreenshot().AsByteArray);

            pp = new ProductsPage(driver);
            Assert.That(pp.IsPageDisplayed(pp.ProductsLabelEl), "Login Failed with entered credentials");
            

        }

        [Test, Description("ordered logout from the app - via logout button")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("logout")]
        public void TC02_Logout()
        {
            pp = new ProductsPage(driver);
            pp.OpenOptionsMenu();
            pp.SelectAction(MenuOption.LOGOUT);
            lp = new LoginPage(driver);
            Assert.That(lp.IsPageDisplayed(lp.LoginBtn));
        }

        [Test, Description("app login with invalid credentials and get an error message")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("login")]
        public void TC03_Login_With_Invalid_Credentials()
        {
            LoginPage lp = new LoginPage(driver);
            lp.Login("rotem", "123456789");
            Assert.That(lp.IsErrMsgDisplayed(), "Error message is not displayed when entering invalid credentials");
            //string expectedErrMsg = Utils.pageElements["loginPage:labels_text:invalid_credentials_err_msg"];
            //Console.WriteLine("actual err msg: " + lp.GetErrMsgText());
            //Assert.That(lp.GetErrMsgText(), Is.EqualTo(expectedErrMsg));
        }

        [Test, Description("check error message content")]
        [AllureSeverity(SeverityLevel.minor)]
        [AllureFeature("login")]
        public void TC04_check_error_message()
        {
            LoginPage lp = new LoginPage(driver);
            string expectedErrMsg = Utils.pageElements["loginPage:labels_text:invalid_credentials_err_msg"];
            Assert.That(lp.GetErrMsgText(), Is.EqualTo(expectedErrMsg));
        }


    }
}
