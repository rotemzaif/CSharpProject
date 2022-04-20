using FrameworkHW2_SwagLabs.Tools;
using OpenQA.Selenium;
using System;
using NUnit.Allure.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkHW2_SwagLabs.PageObjects
{
    class SelectedProductPage : AllPagesTopLayer
    {
        public SelectedProductPage(IWebDriver driver) : base(driver) {}

        // page properties
        public IWebElement BackBtn => driver.FindElement(By.CssSelector(Utils.pageElements["SelectedProductPage:elements_css:BackBtn"]));
        public IWebElement ProdName => driver.FindElement(By.CssSelector(Utils.pageElements["SelectedProductPage:elements_css:ProdName"]));
        public IWebElement ProdDesc => driver.FindElement(By.CssSelector(Utils.pageElements["SelectedProductPage:elements_css:ProdDesc"]));
        public IWebElement ProdPrice => driver.FindElement(By.CssSelector(Utils.pageElements["SelectedProductPage:elements_css:ProdPrice"]));
        public IWebElement ProdAddRmvBtn => driver.FindElement(By.CssSelector(Utils.pageElements["SelectedProductPage:elements_css:ProdAddRmvBtn"]));

        // getters
        [AllureStep("Getting product name displayed in the selected Product page")]
        public string GetProductName()
        {
            return GetElementText(ProdName);
        }

        [AllureStep("Getting product description displayed in the selected Product page")]
        public string GetProductDescription()
        {
            return GetElementText(ProdDesc);
        }

        [AllureStep("Getting product price displayed in the selected Product page")]
        public string GetProductPrice()
        {
            return GetElementText(ProdPrice);
        }

        [AllureStep("Getting product Add/Remove button state displayed in the selected Product page")]
        public ButtonState GetButtonSate()
        {
            ButtonState state = ButtonState.NONE;
            if (ProdAddRmvBtn.GetAttribute("id").Contains("add-to-cart"))
                state = ButtonState.ADD;
            else if (ProdAddRmvBtn.GetAttribute("id").Contains("remove"))
                state = ButtonState.REMOVE;
            return state;
        }

        // page methods

        [AllureStep("Adding the product from the product page itself")]
        public void AddProduct()
        {
            if (GetButtonSate() == ButtonState.ADD)
                Click(ProdAddRmvBtn);
            else
                Console.WriteLine("product '" + GetProductName() + "' was added already");
        }

        [AllureStep("Removing the product from the product page itself")]
        public void RemoveProduct()
        {
            if (GetButtonSate() == ButtonState.REMOVE)
                Click(ProdAddRmvBtn);
            else
                Console.WriteLine("product '" + GetProductName() + "' was not added");
        }

        [AllureStep("Clicking on the 'BACK TO PRODUCTS' button from the selected product page")]
        public void MoveBackToProductsPage()
        {
            Click(BackBtn);
        }
    }
}
