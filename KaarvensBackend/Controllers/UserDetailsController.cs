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
        public UserDetailsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userDetails = await _db.UserDetails.ToListAsync();
            return Ok(userDetails);

        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userDetail = await _db.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(userDetail);

        }
        [HttpPost]
        public async Task<IActionResult> Post(UserDetails user)
        {
            _db.UserDetails.Add(user);
            await _db.SaveChangesAsync();
            return Created($"/UserDetails/{user.Id}",user);

        }
        [HttpPut]
        public async Task<IActionResult> Put(UserDetails userDetails)
        {
            var updateUser = await _db.UserDetails.FindAsync(userDetails.Id);
            if(updateUser == null)
            {
                return NotFound();
            }

            updateUser.Id = userDetails.Id;
            updateUser.FirstName = userDetails.FirstName;
            updateUser.LastName = userDetails.LastName;
            updateUser.Email = userDetails.Email;
            updateUser.MobileNo = userDetails.MobileNo;
            await _db.SaveChangesAsync();
            return Ok(updateUser);

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            _db.UserDetails.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
