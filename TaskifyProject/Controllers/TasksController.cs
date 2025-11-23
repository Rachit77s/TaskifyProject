using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskifyProject.Models.DTOs.Common;
using TaskifyProject.Models.DTOs.Tasks;
using TaskifyProject.Services;

namespace TaskifyProject.Controllers
{
    /// <summary>
    /// API Controller for managing tasks (requires authentication)
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Initializes a new instance of the TasksController class
        /// </summary>
        /// <param name="taskService">The task service for business operations</param>
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Gets the current user's ID from the JWT token
        /// </summary>
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim!);
        }

        /// <summary>
        /// Creates a new task
        /// </summary>
        /// <param name="request">The task creation request</param>
        /// <returns>The created task with success message</returns>
        /// <response code="201">Returns the newly created task</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> CreateTask([FromBody] CreateTaskDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<TaskResponseDto>.ErrorResponse("Validation failed", errors));
            }

            var userId = GetCurrentUserId();
            var result = await _taskService.CreateTaskAsync(request, userId);

            if (result == null)
            {
                return BadRequest(ApiResponse<TaskResponseDto>.ErrorResponse("Failed to create task"));
            }

            return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, 
                ApiResponse<TaskResponseDto>.SuccessResponse(result, "Task created successfully"));
        }

        /// <summary>
        /// Retrieves a task by its unique identifier (only if it belongs to the current user)
        /// </summary>
        /// <param name="id">The task ID</param>
        /// <returns>The task details</returns>
        /// <response code="200">Returns the task details</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the task is not found or doesn't belong to the user</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> GetTaskById(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _taskService.GetTaskByIdAsync(id, userId);

            if (result == null)
            {
                return NotFound(ApiResponse<TaskResponseDto>.ErrorResponse("Task not found"));
            }

            return Ok(ApiResponse<TaskResponseDto>.SuccessResponse(result));
        }

        /// <summary>
        /// Retrieves all tasks for the current user
        /// </summary>
        /// <returns>A collection of all tasks for the current user</returns>
        /// <response code="200">Returns the list of all tasks</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TaskResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TaskResponseDto>>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<IEnumerable<TaskResponseDto>>>> GetAllTasks()
        {
            var userId = GetCurrentUserId();
            var tasks = await _taskService.GetAllTasksAsync(userId);
            return Ok(ApiResponse<IEnumerable<TaskResponseDto>>.SuccessResponse(tasks));
        }

        /// <summary>
        /// Retrieves tasks filtered by status and/or priority with pagination (for current user)
        /// </summary>
        /// <param name="filter">Filter criteria including status, priority, page number, and page size</param>
        /// <returns>A paginated list of filtered tasks with metadata</returns>
        /// <response code="200">Returns the filtered and paginated tasks</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<object>>> GetFilteredTasks([FromQuery] TaskFilterDto filter)
        {
            var userId = GetCurrentUserId();
            var (tasks, totalCount) = await _taskService.GetFilteredTasksAsync(filter, userId);

            var result = new
            {
                Tasks = tasks,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

            return Ok(ApiResponse<object>.SuccessResponse(result));
        }

        /// <summary>
        /// Updates an existing task (only if it belongs to the current user). Only provided fields will be updated.
        /// </summary>
        /// <param name="id">The task ID</param>
        /// <param name="request">The update request containing new values</param>
        /// <returns>The updated task details</returns>
        /// <response code="200">Returns the updated task</response>
        /// <response code="400">If the request is invalid or validation fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the task is not found or doesn't belong to the user</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<TaskResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TaskResponseDto>>> UpdateTask(int id, [FromBody] UpdateTaskDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(ApiResponse<TaskResponseDto>.ErrorResponse("Validation failed", errors));
            }

            var userId = GetCurrentUserId();
            var result = await _taskService.UpdateTaskAsync(id, request, userId);

            if (result == null)
            {
                return NotFound(ApiResponse<TaskResponseDto>.ErrorResponse("Task not found"));
            }

            return Ok(ApiResponse<TaskResponseDto>.SuccessResponse(result, "Task updated successfully"));
        }

        /// <summary>
        /// Deletes a task from the system (only if it belongs to the current user)
        /// </summary>
        /// <param name="id">The task ID</param>
        /// <returns>Success confirmation</returns>
        /// <response code="200">If the task was deleted successfully</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="404">If the task is not found or doesn't belong to the user</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTask(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _taskService.DeleteTaskAsync(id, userId);

            if (!result)
            {
                return NotFound(ApiResponse<object>.ErrorResponse("Task not found"));
            }

            return Ok(ApiResponse<object>.SuccessResponse(null, "Task deleted successfully"));
        }
    }
}
