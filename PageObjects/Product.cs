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
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public ButtonState buttonState { get; set; }

        // constructor
        public Product(string name, string description, string price, ButtonState buttonState)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.buttonState = buttonState;
        }

        public override string ToString()
        {
            return "Product[ \n" +
                "name: " + name + 
                "\ndescription: " + description + 
                "\nprice: " + price + 
                "\nbutton state: " + buttonState; 
        }
    }
}
