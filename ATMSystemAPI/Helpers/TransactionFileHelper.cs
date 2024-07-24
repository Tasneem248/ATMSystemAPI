using ATMSystemAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ATMSystemAPI.Helpers
{
    public static class TransactionFileHelper
    {
        private static string filePath = "transaction.txt";

        public static List<Transaction> ReadTransactionsFromFile()
        {
            var transactions = new List<Transaction>();
            if (!File.Exists(filePath))
            {
                return transactions;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    transactions.Add(new Transaction
                    {
                        Id = int.Parse(parts[0].Split(':')[1]),
                        AccountId = int.Parse(parts[1].Split(':')[1]),
                        Amount = decimal.Parse(parts[2].Split(':')[1]),
                        Date = DateTime.Parse(parts[3].Split(':')[1]),
                        Type = parts[4].Split(':')[1]
                    });
                }
            }
            return transactions;
        }

        public static void WriteTransactionsToFile(List<Transaction> transactions)
        {
            var lines = transactions.Select(t => $"Id:{t.Id},AccountId:{t.AccountId},Amount:{t.Amount},Date:{t.Date},Type:{t.Type}");
            File.WriteAllLines(filePath, lines);
        }

        public static void AddTransaction(Transaction transaction)
        {
            var transactions = ReadTransactionsFromFile();
            transactions.Add(transaction);
            WriteTransactionsToFile(transactions);
        }
    }
}
