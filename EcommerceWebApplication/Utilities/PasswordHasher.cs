// HashingUtilities.cs
using System;
using System.Security.Cryptography;

public static class HashingUtilities
{
    private const int SaltSize = 16; // 128 bit
    public const int KeySize = 32; // 256 bit
    private const int Iterations = 10000; // Number of iterations

    public static string HashPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(
            password,
            SaltSize,
            Iterations,
            HashAlgorithmName.SHA256))
        {
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }
    }
}

// AuthenticationService.cs

public class AuthenticationService
{
    public bool VerifyPassword(string storedHash, string providedPassword)
    {
        // Log the stored hash for debugging
        Console.WriteLine($"Stored Hash: {storedHash}");

        var parts = storedHash.Split('.', 3);

        if (parts.Length != 3)
        {
            // Log the error details for debugging
            Console.WriteLine($"Unexpected hash format. Received {parts.Length} parts.");
            throw new FormatException("Unexpected hash format. Ensure the stored hash is in the format 'iterations.salt.key'.");
        }

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        using (var algorithm = new Rfc2898DeriveBytes(
            providedPassword,
            salt,
            iterations,
            HashAlgorithmName.SHA256))
        {
            var keyToCheck = algorithm.GetBytes(HashingUtilities.KeySize);
            return keyToCheck.SequenceEqual(key);
        }
    }

}
