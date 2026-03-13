namespace W2Day5AssignmentSolution.Models;

public sealed class User
{
    public required string Username { get; init; }
    public required string HashedPassword { get; init; }
    public required string EncryptedDetails { get; init; }
}
