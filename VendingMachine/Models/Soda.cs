using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models
{
    public class Soda : Product
    {
        /// <summary>
        /// Constructor for Soda demands the price, a name and information.
        /// The parameters will be set in base product together with a Usage Description
        /// </summary>
        /// <param name="price">The price of the product in kronor</param>
        /// <param name="name">The name of the product</param>
        /// <param name="info">Information of the product, for example amount</param>
        public Soda(int price, string name, string info) : base(price, name, "Drink it and have a good time!", info)
        {
        }


        /// <summary>
        /// Method overriding the base method
        /// </summary>
        /// <returns>string with Name, price and info about the product.</returns>
        public override string Examine()
        {
            return $"{Name} \t\t{Price} kr. \t{Info}";
        }
    }
}

