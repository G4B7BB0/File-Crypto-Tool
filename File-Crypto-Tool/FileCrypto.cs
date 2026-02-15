using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

static class FileCrypto
{
    // Encrypt file using AES and password-derived key
    public static void Cifra(string inputFile, string outputFile, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16); // Random salt

        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        {
            fsOutput.Write(salt, 0, salt.Length); // Write salt

            using (Aes aes = Aes.Create())
            {
                // Derive 256-bit key from password
                var key = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);

                aes.IV = RandomNumberGenerator.GetBytes(16); // Random IV
                fsOutput.Write(aes.IV, 0, aes.IV.Length);    // Write IV

                using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                {
                    fsInput.CopyTo(cs); // Encrypt file content
                }
            }
        }
    }

    // Decrypt file using AES and password-derived key
    public static void Decifra(string inputFile, string outputFile, string password)
    {
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        {
            byte[] salt = new byte[16];
            if (fsInput.Read(salt, 0, 16) != 16)
                throw new CryptographicException("Invalid file (missing salt).");

            byte[] iv = new byte[16];
            if (fsInput.Read(iv, 0, 16) != 16)
                throw new CryptographicException("Invalid file (missing IV).");

            using (Aes aes = Aes.Create())
            {
                // Derive same key from password and salt
                var key = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
                aes.Key = key.GetBytes(32);
                aes.IV = iv;

                using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                {
                    cs.CopyTo(fsOutput); // Decrypt content
                }
            }
        }
    }

    // Handle encryption workflow
    public static void GestisciCifratura()
    {
        string inputPath = InputHandler.ChiediPercorsoFile();
        string password = InputHandler.ChiediPassword();
        string outputPath = inputPath + ".enc";

        Cifra(inputPath, outputPath, password);
        Console.WriteLine($"Encrypted file saved at: {outputPath}");
    }

    // Handle decryption workflow
    public static void GestisciDecifratura()
    {
        string inputPath = InputHandler.ChiediPercorsoFile();
        string password = InputHandler.ChiediPassword();
        string outputPath = inputPath.Replace(".enc", "") + ".dec";

        Decifra(inputPath, outputPath, password);
        Console.WriteLine($"Decrypted file saved at: {outputPath}");
    }
}
