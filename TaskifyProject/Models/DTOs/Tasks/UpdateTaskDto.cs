using System.ComponentModel.DataAnnotations;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Models.DTOs.Tasks
{
    /// <summary>
    /// Data transfer object for updating an existing task. All fields are optional.
    /// </summary>
    public class UpdateTaskDto
    {
        /// <summary>
        /// The updated title of the task (1-200 characters, optional)
        /// </summary>
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string? Title { get; set; }

        /// <summary>
        /// The updated description of the task (max 1000 characters, optional)
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// The updated due date and time for the task (optional)
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// The updated priority level (0=Low, 1=Medium, 2=High, optional)
        /// </summary>
        public TaskPriorityEnum? Priority { get; set; }

        /// <summary>
        /// The updated status (0=Pending, 1=Completed, optional)
        /// </summary>
        public TaskStatusEnum? Status { get; set; }
    }
}
