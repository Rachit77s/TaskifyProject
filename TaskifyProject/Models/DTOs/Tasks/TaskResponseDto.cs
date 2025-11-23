using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Models.DTOs.Tasks
{
    /// <summary>
    /// Data transfer object for task response containing all task details
    /// </summary>
    public class TaskResponseDto
    {
        /// <summary>
        /// The unique identifier of the task
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the task
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The detailed description of the task
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The due date and time for the task
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The priority level of the task (0=Low, 1=Medium, 2=High)
        /// </summary>
        public TaskPriorityEnum Priority { get; set; }

        /// <summary>
        /// The current status of the task (0=Pending, 1=Completed)
        /// </summary>
        public TaskStatusEnum Status { get; set; }

        /// <summary>
        /// The date and time when the task was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the task was last updated (null if never updated)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
