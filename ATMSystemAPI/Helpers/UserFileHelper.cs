using System.IO;
using System.Linq;
using System.Collections.Generic;
using ATMSystemAPI.Models;

namespace ATMSystemAPI.Helpers
{
    public static class UserFileHelper
    {
        private static string filePath = "users.txt";

        public static List<User> ReadUsersFromFile()
        {
            var users = new List<User>();
            if (!File.Exists(filePath))
            {
                return users;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    users.Add(new User
                    {
                        Username = parts[0].Split(':')[1],
                        Password = parts[1].Split(':')[1],
                        Token = parts[2].Split(':')[1]
                    });
                }
            }
            return users;
        }

        public static void WriteUsersToFile(List<User> users)
        {
            var lines = users.Select(u => $"username:{u.Username},password:{u.Password},token:{u.Token}");
            File.WriteAllLines(filePath, lines);
        }

        public static void UpdateUserToken(string username, string token)
        {
            var users = ReadUsersFromFile();
            var user = users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Token = token;
                WriteUsersToFile(users);
            }
        }
    }
}
