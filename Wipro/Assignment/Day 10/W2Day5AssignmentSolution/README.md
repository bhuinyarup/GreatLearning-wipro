# W2 Day 5 Daily Coding Assignment

This project implements a secure and reliable user management component with unit tests.

## Implemented Features

1. User Authentication
- Register users with username and password.
- Password hashing with SHA-256 (`Sha256PasswordHasher`).
- Authentication by verifying hashed passwords (`UserService.Authenticate`).

2. Data Encryption
- AES encryption/decryption for sensitive details (`AesEncryptionService`).
- Encrypted details are stored in the user model.

3. Error Handling and Logging
- Guarded `try/catch` blocks in service methods.
- Generic user-facing error messages to avoid leaking sensitive internals.
- File-based logging with timestamps, error details, and stack traces (`FileAppLogger`).

4. Unit Tests
- Authentication and password hashing verification.
- Encryption/decryption roundtrip validation.
- Error handling and logging behavior checks.
- File logging output validation.

## Project Structure

- `Models/User.cs`
- `Security/Sha256PasswordHasher.cs`
- `Security/AesEncryptionService.cs`
- `Logging/IAppLogger.cs`
- `Logging/FileAppLogger.cs`
- `Services/UserService.cs`
- `W2Day5AssignmentSolution.Tests/*`

## Run Tests

```powershell
dotnet test C:\Users\rajha\Code\W2Day5AssignmentSolution.slnx
```
