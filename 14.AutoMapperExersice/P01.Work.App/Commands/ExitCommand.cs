using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using P01.Work.App.Contracts;

namespace P01.Work.App.Commands
{
    public class ExitCommand:ICommand
    {
        public string Execute(string[] args)
        {
            for (int i = 5; i >= 1; i--)
            {
                Console.WriteLine($"Program will close after {i} seconds");
                Thread.Sleep(1000);
            }
            Environment.Exit(0);
            return null;
        }
    }
}
