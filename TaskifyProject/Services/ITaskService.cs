using TaskifyProject.Models.DTOs.Tasks;

namespace TaskifyProject.Services
{
    /// <summary>
    /// Service interface for managing task-related business operations
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Creates a new task in the system for a specific user
        /// </summary>
        /// <param name="request">The task creation request containing task details</param>
        /// <param name="userId">The ID of the user creating the task</param>
        /// <returns>The created task details if successful, otherwise null</returns>
        Task<TaskResponseDto?> CreateTaskAsync(CreateTaskDto request, int userId);

        /// <summary>
        /// Retrieves a specific task by its unique identifier (only if it belongs to the user)
        /// </summary>
        /// <param name="taskId">The unique identifier of the task</param>
        /// <param name="userId">The ID of the user requesting the task</param>
        /// <returns>The task details if found and belongs to user, otherwise null</returns>
        Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId);

        /// <summary>
        /// Retrieves all tasks for a specific user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A collection of all tasks for the user</returns>
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int userId);

        /// <summary>
        /// Retrieves tasks filtered by status and/or priority with pagination for a specific user
        /// </summary>
        /// <param name="filter">Filter criteria including status, priority, page number, and page size</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A tuple containing the filtered tasks and the total count</returns>
        Task<(IEnumerable<TaskResponseDto> Tasks, int TotalCount)> GetFilteredTasksAsync(TaskFilterDto filter, int userId);

        /// <summary>
        /// Updates an existing task with new values (only if it belongs to the user)
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to update</param>
        /// <param name="request">The update request containing new values</param>
        /// <param name="userId">The ID of the user updating the task</param>
        /// <returns>The updated task details if successful, otherwise null</returns>
        Task<TaskResponseDto?> UpdateTaskAsync(int taskId, UpdateTaskDto request, int userId);

        /// <summary>
        /// Deletes a task from the system (only if it belongs to the user)
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to delete</param>
        /// <param name="userId">The ID of the user deleting the task</param>
        /// <returns>True if the task was deleted successfully, otherwise false</returns>
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}
