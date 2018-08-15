using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using P01.Work.App.Contracts;

namespace P01.Work.App.Core
{
    public class CommandInterpreter:ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            string commandName = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentException("Invalid Command");
            }

            var constructor = type.GetConstructors()
                .First();

            var constParameters = constructor.GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var service = constParameters.Select(serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);

            return result;
        }
    }
}
