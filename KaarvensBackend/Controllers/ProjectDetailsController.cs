using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KaarvensBackend.Database;
using KaarvensBackend.DTOS;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;

namespace KaarvensBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectDetailsController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of all project details, including their associated drawing details.
        /// </summary>
        /// <returns>Returns a list of all project details.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projectDetails = await _context.ProjectDetails
                .Include(_=>_.DrawingDetails)
                .ToListAsync();

            return Ok( projectDetails);

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var projectDetails = await _context.ProjectDetails
                .Include(_ => _.DrawingDetails)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return Ok(projectDetails);

        }
        // <summary>
        /// Creates a new project detail from the provided DTO.
        /// </summary>
        /// <param name="projectDetailsDto">The DTO containing project detail information.</param>
        /// <returns>Returns the created project detail with a 201 Created status.</returns>

        [HttpPost]
        public async Task<IActionResult> Post(ProjectDetailsDto projectDetailsDto)
        {
            var result = _mapper.Map<ProjectDetails>(projectDetailsDto);
            _context.ProjectDetails.Add(result);
            await _context.SaveChangesAsync();
            return Created($"/ProjectDetaills/{result.Id}",result);
        }

        /// <summary>
        /// Updates an existing project detail with the data from the provided DTO.
        /// </summary>
        /// <param name="projectDetailsDto">The DTO containing updated project detail information.</param>
        /// <returns>Returns the updated project detail with a 200 OK status.</returns>
        [HttpPut]
        public async Task<IActionResult>Put (ProjectDetailsDto projectDetailsDto)
        {
            var updateProject = _mapper.Map<ProjectDetails>(projectDetailsDto);
            _context.ProjectDetails.Update(updateProject);
            await _context.SaveChangesAsync();
            return Ok(updateProject);

        }

        /// <summary>
        /// Deletes a specific project detail by its ID.
        /// </summary>
        /// <param name="id">The ID of the project detail to delete.</param>
        /// <returns>Returns a 204 No Content status if the project detail is successfully deleted, or 404 Not Found if it doesn't exist.</returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>Delete(int id)
        {
            var projectToDelete = await _context.ProjectDetails.Include(_ => _.DrawingDetails)
                .Where(_ => _.Id == id).FirstOrDefaultAsync();

            if(projectToDelete == null)
            {
                return NotFound();
            }
            _context.ProjectDetails.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
