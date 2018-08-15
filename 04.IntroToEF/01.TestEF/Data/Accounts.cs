using System;
using System.Collections.Generic;

namespace _01.TestEF.Data
{
    public partial class Accounts
    {
        public Accounts()
        {
            Logs = new HashSet<Logs>();
        }

        public int Id { get; set; }
        public int AccountHolderId { get; set; }
        public decimal? Balance { get; set; }

        public AccountHolders AccountHolder { get; set; }
        public ICollection<Logs> Logs { get; set; }
    }
}
