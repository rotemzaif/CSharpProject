using System;
using NUnit.Framework;
using FrameworkHW2_SwagLabs.PageObjects;
using static FrameworkHW2_SwagLabs.BasePage;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using Allure.Commons;

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

        [Test, Description("move to selected product page and verify product details match the selected product details")]
        public void TC01_Check_Product_Details()
        {
            lp = new LoginPage(driver);
            lp.Login("standard_user", "secret_sauce");
            pp = new ProductsPage(driver);
            Assert.That(pp.IsPageDisplayed(pp.ProductsLabelEl), "Login Failed with entered credentials");
            // getting a random product from the list
            Random rnd = new Random();
            SelectedProdIndex = rnd.Next(0, pp.ProductsNameElList.Count);
            SelectedProd = pp.GetProuct(SelectedProdIndex);
            Console.WriteLine(SelectedProd.ToString());
            pp.GoToProductPage(SelectedProd.name);
            spp = new SelectedProductPage(driver);
            Assert.That(pp.IsPageDisplayed(spp.BackBtn), "Did not reach the product page!!");
            Assert.AreEqual(SelectedProd.name, spp.GetProductName(), "displayed product name doesn't match the selected product name");
            Assert.AreEqual(SelectedProd.description, spp.GetProductDescription(), "displayed product description doesn't match the selected product description");
            Assert.AreEqual(SelectedProd.price, spp.GetProductPrice(), "displayed product price doesn't match the selected product price");
            Assert.AreEqual(SelectedProd.buttonState, spp.GetButtonSate(), "displayed product button label doesn't match the selected product button label");
        }

        [Test, Description("add prodcut from selected product page")]
        public void TC02_Add_Product()
        {
            spp = new SelectedProductPage(driver);
            int numOfProductsInCartBeforeAdd = spp.GetNumOfProductsDisplay();
            spp.AddProduct();
            spp = new SelectedProductPage(driver);
            Assert.AreEqual(ButtonState.REMOVE, spp.GetButtonSate(), "Button did not toggle to 'Remove' after clicking on 'Add'");
            Assert.AreEqual(numOfProductsInCartBeforeAdd + 1, spp.GetNumOfProductsDisplay(), "num of products display has not increased after adding a product from " +
                "the product page itself");
        }

        [Test, Description("check product button state in 'Products' page")]
        public void TC03_Check_AddRmvBtn_In_Products_Page()
        {
            spp = new SelectedProductPage(driver);
            SelectedProd.buttonState = spp.GetButtonSate();
            spp.MoveBackToProductsPage();
            pp = new ProductsPage(driver);
            Assert.That(pp.IsPageDisplayed(pp.ProductsLabelEl), "'PRODUCTS' page is not displayed after clicking on 'BACK TO PRODUCTS' button in the selected " +
                "product page");
            //Assert.AreEqual(ButtonState.REMOVE, SelectedProd.buttonState, "selected product is still displayed with 'ADD TO CART'");
            Assert.AreEqual(ButtonState.REMOVE, pp.GetButtonSate(pp.ProductsAddRemoveBtnList[SelectedProdIndex]), "selected product is still displayed with 'ADD TO CART'");
        }
    }
}
