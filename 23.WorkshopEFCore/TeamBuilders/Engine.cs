using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilders
{
    public class Engine
    {
        private readonly CommandDispatcher dispatcher;

        public Engine(CommandDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();

                    this.dispatcher.Dispatch(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetBaseException().Message);
                }
            }
        }
    }
}
