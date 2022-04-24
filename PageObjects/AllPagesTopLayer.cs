using FrameworkHW2_SwagLabs.Tools;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkHW2_SwagLabs.PageObjects
{
    class AllPagesTopLayer : BasePage
    {
        public AllPagesTopLayer(IWebDriver driver) : base(driver){}

        // elements
        public IWebElement OptionsMenuBtn => driver.FindElement(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:OptionsMenuBtn"]));
        public IWebElement OptionsMenuContainerEl => driver.FindElement(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:OptionsMenuContainerEl"]));
        public IList<IWebElement> OptionsElList => driver.FindElements(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:OptionsElList"]));
        public IWebElement ShopingCartNumEl => driver.FindElement(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:ShopingCartNumEl"]));
        public IWebElement ShopingCartLink => driver.FindElement(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:ShopingCartLink"]));
        public IWebElement LogoutOption => driver.FindElement(By.CssSelector(Utils.pageElements["AllPagesTopLayer:elements_css:LogoutOption"]));


        // action methods
        [AllureStep("opening the options menu")]
        public void OpenOptionsMenu()
        {
            Click(OptionsMenuBtn);
            WaitForElementToBeVisible(Utils.pageElements["AllPagesTopLayer:elements_css:LogoutOption"]);
            //WaitForElementToBeVisible(".bm-menu-wrap");
        }

        [AllureStep("selecting option {0} from the menu list")]
        public void SelectAction(MenuOption optionIn)
        {
            string optionStr = "";
            switch (optionIn)
            {
                case MenuOption.ALL_ITEMS:
                    optionStr = "ALL ITEMS";
                    break;
                case MenuOption.ABOUT:
                    optionStr = "ABOUT";
                    break;
                case MenuOption.LOGOUT:
                    optionStr = "LOGOUT";
                    break;
                case MenuOption.RESET_APP_STATE:
                    optionStr = "RESET APP STATE";
                    break;
            }
            foreach(IWebElement op in OptionsElList)
            {
                if (GetElementText(op).Equals(optionStr))
                {
                    Click(op);
                    break;
                }
            }
        }

        // getter methods

        [AllureStep("extracting the number displayed inside the products cart")]
        public int GetNumOfProductsDisplay()
        {
            string ShopingCartNumElCssSelector = Utils.pageElements["AllPagesTopLayer:elements_css:ShopingCartNumEl"];
            if (driver.FindElements(By.CssSelector(ShopingCartNumElCssSelector)).Count > 0)
                return Int32.Parse(GetElementText(ShopingCartNumEl));
            else return 0;
        }

        // validation methods


        // enums
        public enum MenuOption
        {
            ALL_ITEMS,
            ABOUT,
            LOGOUT,
            RESET_APP_STATE
        }

    }
}
