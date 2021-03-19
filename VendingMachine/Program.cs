using System;
using VendingMachine.Data;

namespace VendingMachine
{
    class Program
    {
        /// <summary>
        /// Main method with a loop for menu choices
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            VendingController vm = new VendingController(true);

            Console.WriteLine("Welcome to Vending Machine!");
            bool finished = false;
            while (!finished)
            {
                ShowMenu();

                Console.WriteLine("Your current balance is " + vm.Payment + " kr.");
                Console.Write("My Choice: ");
                char choice = Console.ReadKey(true).KeyChar;
                Console.WriteLine("\n--------------------");

                switch (choice)
                {
                    /*case '0':
                        //menu automatically shows... default will do
                        break;*/
                    case '1':
                        Console.WriteLine(vm.ShowAll());
                        break;
                    case '2':
                        InsertCash(vm);
                        break;

                    case '3':
                        Buy(vm);
                        break;

                    case '4':
                        Console.WriteLine(vm.EndTransaction());
                        finished = true;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Method shows the menu
        /// </summary>
        private static void ShowMenu()
        {
            Console.WriteLine("Please make a choice: ");
            Console.WriteLine("--------------------");

            Console.WriteLine("0 Menu");
            Console.WriteLine("1 See all products");
            Console.WriteLine("2 Insert Money");
            Console.WriteLine("3 Buy product");
            Console.WriteLine("4 Finish");
        }


        /// <summary>
        /// method inserts money and add it to the VendingController.
        /// If the amount is not allowed, there will be messages shown
        /// </summary>
        /// <param name="vm">the VendingController instance</param>
        private static void InsertCash(VendingController vm)
        {
            Console.WriteLine("Money allowed SEK: 1000, 500, 100, 50, 20, 10, 5, 1");
            int amount = 0;
            Console.WriteLine("Amount to insert: ");
            var parseOk = int.TryParse(Console.ReadLine(), out amount);
            if (parseOk)
            {
                bool allowedMoney = vm.InsertMoney(amount);
                if (allowedMoney == false)
                {
                    Console.WriteLine("Try again, only Swedish money of" +
                        " the denominations 1000, 500, 100, 50, 20, 10, 5, 1 are allowed.");
                }
            }
            else
            {
                Console.WriteLine("Try again, only allowed 0-9");
            }

            Console.WriteLine("Your balance is: " + vm.Payment + " kr.\n");
        }

        /// <summary>
        /// method purchases products from VendingController.
        /// If there is a problem with the purchase, there will be messages shown
        /// Otherwise the product and it usage will be shown
        /// </summary>
        /// <param name="vm">the VendingController instance</param>
        private static void Buy(VendingController vm)
        {
            Console.WriteLine(vm.ShowAll());
            Console.WriteLine("Choose product to buy: ");
            int wantedProduct = -1;
            var parseOk = int.TryParse(Console.ReadLine(), out wantedProduct);
            if (parseOk)
            {
                Console.WriteLine(vm.Purchase(wantedProduct));
            }
            else
            {
                Console.WriteLine("Problem with choice, try again!");
            }

            Console.WriteLine("Your balance is: " + vm.Payment + " kr.\n");
        }
    }
}
