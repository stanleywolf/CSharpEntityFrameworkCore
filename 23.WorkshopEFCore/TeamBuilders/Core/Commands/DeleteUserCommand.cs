using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilders.Utilities;

namespace TeamBuilders.Core.Commands
{
    public class DeleteUserCommand:ICommand
    {
        private readonly AuthenticationManager authenticationManager;
        private readonly TeamBuilderContext context;
        public DeleteUserCommand(AuthenticationManager manager)
        {
            this.context = new TeamBuilderContext();
            this.authenticationManager = manager;
        }

        public string Execute(string[] args)
        {
            User user = this.authenticationManager.GetCurrentUser();

            if (user == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            user.IsDeleted = true;

            this.context.Users.Update(user);

            this.context.SaveChanges();

            return $"User {user.Username} was deleted successfully!";

        }
    }
}
