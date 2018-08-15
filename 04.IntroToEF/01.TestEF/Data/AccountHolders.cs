using System;
using System.Collections.Generic;

namespace _01.TestEF.Data
{
    public partial class AccountHolders
    {
        public AccountHolders()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }

        public ICollection<Accounts> Accounts { get; set; }
    }
}
