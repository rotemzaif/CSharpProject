using FrameworkHW2_SwagLabs.Tools;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkHW2_SwagLabs.PageObjects
{
    class ProductsPage : AllPagesTopLayer
    {
        // page properties
        public IWebElement ProductsLabelEl => driver.FindElement(By.CssSelector(Utils.pageElements["ProductsPage:elements_css:ProductsLabelEl"]));
        public IList<IWebElement> ProductsNameElList => driver.FindElements(By.CssSelector(Utils.pageElements["ProductsPage:elements_css:ProductsNameElList"]));
        public IList<IWebElement> ProductsDescriptionElList => driver.FindElements(By.CssSelector(Utils.pageElements["ProductsPage:elements_css:ProductsDescriptionElList"]));
        public IList<IWebElement> ProductsPriceElList => driver.FindElements(By.CssSelector(Utils.pageElements["ProductsPage:elements_css:ProductsPriceElList"]));
        public IList<IWebElement> ProductsAddRemoveBtnList => driver.FindElements(By.CssSelector(Utils.pageElements["ProductsPage:elements_css:ProductsAddRemoveBtnList"]));

        public Dictionary<string, List<IWebElement>> ProductsDict { get; set; }

        public ProductsPage(IWebDriver driver) : base(driver)
        {
            InitProductsDict();
        }
        
        // page methods
        [AllureStep("Adding product: {0}")]
        public void AddProduct(string productName)
        {
            IWebElement prodAddRmvBtn;
            prodAddRmvBtn = ProductsDict[productName][3];
            if(GetButtonSate(prodAddRmvBtn) == ButtonState.ADD) 
                Click(prodAddRmvBtn);
            else
                Console.WriteLine("product '" + productName + "' was added already");
        }

        [AllureStep("Removing product: {0}")]
        public void RemoveProduct(string productName)
        {
            IWebElement prodAddRmvBtn;
            prodAddRmvBtn = ProductsDict[productName][3];
            if (GetButtonSate(prodAddRmvBtn) == ButtonState.REMOVE)
                Click(prodAddRmvBtn);
            else
                Console.WriteLine("product '" + productName + "' was not added");
        }

        [AllureStep("Clicking on product page link: {0}")]
        public void GoToProductPage(string productName)
        {
            IWebElement productLink = ProductsDict[productName][0];
            Click(productLink);
        }

        // getter methods
        [AllureStep("Getting product Add/Remove button {0} state in Products page")]
        public ButtonState GetButtonSate(IWebElement btn)
        {
            ButtonState state = ButtonState.NONE;
            if (btn.GetAttribute("id").Contains("add-to-cart"))
                state = ButtonState.ADD;
            else if (btn.GetAttribute("id").Contains("remove"))
                state = ButtonState.REMOVE;
            return state;
        }

        [AllureStep("Getting Product object based on products element list at index {0}")]
        public Product GetProuct(int index)
        {
            var prodName = GetElementText(ProductsNameElList[index]);
            var prodDesc = GetElementText(ProductsDescriptionElList[index]);
            var prodPrice = GetElementText(ProductsPriceElList[index]);
            var prodBtnState = GetButtonSate(ProductsAddRemoveBtnList[index]);
            return new Product(prodName, prodDesc, prodPrice, prodBtnState);
        }

        // validations        

        // assistance methods
        public void InitProductsDict()
        {
            string prodId;
            if (ProductsDict == null)
                ProductsDict = new Dictionary<string, List<IWebElement>>();
            IWebElement prodName,prodDesc, prodPrice, prodAddRmvBtn;
            for(int i = 0; i < ProductsNameElList.Count; i++)
            {
                prodId = GetElementText(ProductsNameElList[i]);
                prodName = ProductsNameElList[i];
                prodDesc = ProductsDescriptionElList[i];
                prodPrice = ProductsPriceElList[i];
                prodAddRmvBtn = ProductsAddRemoveBtnList[i];
                List<IWebElement> prodAtt = new List<IWebElement>() { prodName, prodDesc, prodPrice, prodAddRmvBtn };
                ProductsDict.Add(prodId, prodAtt);
            }
        }


    }
}
