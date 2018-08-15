using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystemInitiallizer
{
    public class BankAccountInitializer
    {
        public static BankAccount[] GetBankAccounts()
        {
            BankAccount[] bankAccounts = new BankAccount[]
            {
                new BankAccount(){BankName = "Unicredit",SwiftCode = "UNCRBGSF",Balance = 1500.00m},
                new BankAccount(){BankName = "FiBank",SwiftCode = "FIBABGSF",Balance = 8501.02m},
                new BankAccount(){BankName = "FirstInvest",SwiftCode = "FIINBGSF",Balance = 4582.00m},
                new BankAccount(){BankName = "BNB",SwiftCode = "BNBNBGSF",Balance = 3600.55m},
            };
            return bankAccounts;
        }
    }
}
