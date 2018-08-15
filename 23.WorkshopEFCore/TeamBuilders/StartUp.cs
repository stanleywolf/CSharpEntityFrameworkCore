using System;
using TeamBuilders.Core;

namespace TeamBuilders
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
           var engine = new Engine(new CommandDispatcher(new AuthenticationManager()));

            engine.Run();
        }
    }
}
