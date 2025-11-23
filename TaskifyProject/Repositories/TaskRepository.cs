using Microsoft.EntityFrameworkCore;
using TaskifyProject.Data;
using TaskifyProject.Models.Entities;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Repositories
{
    /// <summary>
    /// Repository implementation for managing TaskItem entities in the database
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the TaskRepository class
        /// </summary>
        /// <param name="context">The database context</param>
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a task by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the task</param>
        /// <returns>The task if found, otherwise null</returns>
        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all tasks from the database ordered by creation date
        /// </summary>
        /// <returns>A collection of all tasks</returns>
        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves tasks filtered by status and/or priority with pagination support
        /// </summary>
        /// <param name="status">Optional filter for task status (Pending/Completed)</param>
        /// <param name="priority">Optional filter for task priority (Low/Medium/High)</param>
        /// <param name="pageNumber">The page number to retrieve (1-based)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <returns>A paginated collection of filtered tasks</returns>
        public async Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(
            TaskStatusEnum? status, 
            TaskPriorityEnum? priority, 
            int pageNumber, 
            int pageSize)
        {
            var query = _context.Tasks.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }

            return await query
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the total count of tasks matching the specified filter criteria
        /// </summary>
        /// <param name="status">Optional filter for task status</param>
        /// <param name="priority">Optional filter for task priority</param>
        /// <returns>The total number of tasks matching the criteria</returns>
        public async Task<int> GetFilteredTasksCountAsync(TaskStatusEnum? status, TaskPriorityEnum? priority)
        {
            var query = _context.Tasks.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority.Value);
            }

            return await query.CountAsync();
        }

        /// <summary>
        /// Adds a new task to the database context
        /// </summary>
        /// <param name="task">The task entity to add</param>
        /// <returns>The added task entity</returns>
        public async Task<TaskItem> AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            return task;
        }

        /// <summary>
        /// Updates an existing task in the database context
        /// </summary>
        /// <param name="task">The task entity with updated values</param>
        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Removes a task from the database context
        /// </summary>
        /// <param name="task">The task entity to delete</param>
        public async Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Persists all pending changes to the database
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
