namespace TaskifyProject.Models.DTOs.Common
{
    /// <summary>
    /// Generic wrapper for API responses with consistent structure
    /// </summary>
    /// <typeparam name="T">The type of data being returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message describing the result of the operation
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// The data payload of the response
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// A list of error messages if the operation failed
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// Creates a successful response with data
        /// </summary>
        /// <param name="data">The data to return</param>
        /// <param name="message">Success message (default: "Operation successful")</param>
        /// <returns>A success response object</returns>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates an error response with optional error details
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="errors">List of specific error messages (optional)</param>
        /// <returns>An error response object</returns>
        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
