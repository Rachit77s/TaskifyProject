using System.ComponentModel.DataAnnotations;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;

namespace TaskifyProject.Models.DTOs.Tasks
{
    /// <summary>
    /// Data transfer object for creating a new task
    /// </summary>
    public class CreateTaskDto
    {
        /// <summary>
        /// The title of the task (1-200 characters)
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Optional detailed description of the task (max 1000 characters)
        /// </summary>
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// The due date and time for the task
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// The priority level of the task (0=Low, 1=Medium, 2=High)
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        public TaskPriorityEnum Priority { get; set; }
    }
}
