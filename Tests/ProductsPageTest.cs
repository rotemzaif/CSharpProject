using FrameworkHW2_SwagLabs.PageObjects;
using OpenQA.Selenium;
using System;
using NUnit.Framework;
using static FrameworkHW2_SwagLabs.BasePage;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;
using FrameworkHW2_SwagLabs.Tools;

namespace FrameworkHW2_SwagLabs.Tests
{    
    [AllureNUnit]
    [AllureEpic("Products Page")]
    [Parallelizable]
    class ProductsPageTest:BaseTest
    {
        ProductsPage pp;        
        string ExpectedUrl;
        int SelectedProdIndex;
        Product SelectedProd;

        [Test, Description("add a product to cart and check ")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Add Product")]
        public void TC01_Add_Products_To_Cart()
        {
            LoginPage lp = new LoginPage(Driver);
            lp.Login("standard_user", "secret_sauce");
            ExpectedUrl = Utils.pageElements["ProductsPage:ProductsPageURL"];
            Assert.That(Driver.Url, Is.EqualTo(ExpectedUrl), "Products page is not displayed!! cannot proceed with tests");
            pp = new(Driver);
            // selecting randomly one of the products from the list
            Random rnd = new Random();
            SelectedProdIndex = rnd.Next(0, pp.ProductsNameElList.Count);
            SelectedProd = pp.GetProduct(SelectedProdIndex);
            Console.WriteLine(SelectedProd.ToString());
            Console.WriteLine("going to extract number of products in cart before adding a product");
            int numOfProductsInCartBeforeAdd = pp.GetNumOfProductsDisplay();
            pp.AddProduct(SelectedProd, SelectedProdIndex);
            pp = new (Driver);
            IWebElement prodBtn = pp.ProductsDict[SelectedProd.ProductName][3];
            Assert.AreEqual(ButtonState.REMOVE, pp.GetButtonSate(prodBtn), "Button did not toggle to 'Remove' after adding a product");
            Assert.AreEqual(pp.GetNumOfProductsDisplay(), numOfProductsInCartBeforeAdd + 1, "shoping cart products num display is not updated");
        }

        [Test, Description("remove a product from the cart")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Remove Product")]
        public void tc02_Remove_Product_From_Cart()
        {
            pp = new (Driver);
            int numOfProductsInCartBeforeRemove = pp.GetNumOfProductsDisplay();
            if(numOfProductsInCartBeforeRemove == 0)
                Console.WriteLine("no products were added!! Cannot remove any product");
            else
            {
                pp.RemoveProduct(SelectedProd.ProductName);
                pp = new (Driver);
                IWebElement prodBtn = pp.ProductsDict[SelectedProd.ProductName][3];
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
            pp = new ProductsPage(Driver);
            pp.GoToProductPage(SelectedProd.ProductName);
            SelectedProductPage spp = new SelectedProductPage(Driver);
            Assert.That(spp.IsPageDisplayed(spp.BackBtn));
        }

    }
}
