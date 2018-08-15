using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystemInitiallizer;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
       public  static void Main(string[] args)
       {
           using (BillsPaymentSystemContext context = new BillsPaymentSystemContext())
           {
               //context.Database.EnsureDeleted();
               //context.Database.EnsureCreated();
                //Initializer.Seed(context);

               User user = GetUser(context);

               GetInfo(user);

               PayBills(user, 5000);
           }
       }

        private static void PayBills(User user, decimal amount)
        {
            var bankAccountTotals =
                user.PaymentMethods.Where(x => x.BankAccount != null).Sum(x => x.BankAccount.Balance);
            var creditCardTotals =
                user.PaymentMethods.Where(x => x.CreditCard != null).Sum(x => x.CreditCard.LimitLeft);

            var totals = bankAccountTotals + creditCardTotals;

            if (totals >= amount)
            {
                var bankAccounts = user.PaymentMethods.Where(x => x.BankAccount != null).Select(x => x.BankAccount)
                    .OrderBy(x => x.BankAccountId).ToArray();

                foreach (var bankAccount in bankAccounts)
                {
                    if (bankAccount.Balance >= amount)
                    {
                        bankAccount.Withdrow(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= bankAccount.Balance;
                        bankAccount.Withdrow(bankAccount.Balance);
                    }
                    if (amount == 0)
                    {
                        return;
                    }
                }
                var creditCards = user.PaymentMethods.Where(x => x.CreditCard != null).Select(x => x.CreditCard)
                    .OrderBy(x => x.CreditCardId).ToArray();

                foreach (var creditCard in creditCards)
                {
                    if (creditCard.LimitLeft >= 0)
                    {
                        creditCard.Withdrow(amount);
                        amount = 0;
                    }
                    else
                    {
                        amount -= creditCard.LimitLeft;
                        creditCard.Withdrow(creditCard.LimitLeft);
                    }
                    if (amount == 0)
                    {
                        return;
                    }
                }
            }

            else
            {
                Console.WriteLine("No moneyLeft");
            }
        }

        private static void GetInfo(User user)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Bank Accounts:");

            var bankAccounts = user.PaymentMethods.Where(x => x.BankAccount != null).Select(x => x.BankAccount)
                .ToArray();
            foreach (var bankAccount in bankAccounts)
            {
                Console.WriteLine($"--ID: {bankAccount.BankAccountId}");
                Console.WriteLine($"---Balance: {bankAccount.Balance:f2}");
                Console.WriteLine($"---Bank: {bankAccount.BankName}");
                Console.WriteLine($"---SWIFT: {bankAccount.SwiftCode}");
            }
            Console.WriteLine($"Credit Cards:");
            var creditCards = user.PaymentMethods.Where(x => x.CreditCard != null).Select(x => x.CreditCard)
                .ToArray();
            foreach (var creditCard in creditCards)
            {
                Console.WriteLine($"--ID: {creditCard.CreditCardId}");
                Console.WriteLine($"---Limit: {creditCard.Limit:f2}");
                Console.WriteLine($"---Money Owed: {creditCard.MoneyOwned:f2}");
                Console.WriteLine($"---Limit Left: {creditCard.LimitLeft:f2}");
                Console.WriteLine($"---Expiration Date: {creditCard.ExpirationDate:yyyy/MM}");
            }
        }

        private static User GetUser(BillsPaymentSystemContext context)
        {
            int userId = int.Parse(Console.ReadLine());

            User user = null;

            while (true)
            {
                user = context.Users
                    .Where(x => x.UserId == userId)
                    .Include(x => x.PaymentMethods)
                    .ThenInclude(x => x.BankAccount)
                    .Include(x => x.PaymentMethods)
                    .ThenInclude(x => x.CreditCard)
                    .FirstOrDefault();

                if (user == null)
                {
                    userId = int.Parse(Console.ReadLine());
                    continue;
                }
                break;
            }
            return user;
        }
    }
}
