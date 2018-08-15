using System;
using System.Collections.Generic;
using System.Text;

namespace PremierShipDb.App.Contracts
{
    public interface ICommandInterpreter
    {
        string Read(string[] args);
    }
}
