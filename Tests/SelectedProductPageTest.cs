using System;
using NUnit.Framework;
using FrameworkHW2_SwagLabs.PageObjects;
using static FrameworkHW2_SwagLabs.BasePage;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;
using FrameworkHW2_SwagLabs.Tools;
using System.Threading;

namespace FrameworkHW2_SwagLabs.Tests
{
    [AllureNUnit]
    [AllureEpic("Selected Product Page")]
    [Parallelizable]
    [AllureSeverity(SeverityLevel.normal)]
    class SelectedProductPageTest:BaseTest
    {
        LoginPage lp;
        ProductsPage pp;
        SelectedProductPage spp;
        int SelectedProdIndex;
        Product SelectedProd;
        string ExpectedUrl;

        [Test, Description("move to selected product page and verify product details match the selected product details")]
        public void TC01_Check_Product_Details()
        {
            lp = new (Driver);
            lp.Login("standard_user", "secret_sauce");
            ExpectedUrl = Utils.pageElements["ProductsPage:ProductsPageURL"];
            Assert.That(Driver.Url, Is.EqualTo(ExpectedUrl), "Products page is not displayed!! cannot proceed with tests");
            pp = new (Driver);            
            // getting a random product from the list
            Random rnd = new Random();
            SelectedProdIndex = rnd.Next(0, pp.ProductsNameElList.Count);
            SelectedProd = pp.GetProduct(SelectedProdIndex);
            //var SelectedProductName = SelectedProd.GetProductName();
            Console.WriteLine(SelectedProd.ToString());
            var SelectedProductDomId = pp.GetProductDomID(SelectedProdIndex);
            ExpectedUrl = Utils.pageElements["SelectedProductPage:SelectedPageURL"] + SelectedProductDomId;
            Console.WriteLine("expected url: " + ExpectedUrl);
            pp.GoToProductPage(SelectedProd.ProductName);
            //pp.GoToProductPage(SelectedProd.GetProductName());
            Console.WriteLine("current url: " + Driver.Url);
            Assert.That(ExpectedUrl, Is.EqualTo(Driver.Url), "product '" + SelectedProd.ProductName + "' page is not displayed");
            spp = new(Driver);
            Assert.AreEqual(SelectedProd.ProductName, spp.GetProductName(), "displayed product name doesn't match the selected product name");
            //Assert.AreEqual(SelectedProd.GetProductDescription(), spp.GetProductDescription(), "displayed product description doesn't match the selected product description");
            //Assert.AreEqual(SelectedProd.GetProductPrice(), spp.GetProductPrice(), "displayed product price doesn't match the selected product price");
            //Assert.AreEqual(SelectedProd.GetProductButtonState(), spp.GetButtonSate(), "displayed product button label doesn't match the selected product button label");
        }

        [Test, Description("add prodcut from selected product page")]
        public void TC02_Add_Product()
        {
            spp = new (Driver);
            int numOfProductsInCartBeforeAdd = spp.GetNumOfProductsDisplay();
            spp.AddProduct();
            spp = new (Driver);
            Assert.AreEqual(ButtonState.REMOVE, spp.GetButtonSate(), "Button did not toggle to 'Remove' after clicking on 'Add'");
            Assert.AreEqual(numOfProductsInCartBeforeAdd + 1, spp.GetNumOfProductsDisplay(), "num of products display has not increased after adding a product from " +
                "the product page itself");
        }

        [Test, Description("check product button state in 'Products' page")]
        public void TC03_Check_AddRmvBtn_In_Products_Page()
        {
            spp = new (Driver);
            //SelectedProd.ButtonState = spp.GetButtonSate();
            spp.MoveBackToProductsPage();
            ExpectedUrl = Utils.pageElements["ProductsPage:ProductsPageURL"];
            Assert.That(Driver.Url, Is.EqualTo(ExpectedUrl), "'PRODUCTS' page is not displayed after clicking on 'BACK TO PRODUCTS' button in the selected " +
                "product page");
            pp = new (Driver);           
            Assert.AreEqual(ButtonState.REMOVE, pp.GetButtonSate(pp.ProductsAddRemoveBtnList[SelectedProdIndex]), "selected product is still displayed with 'ADD TO CART'");
        }
    }
}
