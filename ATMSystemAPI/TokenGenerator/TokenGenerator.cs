using System;
using System.Text;

public class TokenGenerator
{
    private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+-=[]{}|;:,.<>?";

    public static string GenerateCustomToken(int length)
    {
        Random random = new Random();
        StringBuilder tokenBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            int randomIndex = random.Next(Characters.Length);
            tokenBuilder.Append(Characters[randomIndex]);
        }

        return tokenBuilder.ToString();
    }
}
