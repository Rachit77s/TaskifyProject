namespace TaskifyProject.Models.Configuration
{
    /// <summary>
    /// Configuration settings for JWT authentication
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// The secret key used to sign JWT tokens
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// The token issuer
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// The token audience
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Token expiration time in minutes
        /// </summary>
        public int ExpirationMinutes { get; set; } = 60;
    }
}
