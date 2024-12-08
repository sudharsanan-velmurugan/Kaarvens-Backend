using KaarvensBackend.Database;
using KaarvensBackend.DTOS;
using KaarvensBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaarvensBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        // Constructor to initialize the database context
        public UserDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Retrieves all user details.
        /// </summary>
        /// <returns>Returns a list of all users with HTTP 200 OK status.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Fetch all user details from the database
            var userDetails = await _db.UserDetails.ToListAsync();

            // Return the user details as an HTTP 200 OK response
            return Ok(userDetails);
        }

        /// <summary>
        /// Retrieves details of a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Returns the user details with HTTP 200 OK, or HTTP 404 Not Found if the user does not exist.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Fetch user details by the specified ID
            var userDetail = await _db.UserDetails.FirstOrDefaultAsync(x => x.Id == id);

            // Return the user details as an HTTP 200 OK response
            return Ok(userDetail);
        }

        /// <summary>
        /// Creates a new user entry in the database.
        /// </summary>
        /// <param name="user">The user details object to be created.</param>
        /// <returns>Returns the created user details with HTTP 201 Created status.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(UserDetails user)
        {
            // Add the new user to the database
            _db.UserDetails.Add(user);
            await _db.SaveChangesAsync();

            // Return the created user details with a 201 Created status
            return Created($"/UserDetails/{user.Id}", user);
        }

        /// <summary>
        /// Updates an existing user's details.
        /// </summary>
        /// <param name="userDetails">The user details object with updated data.</param>
        /// <returns>Returns the updated user details with HTTP 200 OK, or HTTP 404 Not Found if the user does not exist.</returns>
        [HttpPut]
        public async Task<IActionResult> Put(UserDetails userDetails)
        {
            // Find the user to update by their ID
            var updateUser = await _db.UserDetails.FindAsync(userDetails.Id);

            // If the user is not found, return a 404 Not Found response
            if (updateUser == null)
            {
                return NotFound();
            }

            // Update the user's details
            updateUser.Id = userDetails.Id;
            updateUser.FirstName = userDetails.FirstName;
            updateUser.LastName = userDetails.LastName;
            updateUser.Email = userDetails.Email;
            updateUser.MobileNo = userDetails.MobileNo;

            // Save the changes to the database
            await _db.SaveChangesAsync();

            // Return the updated user details as an HTTP 200 OK response
            return Ok(updateUser);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Returns HTTP 204 No Content if successful, or HTTP 404 Not Found if the user does not exist.</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the user to delete by their ID
            var user = await _db.UserDetails.FirstOrDefaultAsync(x => x.Id == id);

            // If the user is not found, return a 404 Not Found response
            if (user == null)
            {
                return NotFound();
            }

            // Remove the user from the database
            _db.UserDetails.Remove(user);
            await _db.SaveChangesAsync();

            // Return a 204 No Content response to indicate successful deletion
            return NoContent();
        }
    }
}
