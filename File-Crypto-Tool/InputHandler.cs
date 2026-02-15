using System;
using System.IO;

static class InputHandler
{
    // Ask user for file path and validate it
    public static string ChiediPercorsoFile()
    {
        Console.Write("Percorso file: ");
        string path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Invalid file path.");

        if (!File.Exists(path))
            throw new FileNotFoundException("Specified file does not exist.");

        return path;
    }

    // Ask user for password and validate it
    public static string ChiediPassword()
    {
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.");

        return password;
    }
}
