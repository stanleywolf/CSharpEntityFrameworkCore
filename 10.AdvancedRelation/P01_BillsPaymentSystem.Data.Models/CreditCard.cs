using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_BillsPaymentSystem.Data.Models
{
   public  class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        [Required]
        public decimal Limit { get; set; }

        [Required]
        public decimal MoneyOwned { get; private set; }

        [NotMapped]
        public decimal LimitLeft { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                this.MoneyOwned -= amount;
            }
        }

        public void Withdrow(decimal amount)
        {
            if (this.LimitLeft - amount >= 0)
            {
                this.MoneyOwned += amount;
            }
        }
    }
}
