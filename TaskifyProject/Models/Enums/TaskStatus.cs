namespace TaskifyProject.Models.Enums
{
    /// <summary>
    /// Represents the current status of a task
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// Task is pending and not yet completed (default)
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Task has been completed
        /// </summary>
        Completed = 1
    }
}
