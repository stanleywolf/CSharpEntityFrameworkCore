using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using TeamBuilder.Models;
using TeamBuilders.Utilities;

namespace TeamBuilders.Core
{
    public class AuthenticationManager
    {

        public User LogedInUser { get; private set; }

        public  AuthenticationManager() { }

        public void Login(User user)
        {
            if (this.LogedInUser != null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            this.LogedInUser = user;
        }
        public void Logout()
        {
            this.LogedInUser = null;
        }

        public void Authorize()
        {
            if (IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public bool IsAuthenticated()
        {
            return this.LogedInUser != null;
        }

        public User GetCurrentUser()
        {
            Authorize();

            return this.LogedInUser;
        }
    }
}
