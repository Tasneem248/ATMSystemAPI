using ATMSystemAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ATMSystemAPI.Helpers
{
    public static class AccountFileHelper
    {
        private static string filePath = "accounts.txt";

        public static List<Account> ReadAccountsFromFile()
        {
            var accounts = new List<Account>();
            if (!File.Exists(filePath))
            {
                return accounts;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    var idPart = parts[0].Split(':');
                    var userIdPart = parts[1].Split(':');
                    var balancePart = parts[2].Split(':');

                    if (idPart.Length == 2 && userIdPart.Length == 2 && balancePart.Length == 2)
                    {
                        accounts.Add(new Account
                        {
                            Id = int.Parse(idPart[1]),
                            UserId = userIdPart[1], // UserId string olmalı
                            Balance = decimal.Parse(balancePart[1])
                        });
                    }
                }
            }
            return accounts;
        }

        public static void WriteAccountsToFile(List<Account> accounts)
        {
            var lines = accounts.Select(a => $"id:{a.Id},userid:{a.UserId},balance:{a.Balance}");
            File.WriteAllLines(filePath, lines);
        }
    }
}
