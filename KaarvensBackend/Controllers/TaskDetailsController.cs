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
        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult>Get(int id)
        {
            var taskDetail = await _db.TaskDetails.FirstOrDefaultAsync(x=>x.TaskId== id);
            return Ok(taskDetail);
        }
        [HttpPost]
        public async Task<IActionResult>Post(TaskDetails taskDetails)
        {
            _db.TaskDetails.Add(taskDetails);
            await _db.SaveChangesAsync();
            return Created($"/TaskDetails/{taskDetails.TaskId}", taskDetails);
        }

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
