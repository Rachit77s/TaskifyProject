using Microsoft.AspNetCore.Mvc;
using TaskifyProject.Models.DTOs.Auth;
using TaskifyProject.Models.DTOs.Common;
using TaskifyProject.Services;

namespace TaskifyProject.Controllers
{
    /// <summary>
    /// API Controller for authentication operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the AuthController class
        /// </summary>
        /// <param name="authService">The authentication service</param>
        /// <param name="logger">The logger</param>
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="registerDto">The registration details</param>
        /// <returns>The authentication response with JWT token</returns>
        /// <response code="201">Returns the newly registered user with JWT token</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="409">If username or email already exists</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse("Validation failed", errors));
            }

            var result = await _authService.RegisterAsync(registerDto);

            if (result == null)
            {
                return Conflict(ApiResponse<AuthResponseDto>.ErrorResponse(
                    "Username or email already exists"));
            }

            _logger.LogInformation("User {Username} registered successfully", registerDto.Username);

            return CreatedAtAction(
                nameof(Register),
                ApiResponse<AuthResponseDto>.SuccessResponse(result, "User registered successfully"));
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        /// <param name="loginDto">The login credentials</param>
        /// <returns>The authentication response with JWT token</returns>
        /// <response code="200">Returns the user details with JWT token</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="401">If the credentials are incorrect</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<AuthResponseDto>.ErrorResponse("Validation failed", errors));
            }

            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                _logger.LogWarning("Failed login attempt for {UsernameOrEmail}", loginDto.UsernameOrEmail);
                return Unauthorized(ApiResponse<AuthResponseDto>.ErrorResponse(
                    "Invalid username/email or password"));
            }

            _logger.LogInformation("User {Username} logged in successfully", result.Username);

            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful"));
        }

        /// <summary>
        /// Validates a JWT token
        /// </summary>
        /// <param name="token">The JWT token to validate</param>
        /// <returns>Validation result</returns>
        /// <response code="200">If the token is valid</response>
        /// <response code="401">If the token is invalid</response>
        [HttpPost("validate")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public ActionResult<ApiResponse<object>> ValidateToken([FromBody] string token)
        {
            var isValid = _authService.ValidateToken(token);

            if (!isValid)
            {
                return Unauthorized(ApiResponse<object>.ErrorResponse("Invalid or expired token"));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Token is valid"));
        }
    }
}
