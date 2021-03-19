using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models
{
    public class Chocolate : Product
    {
        /// <summary>
        /// Constructor for Chocolate demands the price, a name and information.
        /// The parameters will be set in base product together with a Usage Description
        /// </summary>
        /// <param name="price">The price of the product in kronor</param>
        /// <param name="name">The name of the product</param>
        /// <param name="info">Information of the product, for example weight</param>
        public Chocolate(int price, string name, string info) : base(price, name, "Eat it and enjoy!", info)
        {

        }

        /// <summary>
        /// Method overriding the base method with check of Name length.
        /// </summary>
        /// <returns>string with Name, price and info about the product.</returns>
        public override string Examine()
        {
            string returnMsg = "";
            returnMsg += Name;
            if (returnMsg.Length < 21) //should probably check more lengths, but this will do with the original products
            {
                returnMsg += "\t";
            }
            returnMsg += $"{Price} kr. \t{Info}";

            return returnMsg;
        }

    }
}
