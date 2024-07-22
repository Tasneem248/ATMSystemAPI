using ATMSystemAPI.Models;
using System.Collections.Generic;

namespace ATMSystemAPI.Data
{
    public static class Common
    {
        //public static List<User> Users = new List<User>
        //{
        //    new User { Id = 1, Username = "tasnim", Password = "123456", Token="" }
        //};
        public static List<User> Users = new List<User>();

        public static List<Account> Accounts = new List<Account>();
        //{
        //    new Account { Id = 1, UserId = 1, Balance = 1000 },
        //    new Account { Id = 2, UserId = 2, Balance = 2000 }
        //};

        public static List<Transaction> Transactions = new List<Transaction>();
    }
}
