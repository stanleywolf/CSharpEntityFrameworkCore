using System;
using System.Collections.Generic;
using System.Text;

namespace PremierShipDb.App.Contracts
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
