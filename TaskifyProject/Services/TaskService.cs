using TaskifyProject.Models.DTOs.Tasks;
using TaskifyProject.Models.Entities;
using TaskifyProject.Repositories;

namespace TaskifyProject.Services
{
    /// <summary>
    /// Service implementation for managing task-related business logic
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of the TaskService class
        /// </summary>
        /// <param name="taskRepository">The task repository for data access</param>
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Creates a new task in the system with the provided details for a specific user
        /// </summary>
        /// <param name="request">The task creation request containing task details</param>
        /// <param name="userId">The ID of the user creating the task</param>
        /// <returns>The created task details if successful, otherwise null</returns>
        public async Task<TaskResponseDto?> CreateTaskAsync(CreateTaskDto request, int userId)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return MapToDto(task);
        }

        /// <summary>
        /// Retrieves a specific task by its unique identifier (only if it belongs to the user)
        /// </summary>
        /// <param name="taskId">The unique identifier of the task</param>
        /// <param name="userId">The ID of the user requesting the task</param>
        /// <returns>The task details if found and belongs to user, otherwise null</returns>
        public async Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null || task.UserId != userId)
            {
                return null;
            }

            return MapToDto(task);
        }

        /// <summary>
        /// Retrieves all tasks for a specific user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A collection of all tasks for the user</returns>
        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Where(t => t.UserId == userId).Select(MapToDto);
        }

        /// <summary>
        /// Retrieves tasks filtered by status and/or priority with pagination support for a specific user
        /// </summary>
        /// <param name="filter">Filter criteria including status, priority, page number, and page size</param>
        /// <param name="userId">The ID of the user</param>
        /// <returns>A tuple containing the filtered tasks and the total count of matching tasks</returns>
        public async Task<(IEnumerable<TaskResponseDto> Tasks, int TotalCount)> GetFilteredTasksAsync(TaskFilterDto filter, int userId)
        {
            var tasks = await _taskRepository.GetFilteredTasksAsync(
                filter.Status,
                filter.Priority,
                filter.PageNumber,
                filter.PageSize);

            var totalCount = await _taskRepository.GetFilteredTasksCountAsync(
                filter.Status,
                filter.Priority);

            // Filter by userId
            var userTasks = tasks.Where(t => t.UserId == userId);
            var userTotalCount = userTasks.Count();

            return (userTasks.Select(MapToDto), userTotalCount);
        }

        /// <summary>
        /// Updates an existing task with new values (only if it belongs to the user). Only provided fields will be updated.
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to update</param>
        /// <param name="request">The update request containing new values</param>
        /// <param name="userId">The ID of the user updating the task</param>
        /// <returns>The updated task details if successful, otherwise null if task not found or doesn't belong to user</returns>
        public async Task<TaskResponseDto?> UpdateTaskAsync(int taskId, UpdateTaskDto request, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null || task.UserId != userId)
            {
                return null;
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(request.Title))
                task.Title = request.Title;

            if (request.Description != null)
                task.Description = request.Description;

            if (request.DueDate.HasValue)
                task.DueDate = request.DueDate.Value;

            if (request.Priority.HasValue)
                task.Priority = request.Priority.Value;

            if (request.Status.HasValue)
                task.Status = request.Status.Value;

            task.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateAsync(task);
            await _taskRepository.SaveChangesAsync();

            return MapToDto(task);
        }

        /// <summary>
        /// Deletes a task from the system (only if it belongs to the user)
        /// </summary>
        /// <param name="taskId">The unique identifier of the task to delete</param>
        /// <param name="userId">The ID of the user deleting the task</param>
        /// <returns>True if the task was deleted successfully, false if task not found or doesn't belong to user</returns>
        public async Task<bool> DeleteTaskAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null || task.UserId != userId)
            {
                return false;
            }

            await _taskRepository.DeleteAsync(task);
            await _taskRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Maps a TaskItem entity to a TaskResponseDto
        /// </summary>
        /// <param name="task">The task entity to map</param>
        /// <returns>The mapped task response DTO</returns>
        private static TaskResponseDto MapToDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                Status = task.Status,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}
