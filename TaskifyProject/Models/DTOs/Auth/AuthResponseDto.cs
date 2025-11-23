namespace TaskifyProject.Models.DTOs.Auth
{
    /// <summary>
    /// Data transfer object for authentication response
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// The user ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// The user's role
        /// </summary>
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// The JWT access token
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration date and time
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
