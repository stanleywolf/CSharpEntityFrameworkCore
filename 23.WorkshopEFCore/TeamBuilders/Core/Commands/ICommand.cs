using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilders.Core.Commands
{
    public  interface ICommand
    {
        string Execute(string[] args);
    }
}
