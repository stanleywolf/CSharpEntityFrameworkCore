using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystemInitiallizer
{
    public class CreditCardInitializer
    {
        public static CreditCard[] GetCreditCards()
        {
            CreditCard[] creditCards = new CreditCard[]
            {
                new CreditCard(){Limit = 2500,MoneyOwned = 5200,ExpirationDate = DateTime.Now.AddMonths(-5)},
                new CreditCard(){Limit = 4500,MoneyOwned = 4569,ExpirationDate = DateTime.Now.AddMonths(-1)},
                new CreditCard(){Limit = 2366,MoneyOwned = 369,ExpirationDate = DateTime.Now.AddMonths(-7)},
                new CreditCard(){Limit = 1520,MoneyOwned = 7822,ExpirationDate = DateTime.Now.AddMonths(-12)}
            };
            return creditCards;
        }
    }
}
