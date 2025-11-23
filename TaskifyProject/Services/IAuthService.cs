using TaskifyProject.Models.DTOs.Auth;

namespace TaskifyProject.Services
{
    /// <summary>
    /// Service interface for authentication operations
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="registerDto">The registration details</param>
        /// <returns>The authentication response with JWT token if successful, otherwise null</returns>
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="loginDto">The login credentials</param>
        /// <returns>The authentication response with JWT token if successful, otherwise null</returns>
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Validates a JWT token
        /// </summary>
        /// <param name="token">The JWT token to validate</param>
        /// <returns>True if the token is valid, otherwise false</returns>
        bool ValidateToken(string token);

        /// <summary>
        /// Gets user ID from JWT token
        /// </summary>
        /// <param name="token">The JWT token</param>
        /// <returns>The user ID if token is valid, otherwise null</returns>
        int? GetUserIdFromToken(string token);
    }
}
