using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Models;

namespace VendingMachine.Data
{
    public class VendingController : IVending
    {

        readonly static List<int> Denominations = new List<int> { 1000, 500, 100, 50, 20, 10, 5, 1 };
        private static Dictionary<int, int> MoneyPool = new Dictionary<int, int>();
        private int _paymentSum = 0;
        private List<Product> products = new List<Product>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="addProducts">If set to true, a bunch of products will be added</param>
        public VendingController(bool addProducts)
        {
            if (addProducts)
            {
                AddAvailableProductTypes();
            }
        }

        /// <summary>
        /// Property for how much money the customer has inserted.
        /// </summary>
        public int Payment
        {
            get
            {
                return _paymentSum;
            }
            private set
            {
                if (value > 0)
                {
                    _paymentSum = value;
                }
                else //wont set it to minus, but this is already prevented from happening in the calling methods
                {
                    _paymentSum = 0;
                }
            }
        }

        /// <summary>
        /// Method for adding a bunch of products. Called from constructor if addProduct is set to true
        /// </summary>
        private void AddAvailableProductTypes()
        {
            //addProducts to VendingMachine
            products.Add(new Chocolate(25, "Marabou Mjölkchoklad", "200 g"));
            products.Add(new Chocolate(15, "Marabou Schweizernöt", "100 g"));
            products.Add(new Chocolate(15, "Marabou Helnöt", "100 g"));
            products.Add(new PopCorn(26, "Estrella", "500 g"));
            products.Add(new Soda(15, "CocaCola", "0,5 l"));
            products.Add(new Soda(15, "Fanta", "0,5 l"));
            products.Add(new Soda(15, "Sprite", "0,5 l"));
            products.Add(new Soda(10, "Water", "0,5 l"));
        }

        /// <summary>
        /// Method that returns a string with all available products examined
        /// </summary>
        /// <returns>string with available products</returns>
        public string ShowAll()
        {
            string returnMsg = "";
            for (int i = 0; i < products.Count; i++)
            {
                returnMsg += "\nChoice " + i + " : ";
                returnMsg += products[i].Examine();
            }

            return returnMsg;
        }


        /// <summary>
        /// Method that inserts money if they are of the correct Denomination
        /// </summary>
        /// <param name="amount">The inserted amount</param>
        /// <returns>Returns true if the money was of correct denomination</returns>
        public bool InsertMoney(int amount)
        {
            bool accepted = false;

            //only amount in Denominations are accepted
            if (Denominations.Contains(amount))
            {
                Payment += amount;
                accepted = true;
            }
            return accepted;
        }


        /// <summary>
        /// This returns a string with the available balance divided in the different denominations
        /// </summary>
        /// <returns>Returns a string representing the balance with nr of each denominations</returns>
        private string ShowMoneyChange()
        {
            int change = Payment;
            string returnMsg = "";
            foreach (int den in Denominations)
            {
                MoneyPool[den] = (change / den); // get nr of whole denominations (each for each)
                change %= den;               // continue with the rest (modulo)
                returnMsg += "\n" + den + " kr: " + MoneyPool[den];
            }
            return returnMsg;
        }

        /// <summary>
        /// Method is called when the Customer is finished. The Change will be presented.
        /// </summary>
        /// <returns>Returns a string with the change.</returns>
        public string EndTransaction()
        {
            string returnMsg = "Thank you for using the Vending machine!";
            returnMsg += "\nHere is your change:";

            returnMsg += ShowMoneyChange();

            return returnMsg;
        }

        /// <summary>
        /// Method lets the customer purchase if the product exist and there is enough money.
        /// Errormessages will be returned if product is not found or if it is too expensive.
        /// </summary>
        /// <param name="choiceIndex">The index of the wanted product</param>
        /// <returns>Return the product use or errormessage</returns>
        public string Purchase(int choiceIndex)
        {
            string returnString = "Product not found";

            //get productType in question
            if (choiceIndex < products.Count)
            {
                Product wantedProduct = products[choiceIndex];
                if (wantedProduct != null)
                {
                    if (wantedProduct.Price <= Payment)
                    {
                        returnString = "Here is your product: " + wantedProduct.Name + "\n";
                        returnString += wantedProduct.Use();
                        Payment -= wantedProduct.Price;
                    }
                    else
                    {
                        returnString = "More money needed for this product";
                    }
                }
            }
            return returnString;
        }

    }
}
