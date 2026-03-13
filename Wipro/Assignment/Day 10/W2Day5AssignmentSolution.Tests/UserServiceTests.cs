using System.Security.Cryptography;
using W2Day5AssignmentSolution.Logging;
using W2Day5AssignmentSolution.Security;
using W2Day5AssignmentSolution.Services;

namespace W2Day5AssignmentSolution.Tests;

public sealed class UserServiceTests
{
    private static readonly byte[] Key = [
        0x10, 0x22, 0x34, 0x45, 0x51, 0x62, 0x73, 0x84,
        0x95, 0xA6, 0xB7, 0xC8, 0xD9, 0xEA, 0xFB, 0x0C,
        0x11, 0x21, 0x31, 0x41, 0x52, 0x63, 0x74, 0x85,
        0x96, 0xA7, 0xB8, 0xC9, 0xDA, 0xEB, 0xFC, 0x0D
    ];

    private static readonly byte[] Iv =
    [
        0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78,
        0x89, 0x9A, 0xAB, 0xBC, 0xCD, 0xDE, 0xEF, 0xF0
    ];

    [Fact]
    public void Register_And_Authenticate_Should_Hash_And_Verify_Password()
    {
        var service = CreateUserService(out _);

        service.Register("alice", "StrongPassword123!", "alice@example.com");

        var user = service.FindUser("alice");
        Assert.NotNull(user);
        Assert.NotEqual("StrongPassword123!", user!.HashedPassword);
        Assert.True(service.Authenticate("alice", "StrongPassword123!"));
        Assert.False(service.Authenticate("alice", "WrongPassword"));
    }

    [Fact]
    public void GetSensitiveDetails_Should_Return_Decrypted_Value()
    {
        var service = CreateUserService(out _);

        service.Register("bob", "Password!42", "bob@example.com");

        var details = service.GetSensitiveDetails("bob");
        Assert.Equal("bob@example.com", details);
    }

    [Fact]
    public void Register_When_Encryption_Fails_Should_Log_Error_And_Throw_Generic_Message()
    {
        var logger = new InMemoryLogger();
        var service = new UserService(new Sha256PasswordHasher(), new ThrowingEncryptionService(), logger);

        var ex = Assert.Throws<InvalidOperationException>(() =>
            service.Register("charlie", "Password!42", "charlie@example.com"));

        Assert.Equal("Unable to register user due to a processing error.", ex.Message);
        Assert.Single(logger.ErrorEntries);
        Assert.Contains("registering", logger.ErrorEntries[0].Message, StringComparison.OrdinalIgnoreCase);
    }

    private static UserService CreateUserService(out InMemoryLogger logger)
    {
        logger = new InMemoryLogger();
        return new UserService(
            new Sha256PasswordHasher(),
            new AesEncryptionService(Key, Iv),
            logger);
    }

    private sealed class ThrowingEncryptionService : IDataEncryptionService
    {
        public string Encrypt(string plainText) => throw new CryptographicException("Encryption failure details");
        public string Decrypt(string cipherText) => throw new NotImplementedException();
    }

    private sealed class InMemoryLogger : IAppLogger
    {
        public List<string> InfoEntries { get; } = [];
        public List<(string Message, Exception Exception)> ErrorEntries { get; } = [];

        public void Info(string message) => InfoEntries.Add(message);

        public void Error(string message, Exception exception) => ErrorEntries.Add((message, exception));
    }
}
