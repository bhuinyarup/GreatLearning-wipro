using W2Day5AssignmentSolution.Logging;
using W2Day5AssignmentSolution.Models;
using W2Day5AssignmentSolution.Security;

namespace W2Day5AssignmentSolution.Services;

public sealed class UserService
{
    private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDataEncryptionService _encryptionService;
    private readonly IAppLogger _logger;

    public UserService(IPasswordHasher passwordHasher, IDataEncryptionService encryptionService, IAppLogger logger)
    {
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _encryptionService = encryptionService ?? throw new ArgumentNullException(nameof(encryptionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Register(string username, string password, string sensitiveDetails)
    {
        try
        {
            ValidateCredentials(username, password);

            if (_users.ContainsKey(username))
            {
                throw new InvalidOperationException("User already exists.");
            }

            var user = new User
            {
                Username = username,
                HashedPassword = _passwordHasher.Hash(password),
                EncryptedDetails = _encryptionService.Encrypt(sensitiveDetails)
            };

            _users[username] = user;
            _logger.Info($"User registered: {username}");
        }
        catch (Exception ex)
        {
            _logger.Error("An error occurred while registering a user.", ex);
            throw new InvalidOperationException("Unable to register user due to a processing error.");
        }
    }

    public bool Authenticate(string username, string password)
    {
        try
        {
            ValidateCredentials(username, password);

            if (!_users.TryGetValue(username, out var user))
            {
                _logger.Info($"Authentication failed for unknown user: {username}");
                return false;
            }

            var isAuthenticated = _passwordHasher.Verify(password, user.HashedPassword);
            _logger.Info($"Authentication {(isAuthenticated ? "succeeded" : "failed")} for user: {username}");
            return isAuthenticated;
        }
        catch (Exception ex)
        {
            _logger.Error("An error occurred while authenticating a user.", ex);
            throw new InvalidOperationException("Unable to authenticate user due to a processing error.");
        }
    }

    public string GetSensitiveDetails(string username)
    {
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);

            if (!_users.TryGetValue(username, out var user))
            {
                throw new KeyNotFoundException("User not found.");
            }

            var details = _encryptionService.Decrypt(user.EncryptedDetails);
            _logger.Info($"Sensitive details retrieved for user: {username}");
            return details;
        }
        catch (Exception ex)
        {
            _logger.Error("An error occurred while retrieving user details.", ex);
            throw new InvalidOperationException("Unable to retrieve user details due to a processing error.");
        }
    }

    public User? FindUser(string username)
    {
        _users.TryGetValue(username, out var user);
        return user;
    }

    private static void ValidateCredentials(string username, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
    }
}
