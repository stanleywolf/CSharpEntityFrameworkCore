﻿using System;
using System.Collections.Generic;
using System.Text;

namespace P01.Work.App.Contracts
{
   public  interface ICommandInterpreter
   {
       string Read(string[] input);
   }
}
