using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models
{
    public abstract class Product
    {
        private int _price;
        private string _name;
        private string _usageDescription;
        private string _info;

        /// <summary>
        /// Constructor for Product that takes price, name, usage description and information
        /// </summary>
        /// <param name="price">The price for the product in Swedish kronor</param>
        /// <param name="name">The name of the product</param>
        /// <param name="usage">A usage description of the product.</param>
        /// <param name="info">Information about the product</param>
        public Product(int price, string name, string usage, string info)
        {
            this.Price = price;
            this.Name = name;
            this.UsageDescription = usage;
            this.Info = info;
        }

        /// <summary>
        /// Property for the Price with get and set method.
        /// Will not allow a negative price to be set.
        /// </summary>
        public int Price
        {
            get
            {
                return this._price;
            }
            set
            {
                if (value < 0)
                {
                    value *= -1;
                }
                this._price = value;
            }
        }

        /// <summary>
        /// Property for the Name with get and set method.
        /// Will not allow a null of empty Name to be set.
        /// </summary>
        public string Name
        {
            get { return this._name; }

            set
            {
                if (value != null && value != "")
                {
                    this._name = value;
                }
                else
                {
                    this._name = "Unknown name";
                }
            }
        }

        /// <summary>
        /// Property for the UsageDescription with get and set method.
        /// Will not allow a null of empty Usage description to be set.
        /// </summary>
        public string UsageDescription
        {
            get { return this._usageDescription; }
            set
            {
                if (value != null && value != "")
                {
                    this._usageDescription = value;
                }
                else
                {
                    this._usageDescription = "Unknown usage";
                }
            }
        }

        /// <summary>
        /// Property for the Information with get and set method.
        /// Will not allow a null of empty Information to be set.
        /// </summary>
        public string Info
        {
            get { return this._info; }
            set
            {
                if (value != null && value != "")
                {
                    this._info = value;
                }
                else
                {
                    this._info = "Information unknown";
                }
            }
        }


        /// <summary>
        /// Method that returns Usage and can be overrided by the Product children. 
        /// </summary>
        /// <returns>string with Usage Description</returns>
        public virtual string Use()
        {
            return UsageDescription;
        }

        /// <summary>
        /// Method that returns Name, Price and Information about the product.
        /// Can be overrided by the Product children. 
        /// </summary>
        /// <returns>string with the product's Name, Price and Information</returns>
        public virtual string Examine()
        {
            return $"{Name} \t{Price} kr. \t{Info}";
        }

    }
}
