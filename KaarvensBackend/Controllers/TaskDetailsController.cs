using KaarvensBackend.Database;
using KaarvensBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaarvensBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public TaskDetailsController(ApplicationDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Retrieves a list of all task details.
        /// </summary>
        /// <returns>Returns an HTTP 200 OK status with a list of all task details.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _db.TaskDetails.ToListAsync();

            return Ok(result);
        }
        /// <summary>
        /// Retrieves a specific task detail by its TaskId.
        /// </summary>
        /// <param name="id">The ID of the task to retrieve.</param>
        /// <returns>Returns the task detail if found, or HTTP 404 Not Found if not.</returns>
        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult>Get(int id)
        {
            var taskDetail = await _db.TaskDetails.FirstOrDefaultAsync(x=>x.TaskId== id);
            return Ok(taskDetail);
        }

        /// <summary>
        /// Creates a new task detail.
        /// </summary>
        /// <param name="taskDetails">The task detail object to be created.</param>
        /// <returns>Returns the created task detail with an HTTP 201 Created status.</returns>
        [HttpPost]
        public async Task<IActionResult>Post(TaskDetails taskDetails)
        {
            _db.TaskDetails.Add(taskDetails);
            await _db.SaveChangesAsync();
            return Created($"/TaskDetails/{taskDetails.TaskId}", taskDetails);
        }

        /// <summary>
        /// Updates an existing task detail.
        /// </summary>
        /// <param name="taskDetails">The task detail object with updated data.</param>
        /// <returns>Returns the updated task detail with an HTTP 200 OK status, or HTTP 404 Not Found if the task doesn't exist.</returns>
        [HttpPut]
        public async Task<IActionResult> Put (TaskDetails taskDetails)
        {
            var updatedTask = await _db.TaskDetails.FindAsync(taskDetails.TaskId);

            if(updatedTask == null)
            {
                return NotFound();
            }
            updatedTask.TaskId = taskDetails.TaskId;
            updatedTask.TaskName = taskDetails.TaskName;
            updatedTask.TaskOwner= taskDetails.TaskOwner;
            updatedTask.Assignee = taskDetails.Assignee;
            updatedTask.StartDate = taskDetails.StartDate;
            updatedTask.DueDate = taskDetails.DueDate;
            updatedTask.Status = taskDetails.Status;
            updatedTask.Comments= taskDetails.Comments;

            await _db.SaveChangesAsync();
            return Ok(updatedTask);

        }

        /// <summary>
        /// Deletes a specific task detail by its TaskId.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>Returns an HTTP 204 No Content status if successful, or HTTP 404 Not Found if the task doesn't exist.</returns>
        [HttpDelete]

        public async Task<IActionResult>Delete(int id)
        {
            var taskDetail = await _db.TaskDetails.FirstOrDefaultAsync(x=>x.TaskId == id);  
            if(taskDetail == null)
            {
                return NotFound();
            }
             _db.TaskDetails.Remove(taskDetail);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
