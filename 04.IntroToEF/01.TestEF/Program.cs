using System;
using System.Linq;
using _01.TestEF.Data;

namespace _01.TestEF
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BankContext();

            var account = context.AccountHolders.FirstOrDefault();
            Console.WriteLine(string.Join(", ",account.FirstName));
            Console.WriteLine("Hello World!");
        }
    }
}
