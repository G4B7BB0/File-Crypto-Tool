using System;
using System.IO;

static class InputHandler
{
    public static string ChiediPercorsoFile()
    {
        Console.Write("Percorso file: ");
        string path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Percorso file non valido.");

        if (!File.Exists(path))
            throw new FileNotFoundException("Il file specificato non esiste.");

        return path;
    }

    public static string ChiediPassword()
    {
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La password non può essere vuota.");

        return password;
    }
}
