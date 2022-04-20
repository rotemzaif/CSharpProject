using FrameworkHW2_SwagLabs.PageObjects;
using OpenQA.Selenium;
using System;
using NUnit.Framework;
using static FrameworkHW2_SwagLabs.BasePage;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;

namespace FrameworkHW2_SwagLabs.Tests
{
    [AllureNUnit]
    [AllureEpic("Products Page")]
    [Parallelizable]
    class ProductsPageTest:BaseTest
    {
        ProductsPage pp;
        string prodName = "";

        [Test, Description("add a product to cart and check ")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Add Product")]
        public void TC01_Add_Products_To_Cart()
        {
            LoginPage lp = new LoginPage(driver);
            lp.Login("standard_user", "secret_sauce");
            pp = new ProductsPage(driver);            
            prodName = "Sauce Labs Backpack";
            int numOfProductsInCartBeforeAdd = pp.GetNumOfProductsDisplay();
            pp.AddProduct(prodName);
            pp = new ProductsPage(driver);
            IWebElement prodBtn = pp.ProductsDict[prodName][3];            
            Assert.AreEqual(ButtonState.REMOVE, pp.GetButtonSate(prodBtn),"Button did not toggle to 'Remove' after adding a product");
            Assert.AreEqual(pp.GetNumOfProductsDisplay(), numOfProductsInCartBeforeAdd + 1,"shoping cart products num display is not updated");
        }

        [Test, Description("remove a product from the cart")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Remove Product")]
        public void tc02_Remove_Product_From_Cart()
        {
            pp = new ProductsPage(driver);
            int numOfProductsInCartBeforeRemove = pp.GetNumOfProductsDisplay();
            if(numOfProductsInCartBeforeRemove == 0)
                Console.WriteLine("no products were added!! Cannot remove any product");
            else
            {
                pp.RemoveProduct(prodName);
                pp = new ProductsPage(driver);
                IWebElement prodBtn = pp.ProductsDict[prodName][3];
                Assert.AreEqual(ButtonState.ADD, pp.GetButtonSate(prodBtn), "Button did not toggle to 'ADD TO CART' after adding a product");
                Assert.AreEqual(pp.GetNumOfProductsDisplay(), numOfProductsInCartBeforeRemove - 1, 
                    "shoping cart products num display is not updated");
            }            
        }

        [Test, Description("move to 'Products' page from selected product page")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("View Product")]
        public void TC03_Move_To_Product_Page()
        {
            pp = new ProductsPage(driver);
            pp.GoToProductPage(prodName);
            SelectedProductPage spp = new SelectedProductPage(driver);
            Assert.That(spp.IsPageDisplayed(spp.BackBtn));
        }

    }
}
