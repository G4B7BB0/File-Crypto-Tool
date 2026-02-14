using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

static class FileCrypto
{

    public static void Cifra(string inputFile, string outputFile, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        {
            fsOutput.Write(salt, 0, salt.Length);

            using (Aes aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);
                aes.IV = RandomNumberGenerator.GetBytes(16);

                fsOutput.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                {
                    fsInput.CopyTo(cs);
                }
            }
        }
    }

    public static void Decifra(string inputFile, string outputFile, string password)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        {
            byte[] salt = new byte[16];
            if (fsInput.Read(salt, 0, salt.Length) != 16)
                throw new CryptographicException("File non valido (salt mancante).");

            byte[] iv = new byte[16];
            if (fsInput.Read(iv, 0, iv.Length) != 16)
                throw new CryptographicException("File non valido (IV mancante).");

            using (Aes aes = Aes.Create())
            {
                var key = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                {
                    cs.CopyTo(fsOutput);
                }
            }
        }
    }

    public static void GestisciCifratura()
    {
        string inputPath = InputHandler.ChiediPercorsoFile();
        string password = InputHandler.ChiediPassword();
        string outputPath = inputPath + ".enc";

        Cifra(inputPath, outputPath, password);
        Console.WriteLine($"File cifrato salvato in: {outputPath}");
    }

    public static void GestisciDecifratura()
    {
        string inputPath = InputHandler.ChiediPercorsoFile();
        string password = InputHandler.ChiediPassword();
        string outputPath = inputPath.Replace(".enc", "") + ".dec";

        Decifra(inputPath, outputPath, password);
        Console.WriteLine($"File decifrato salvato in: {outputPath}");
    }
}
