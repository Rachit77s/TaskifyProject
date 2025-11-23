using TaskifyProject.Models.Entities;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Repositories
{
    /// <summary>
    /// Repository interface for managing TaskItem entities
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Retrieves a task by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the task</param>
        /// <returns>The task if found, otherwise null</returns>
        Task<TaskItem?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all tasks from the database
        /// </summary>
        /// <returns>A collection of all tasks ordered by creation date descending</returns>
        Task<IEnumerable<TaskItem>> GetAllAsync();

        /// <summary>
        /// Retrieves tasks filtered by status and/or priority with pagination
        /// </summary>
        /// <param name="status">Optional filter for task status (Pending/Completed)</param>
        /// <param name="priority">Optional filter for task priority (Low/Medium/High)</param>
        /// <param name="pageNumber">The page number to retrieve (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A collection of filtered and paginated tasks</returns>
        Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(TaskStatusEnum? status, TaskPriorityEnum? priority, int pageNumber, int pageSize);

        /// <summary>
        /// Gets the total count of tasks matching the filter criteria
        /// </summary>
        /// <param name="status">Optional filter for task status</param>
        /// <param name="priority">Optional filter for task priority</param>
        /// <returns>The total number of tasks matching the criteria</returns>
        Task<int> GetFilteredTasksCountAsync(TaskStatusEnum? status, TaskPriorityEnum? priority);

        /// <summary>
        /// Adds a new task to the database
        /// </summary>
        /// <param name="task">The task entity to add</param>
        /// <returns>The added task entity</returns>
        Task<TaskItem> AddAsync(TaskItem task);

        /// <summary>
        /// Updates an existing task in the database
        /// </summary>
        /// <param name="task">The task entity with updated values</param>
        Task UpdateAsync(TaskItem task);

        /// <summary>
        /// Deletes a task from the database
        /// </summary>
        /// <param name="task">The task entity to delete</param>
        Task DeleteAsync(TaskItem task);

        /// <summary>
        /// Saves all pending changes to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        Task<int> SaveChangesAsync();
    }
}
