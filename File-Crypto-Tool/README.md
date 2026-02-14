🔐 FileCipherSharp

A lightweight C# application for encrypting and decrypting files securely using SHA256-based key derivation.

📌 Overview

FileCipherSharp is a small C# project designed to demonstrate practical file encryption and decryption techniques in .NET. The application uses SHA256 to generate secure cryptographic hashes that strengthen data protection and ensure integrity. It provides a simple and efficient way to protect sensitive files while showcasing core cryptographic concepts. This project was developed for educational purposes and to deepen understanding of: Cryptographic hashing Secure file handling Key derivation concepts Practical implementation of security in C#

🚀 Features

Encrypt files securely Decrypt encrypted files SHA256-based key strengthening Simple and lightweight implementation Easy-to-understand code structure

🛠 Technologies Used

C#

.NET

SHA256 (System.Security.Cryptography)

📂 How It Works

The user provides a file and a password. The password is processed using SHA256. The derived hash is used to strengthen encryption security. The file is encrypted or decrypted accordingly.

⚠️ Important Note

SHA256 is a cryptographic hash function and not an encryption algorithm by itself. In this project, it is used to strengthen key derivation and improve security. This tool is intended for educational purposes and should not be used in production environments without additional security considerations.

📜 License

This project is open-source and available for educational use.
