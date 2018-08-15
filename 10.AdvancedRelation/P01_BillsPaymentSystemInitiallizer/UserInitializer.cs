using System;
using System.Collections.Generic;
using System.Text;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystemInitiallizer
{
    public class UserInitializer
    {
        public static User[] GetUsers()
        {
            User[] users = new User[]
            {
                new User()
                {
                    FirstName = "Stanislav",
                    LastName = "Nikolov",
                    Email = "stanleyoffer@gmail.com",
                    Password = "********"
                },
                new User()
                {
                    FirstName = "Petya",
                    LastName = "Peeva",
                    Email = "Beckinternational@gmail.com",
                    Password = "********"
                },
                new User()
                {
                    FirstName = "Nikola",
                    LastName = "Nikolov",
                    Email = "nikola2010@gmail.com",
                    Password = "********"
                },
                new User()
                {
                    FirstName = "Yoan",
                    LastName = "Nikolov",
                    Email = "yoan2016@gmail.com",
                    Password = "********"
                }
            };
            return users;
        }
        
    }
}
