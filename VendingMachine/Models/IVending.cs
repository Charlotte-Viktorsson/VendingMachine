using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Models
{
    interface IVending
    {
        public string Purchase(int choiceIndex);

        public string ShowAll();

        public bool InsertMoney(int amount);

        public string EndTransaction();

    }
}
