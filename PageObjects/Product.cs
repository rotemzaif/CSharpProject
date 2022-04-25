using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FrameworkHW2_SwagLabs.BasePage;

namespace FrameworkHW2_SwagLabs.PageObjects
{
    class Product
    {
        // properties
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public ButtonState AddRemoveButtonState { get; set; }

        // constructor
        public Product(IWebElement ProductName, IWebElement ProductDescription, IWebElement ProductPrice, IWebElement AddRemoveButton)
        {
            this.ProductName = ProductName.Text;
            this.ProductDescription = ProductDescription.Text;
            this.ProductPrice = ProductPrice.Text;
            if (AddRemoveButton.GetAttribute("id").Contains("add-to-cart"))
                this.AddRemoveButtonState = ButtonState.ADD;
            else if (AddRemoveButton.GetAttribute("id").Contains("remove"))
                this.AddRemoveButtonState = ButtonState.REMOVE;
        }

        // getters
        //public ButtonState GetProductButtonState()
        //{
        //    if (AddRemoveButton.GetAttribute("id").Contains("add-to-cart"))
        //        return ButtonState.ADD;
        //    else if(AddRemoveButton.GetAttribute("id").Contains("remove"))
        //        return ButtonState.REMOVE;
        //    else return ButtonState.NONE;
        //}

        public override string ToString()
        {
            return "Product[ \n" +
                "name: " + ProductName + 
                "\ndescription: " + ProductDescription + 
                "\nprice: " + ProductPrice + 
                "\nbutton state: " + AddRemoveButtonState; 
        }
    }
}
