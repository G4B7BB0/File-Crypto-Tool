using System;
using System.Security.Cryptography;

class Program
{

    static void MostraMenu()
    {
        Console.WriteLine("=== File Crypto Tool ===");
        Console.WriteLine("1 - Cifra file");
        Console.WriteLine("2 - Decifra file");
        Console.Write("Scelta: ");
    }

    static void Main()
    {
        try
        {
            MostraMenu();
            string scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    FileCrypto.GestisciCifratura();
                    break;
                case "2":
                    FileCrypto.GestisciDecifratura();
                    break;
                default:
                    Console.WriteLine("Scelta non valida.");
                    break;
            }
        }
        catch (Exception ex) when (
            ex is FileNotFoundException ||
            ex is UnauthorizedAccessException ||
            ex is CryptographicException ||
            ex is IOException ||
            ex is ArgumentException
        )
        {
            Console.WriteLine("Errore: " + ex + " -- " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore imprevisto: " +  ex + " -- " + ex.Message);
        }
    }
}
