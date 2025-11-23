using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Models.DTOs.Tasks
{
    /// <summary>
    /// Data transfer object for filtering and paginating task queries
    /// </summary>
    public class TaskFilterDto
    {
        /// <summary>
        /// Optional filter by task status (0=Pending, 1=Completed)
        /// </summary>
        public TaskStatusEnum? Status { get; set; }

        /// <summary>
        /// Optional filter by task priority (0=Low, 1=Medium, 2=High)
        /// </summary>
        public TaskPriorityEnum? Priority { get; set; }

        /// <summary>
        /// The page number to retrieve (1-based, default: 1)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The number of items per page (default: 10)
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
