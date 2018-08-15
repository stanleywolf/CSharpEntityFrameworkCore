using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.Data;
using TeamBuilder.Models;
using TeamBuilder.Models.Enums;
using TeamBuilders.Utilities;

namespace TeamBuilders.Core.Commands
{
    public class RegisterUserCommand:ICommand
    {
        private readonly TeamBuilderContext context;
        private readonly AuthenticationManager authenticationManager;

        public RegisterUserCommand(AuthenticationManager manager)
        {
            this.context = new TeamBuilderContext();
            this.authenticationManager = manager;
        }

        public string Execute(string[] args)
        {
            Check.CheckLenght(7,args);

            string username = args[0];
            string password = args[1];
            string repeatPassword = args[2];
            string firstName = args[3];
            string lastName = args[4];
            int age;
            GenderType gender;


            if (username.Length < Constants.MinUsernameLength || username.Length < Constants.MaxUsernameLength)
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.UsernameNotValid,username));
            }
            if (password.Length < Constants.MinPasswordLength || password.Length < Constants.MaxPasswordLength)
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            if(!int.TryParse(args[5], out age))
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            if (!Enum.TryParse<GenderType>( args[6], out gender))
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }
            if(CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(String.Format(Constants.ErrorMessages.UsernameIsTaken,username));
            }
            //TODO add session check
            if (authenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            User entity = new User()
            {
                Username = username,
                Password = password,
                Age = age,
                FirstName = firstName,
                LastName = lastName,
                GenderType = gender
            };

            this.context.Users.Add(entity);
            this.context.SaveChanges();

            return $"User {username} was registered successfully!";
        }
    }
}
