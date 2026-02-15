using System;
using System.Security.Cryptography;

class Program
{
    // Display main menu
    static void MostraMenu()
    {
        Console.WriteLine("=== File Crypto Tool ===");
        Console.WriteLine("1 - Encrypt file");
        Console.WriteLine("2 - Decrypt file");
        Console.Write("Choice: ");
    }

    static void Main()
    {
        try
        {
            MostraMenu();
            string scelta = Console.ReadLine();

            // Handle user choice
            switch (scelta)
            {
                case "1":
                    FileCrypto.GestisciCifratura();
                    break;

                case "2":
                    FileCrypto.GestisciDecifratura();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
        // Handle expected operational errors
        catch (Exception ex) when (
            ex is FileNotFoundException ||
            ex is UnauthorizedAccessException ||
            ex is CryptographicException ||
            ex is IOException ||
            ex is ArgumentException
        )
        {
            Console.WriteLine("Error: " + ex + " -- " + ex.Message);
        }
        // Handle unexpected errors
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex + " -- " + ex.Message);
        }
    }
}
