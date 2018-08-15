using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilders.Utilities;

namespace TeamBuilders.Core.Commands
{
    public class LoginCommand:ICommand
    {
        private readonly AuthenticationManager authenticationManager;
        private readonly TeamBuilderContext context;
        public LoginCommand(AuthenticationManager manager)
        {
            this.context = new TeamBuilderContext();
            this.authenticationManager = manager;
        }


        public string Execute(string[] args)
        {
            Check.CheckLenght(2,args);

            if (this.authenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }
            string username = args[0];
            string password = args[1];

            if (!this.context.Users.Any(x => x.Username == username))
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }
            User user = this.context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }
            this.authenticationManager.Login(user);

            return $"User {username} successfully logged in!";
        }
    }
}
