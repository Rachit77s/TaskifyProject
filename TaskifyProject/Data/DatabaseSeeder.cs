using TaskifyProject.Models.Entities;
using TaskPriorityEnum = TaskifyProject.Models.Enums.TaskPriority;
using TaskStatusEnum = TaskifyProject.Models.Enums.TaskStatus;
using BCrypt.Net;

namespace TaskifyProject.Data
{
    /// <summary>
    /// Service for seeding initial data into the database
    /// </summary>
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseSeeder> _logger;

        /// <summary>
        /// Initializes a new instance of the DatabaseSeeder class
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="logger">The logger for recording seeding operations</param>
        public DatabaseSeeder(ApplicationDbContext context, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Seeds the database with initial sample data if it's empty
        /// </summary>
        /// <returns>A task representing the asynchronous operation</returns>
        public async Task SeedAsync()
        {
            try
            {
                // Check if database has any users
                if (_context.Users.Any())
                {
                    _logger.LogInformation("Database already contains data. Skipping seed.");
                    return;
                }

                _logger.LogInformation("Starting database seeding...");

                // Create test users first
                var users = GetSeedUsers();
                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created {Count} test users.", users.Count);

                // Create tasks for each user
                var allTasks = new List<TaskItem>();

                // Get user IDs
                var rachit = users.First(u => u.Username == "rachit");
                var testUser = users.First(u => u.Username == "testuser");
                var demo = users.First(u => u.Username == "demo");

                // Add tasks for Rachit (6 tasks - 3 pending, 3 completed)
                allTasks.AddRange(GetRachitTasks(rachit.Id));

                // Add tasks for Test User (4 tasks - 2 pending, 2 completed)
                allTasks.AddRange(GetTestUserTasks(testUser.Id));

                // Add tasks for Demo (4 tasks - 2 pending, 2 completed)
                allTasks.AddRange(GetDemoTasks(demo.Id));

                await _context.Tasks.AddRangeAsync(allTasks);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Database seeded successfully with {UserCount} users and {TaskCount} tasks.", 
                    users.Count, allTasks.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        /// <summary>
        /// Creates a collection of test users for seeding
        /// </summary>
        /// <returns>A list of sample User entities</returns>
        private static List<User> GetSeedUsers()
        {
            return new List<User>
            {
                new User
                {
                    Username = "rachit",
                    Email = "rachit@taskify.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    FirstName = "Rachit",
                    LastName = "Srivastava",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Username = "testuser",
                    Email = "test@taskify.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test@123"),
                    FirstName = "Test",
                    LastName = "User",
                    Role = "User",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Username = "demo",
                    Email = "demo@taskify.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Demo@123"),
                    FirstName = "Demo",
                    LastName = "Account",
                    Role = "User",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }

        /// <summary>
        /// Creates 6 tasks for Rachit (Admin) - 3 pending, 3 completed
        /// </summary>
        private static List<TaskItem> GetRachitTasks(int userId)
        {
            var baseDate = DateTime.UtcNow;

            return new List<TaskItem>
            {
                new TaskItem
                {
                    Title = "Complete Project Documentation",
                    Description = "Write comprehensive documentation for the Task Management API including setup instructions and usage examples.",
                    DueDate = baseDate.AddDays(30),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-5),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Design Database Schema",
                    Description = "Create Entity-Relationship diagram and design the database schema for optimal performance.",
                    DueDate = baseDate.AddDays(15),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-10),
                    UpdatedAt = baseDate.AddDays(-2),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Implement User Authentication",
                    Description = "Add JWT-based authentication and authorization to secure the API endpoints.",
                    DueDate = baseDate.AddDays(45),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-3),
                    UpdatedAt = baseDate,
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Setup CI/CD Pipeline",
                    Description = "Configure automated build, test, and deployment pipeline using GitHub Actions.",
                    DueDate = baseDate.AddDays(60),
                    Priority = TaskPriorityEnum.Medium,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-4),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Write Unit Tests",
                    Description = "Create comprehensive unit tests for repositories, services, and controllers.",
                    DueDate = baseDate.AddDays(20),
                    Priority = TaskPriorityEnum.Medium,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-6),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Build Frontend UI",
                    Description = "Develop the React frontend application with responsive design.",
                    DueDate = baseDate.AddDays(90),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-2),
                    UpdatedAt = baseDate.AddDays(-1),
                    UserId = userId
                }
            };
        }

        /// <summary>
        /// Creates 4 tasks for Test User - 2 pending, 2 completed
        /// </summary>
        private static List<TaskItem> GetTestUserTasks(int userId)
        {
            var baseDate = DateTime.UtcNow;

            return new List<TaskItem>
            {
                new TaskItem
                {
                    Title = "Learn React Hooks",
                    Description = "Study and practice using React hooks like useState, useEffect, and useContext.",
                    DueDate = baseDate.AddDays(14),
                    Priority = TaskPriorityEnum.Medium,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-3),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Complete TypeScript Course",
                    Description = "Finish the TypeScript fundamentals course on online learning platform.",
                    DueDate = baseDate.AddDays(21),
                    Priority = TaskPriorityEnum.Low,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-7),
                    UpdatedAt = baseDate.AddDays(-1),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Build Portfolio Website",
                    Description = "Create a personal portfolio website showcasing projects and skills.",
                    DueDate = baseDate.AddDays(45),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-2),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Practice LeetCode Problems",
                    Description = "Solve 50 medium-level algorithm problems to prepare for interviews.",
                    DueDate = baseDate.AddDays(30),
                    Priority = TaskPriorityEnum.Medium,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-8),
                    UpdatedAt = baseDate.AddDays(-3),
                    UserId = userId
                }
            };
        }

        /// <summary>
        /// Creates 4 tasks for Demo - 2 pending, 2 completed
        /// </summary>
        private static List<TaskItem> GetDemoTasks(int userId)
        {
            var baseDate = DateTime.UtcNow;

            return new List<TaskItem>
            {
                new TaskItem
                {
                    Title = "Prepare Product Demo",
                    Description = "Create presentation slides and demo scenarios for stakeholder review meeting.",
                    DueDate = baseDate.AddDays(5),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-2),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Update Marketing Materials",
                    Description = "Refresh brochures and website content with latest product features.",
                    DueDate = baseDate.AddDays(10),
                    Priority = TaskPriorityEnum.Medium,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-5),
                    UpdatedAt = baseDate.AddDays(-1),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Conduct User Testing",
                    Description = "Organize and run user testing sessions to gather feedback on new features.",
                    DueDate = baseDate.AddDays(20),
                    Priority = TaskPriorityEnum.High,
                    Status = TaskStatusEnum.Pending,
                    CreatedAt = baseDate.AddDays(-3),
                    UserId = userId
                },
                new TaskItem
                {
                    Title = "Client Onboarding Session",
                    Description = "Complete onboarding training for new client team members.",
                    DueDate = baseDate.AddDays(7),
                    Priority = TaskPriorityEnum.Low,
                    Status = TaskStatusEnum.Completed,
                    CreatedAt = baseDate.AddDays(-6),
                    UpdatedAt = baseDate.AddDays(-2),
                    UserId = userId
                }
            };
        }
    }
}
