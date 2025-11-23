using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;

namespace TaskifyProject.Models.Entities
{
    /// <summary>
    /// Entity representing a task in the task management system
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the task (max 200 characters)
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the task (max 1000 characters)
        /// </summary>
        [MaxLength(1000)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the due date and time for the task
        /// </summary>
        [Required]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the priority level of the task (Low=0, Medium=1, High=2)
        /// </summary>
        [Required]
        public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;

        /// <summary>
        /// Gets or sets the current status of the task (Pending=0, Completed=1)
        /// </summary>
        [Required]
        public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;

        /// <summary>
        /// Gets or sets the date and time when the task was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the task was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who created this task
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Navigation property to the user who created this task
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
