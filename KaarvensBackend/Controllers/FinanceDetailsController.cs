using KaarvensBackend.Database;
using KaarvensBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaarvensBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        // Constructor to initialize the database context
        public FinanceDetailsController(ApplicationDbContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Retrieves all financial details from the database.
        /// </summary>
        /// <returns>Returns a list of all finance records with HTTP 200 OK status.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Fetch all finance records from the database
            var result = await _db.FinanceDetails.ToListAsync();

            // Return the fetched records as an HTTP 200 OK response
            return Ok(result);
        }

        /// <summary>
        /// Retrieves specific financial details by ID.
        /// </summary>
        /// <param name="id">The ID of the finance record to retrieve.</param>
        /// <returns>Returns the finance record with HTTP 200 OK, or HTTP 404 Not Found if not found.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Fetch a specific finance record by ID
            var taskDetail = await _db.FinanceDetails.FirstOrDefaultAsync(x => x.Id == id);

            // Return the finance record as an HTTP 200 OK response
            return Ok(taskDetail);
        }

        /// <summary>
        /// Adds a new finance record to the database.
        /// </summary>
        /// <param name="financeDetails">The finance details to add.</param>
        /// <returns>Returns the created record with HTTP 201 Created status.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(FinanceDetails financeDetails)
        {
            // Add the new finance record to the database
            _db.FinanceDetails.Add(financeDetails);
            await _db.SaveChangesAsync();

            // Return the created finance record with a 201 Created status
            return Created($"/FinanceDetails/{financeDetails.Id}", financeDetails);
        }

        /// <summary>
        /// Updates an existing finance record.
        /// </summary>
        /// <param name="financeDetails">The updated finance details.</param>
        /// <returns>Returns the updated record with HTTP 200 OK, or HTTP 404 Not Found if not found.</returns>
        [HttpPut]
        public async Task<IActionResult> Put(FinanceDetails financeDetails)
        {
            // Find the existing finance record by ID
            var updatedFD = await _db.FinanceDetails.FindAsync(financeDetails.Id);

            // If the record is not found, return a 404 Not Found response
            if (updatedFD == null)
            {
                return NotFound();
            }

            // Update the record's properties
            updatedFD.Id = financeDetails.Id;
            updatedFD.ProjectName = financeDetails.ProjectName;
            updatedFD.Status = financeDetails.Status;
            updatedFD.Comments = financeDetails.Comments;

            // Save the changes to the database
            await _db.SaveChangesAsync();

            // Return the updated record as an HTTP 200 OK response
            return Ok(updatedFD);
        }

        /// <summary>
        /// Deletes a finance record by ID.
        /// </summary>
        /// <param name="id">The ID of the finance record to delete.</param>
        /// <returns>Returns HTTP 204 No Content if successful, or HTTP 404 Not Found if the record does not exist.</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the finance record to delete by ID
            var financeDetail = await _db.FinanceDetails.FirstOrDefaultAsync(x => x.Id == id);

            // If the record is not found, return a 404 Not Found response
            if (financeDetail == null)
            {
                return NotFound();
            }

            // Remove the finance record from the database
            _db.FinanceDetails.Remove(financeDetail);
            await _db.SaveChangesAsync();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }
    }
}
