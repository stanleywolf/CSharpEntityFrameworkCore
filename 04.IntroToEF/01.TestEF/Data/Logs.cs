using System;
using System.Collections.Generic;

namespace _01.TestEF.Data
{
    public partial class Logs
    {
        public int LogId { get; set; }
        public int? AccountId { get; set; }
        public decimal? OldSum { get; set; }
        public decimal? NewSum { get; set; }

        public Accounts Account { get; set; }
    }
}
