using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilders.Core;
using TeamBuilders.Core.Commands;

namespace TeamBuilders
{
    public class CommandDispatcher
    {
        private readonly AuthenticationManager authenticationManager;

        public CommandDispatcher(AuthenticationManager manager)
        {
            this.authenticationManager = manager;
        }
        public string Dispatch(string input)
        {
            string[] args = input.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            string commandName = args.Length > 0 ? args[0] : String.Empty;

            string[] commandArgs = args.Skip(1).ToArray();

            switch (commandName)
            {
                case "Exit":
                    ExitCommand exitCommand = new ExitCommand();
                    return exitCommand.Execute(new string[0]);

                case "RegisterUser":
                    RegisterUserCommand registerUserCommand = new RegisterUserCommand(authenticationManager);
                    return registerUserCommand.Execute(commandArgs);

                case "Login":
                    LoginCommand loginCommand = new LoginCommand(authenticationManager);
                    return loginCommand.Execute(commandArgs);

                case "Logout":
                    LogoutCommand logoutCommand = new LogoutCommand(authenticationManager);
                    return logoutCommand.Execute(commandArgs);

                case "DeleteUser":
                    DeleteUserCommand deleteUserCommand = new DeleteUserCommand(authenticationManager);
                    return deleteUserCommand.Execute(commandArgs);

                default:
                    throw new NotSupportedException($"Command {commandName} is not supported");
            }
        }
    }
}
