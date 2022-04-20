using OpenQA.Selenium;
using FrameworkHW2_SwagLabs.Tools;
using NUnit.Allure.Attributes;

namespace FrameworkHW2_SwagLabs.PageObjects
{
    class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            WaitForElementToBeVisible(Utils.pageElements["loginPage:elements_css:LoginBtn"]);
        }

        // page properties
        public IWebElement UsernameEl => driver.FindElement(By.CssSelector(Utils.pageElements["loginPage:elements_css:user_name"]));
        public IWebElement PasswordEl => driver.FindElement(By.CssSelector(Utils.pageElements["loginPage:elements_css:pswd"]));
        public IWebElement LoginBtn => driver.FindElement(By.CssSelector(Utils.pageElements["loginPage:elements_css:LoginBtn"]));
        public IWebElement ErrMsgLabel => driver.FindElement(By.CssSelector(Utils.pageElements["loginPage:elements_css:ErrMsgLabel"]));

        // page methods
        [AllureStep("Login with user: {0} and pswd: {1}")]
        public void Login(string userName, string pswd)
        {
            FillText(UsernameEl, userName);
            FillText(PasswordEl, pswd);
            Click(LoginBtn);
        }

        [AllureStep("Getting error message in case of invalid credentials")]
        public string GetErrMsgText()
        {
            return GetElementText(ErrMsgLabel);
        }

        // page validations

        [AllureStep("Checking if error message is displayed in case of invalid credentials")]
        public bool IsErrMsgDisplayed()
        {
            return ErrMsgLabel.Displayed;
        }

    }
}
