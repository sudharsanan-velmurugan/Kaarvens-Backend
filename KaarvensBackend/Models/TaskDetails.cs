using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KaarvensBackend.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetails
    {
        [Key] // This will be the primary key of the table
        public int TaskId { get; set; } // Unique identifier for each task

        [Required(ErrorMessage = "Task Name is required")]
        public string TaskName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Task Owner is required")]
        public string TaskOwner { get; set; } = string.Empty;

        [Required(ErrorMessage = "Assignee is required")]
        public string Assignee { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; } // Start date of the task

        [Required(ErrorMessage = "Due Date is required")]
        public DateTime DueDate { get; set; } // Due date of the task

        [Required(ErrorMessage = "Status is required")]
        [MaxLength(50)] // Task status, such as "In Progress", "Completed", etc.
        public string Status { get; set; } = string.Empty;

        [MaxLength(500)] // Limit comments to 500 characters
        public string Comments { get; set; } = string.Empty; // Optional comments for the task
    }
}
