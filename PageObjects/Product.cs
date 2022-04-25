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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public ButtonState ButtonState { get; set; }

        // constructor
        public Product(string name, string description, string price, ButtonState buttonState)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.ButtonState = buttonState;
        }

        public override string ToString()
        {
            return "Product[ \n" +
                "name: " + Name + 
                "\ndescription: " + Description + 
                "\nprice: " + Price + 
                "\nbutton state: " + ButtonState; 
        }
    }
}
