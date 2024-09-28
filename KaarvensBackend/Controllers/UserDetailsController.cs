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
        [HttpPost]
        public async Task<IActionResult> Post(UserDetails user)
        {
            _db.UserDetails.Add(user);
            await _db.SaveChangesAsync();
            return Created($"{user.Id}",user);

        }
        [HttpPut]
        public async Task<IActionResult> Put(UserDetails userDetails)
        {
            var updateProject = _mapper.Map<ProjectDetails>(projectDetailsDto);
            _context.ProjectDetails.Update(updateProject);
            await _context.SaveChangesAsync();
            return Ok(updateProject);

        }
    }
}
