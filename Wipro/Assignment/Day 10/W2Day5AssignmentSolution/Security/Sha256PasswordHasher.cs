using System.Security.Cryptography;
using System.Text;

namespace W2Day5AssignmentSolution.Security;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hashedPassword);
}

public sealed class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash);
    }

    public bool Verify(string password, string hashedPassword)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword);
        var computed = Hash(password);
        return string.Equals(computed, hashedPassword, StringComparison.Ordinal);
    }
}
