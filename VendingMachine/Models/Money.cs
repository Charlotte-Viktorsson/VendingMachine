using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models
{
    public class Money
    {
        public int SEK1000 { get; }
        public int SEK500 { get; }
        public int SEK100 { get; }
        public int SEK50 { get; }
        public int SEK20 { get; }
        public int SEK10 { get; }
        public int SEK5 { get; }
        public int SEK1 { get; }


        public Money(int price)
        {
            SEK1000 += (price / 1000);
            price %= 1000;

            SEK500 += (price / 500);
            price %= 500;

            SEK100 += (price / 100);
            price %= 100;

            SEK50 += (price / 50);
            price %= 50;

            SEK20 += (price / 20);
            price %= 20;

            SEK10 += (price / 10);
            price %= 10;

            SEK5 += (price / 5);
            price %= 5;

            SEK1 += price;
        }

        public int GetSum()
        {
            return (SEK1000 * 1000 + SEK500 * 500 + SEK100 * 100 + SEK50 * 50 + SEK20 * 20 + SEK10 * 10 + SEK5 * 5 + SEK1);
        }


        public override string ToString()
        {
            string returnString = "";
            returnString += "\n1000: " + SEK1000;

            returnString += "\n500: " + SEK500;
            returnString += "\n100: " + SEK100;
            returnString += "\n50: " + SEK50;
            returnString += "\n20: " + SEK20;
            returnString += "\n10: " + SEK10;
            returnString += "\n5: " + SEK5;
            returnString += "\n1: " + SEK1;

            return returnString;
        }
    }
}
