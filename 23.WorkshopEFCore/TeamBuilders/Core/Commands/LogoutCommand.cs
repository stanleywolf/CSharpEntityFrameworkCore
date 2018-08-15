using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Data;
using TeamBuilders.Utilities;

namespace TeamBuilders.Core.Commands
{
    public class LogoutCommand:ICommand
    {

        private readonly AuthenticationManager authenticationManager;
        public LogoutCommand(AuthenticationManager manager)
        {
            this.authenticationManager = manager;
        }
        public string Execute(string[] args)
        {
            string username = this.authenticationManager.GetCurrentUser().Username;

            if (!this.authenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            this .authenticationManager.Logout();

            return $"User {username} successfully logged out!";
        }
    }
}
