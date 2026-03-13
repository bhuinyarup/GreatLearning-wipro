using W2Day5AssignmentSolution.Security;

namespace W2Day5AssignmentSolution.Tests;

public sealed class AesEncryptionServiceTests
{
    [Fact]
    public void Encrypt_Then_Decrypt_Should_Return_Original_Text()
    {
        var key = new byte[]
        {
            0x10, 0x22, 0x34, 0x45, 0x51, 0x62, 0x73, 0x84,
            0x95, 0xA6, 0xB7, 0xC8, 0xD9, 0xEA, 0xFB, 0x0C,
            0x11, 0x21, 0x31, 0x41, 0x52, 0x63, 0x74, 0x85,
            0x96, 0xA7, 0xB8, 0xC9, 0xDA, 0xEB, 0xFC, 0x0D
        };

        var iv = new byte[]
        {
            0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78,
            0x89, 0x9A, 0xAB, 0xBC, 0xCD, 0xDE, 0xEF, 0xF0
        };

        var service = new AesEncryptionService(key, iv);

        const string plain = "Top secret details";
        var cipher = service.Encrypt(plain);
        var decrypted = service.Decrypt(cipher);

        Assert.NotEqual(plain, cipher);
        Assert.Equal(plain, decrypted);
    }
}
