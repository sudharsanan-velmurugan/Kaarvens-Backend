using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KaarvensBackend.Database;
using KaarvensBackend.DTOS;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using Microsoft.AspNetCore.Components;

namespace KaarvensBackend.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projectDetails = await _context.ProjectDetails
                .Include(_=>_.DrawingDetails)
                .ToListAsync();

            return Ok( projectDetails);

        }
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var projectDetails = await _context.ProjectDetails
                .Include(_ => _.DrawingDetails)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return Ok(projectDetails);

        }


        //private ProjectDetails MapProjectObject(ProjectDetailsDto projectDetailsDto)
        //{
        //    var result = new ProjectDetails();
        //    result.JobNo= projectDetailsDto.JobNo;
        //    result.ProjectName= projectDetailsDto.ProjectName;
        //    result.ArchitectName= projectDetailsDto.ArchitectName;
        //    result.SiteLocation= projectDetailsDto.SiteLocation;
        //    result.DrawingDetails = new List<DrawingDetails>();

        //    foreach (var item in projectDetailsDto.DrawingDetails)
        //    {
        //        var newDrawingDetails = new DrawingDetails();
        //        newDrawingDetails.DrawingStatus= item.DrawingStatus;
        //        newDrawingDetails.DrawingName   = item.DrawingName;
        //        newDrawingDetails.Revision= item.Revision;
        //        result.DrawingDetails.Add(newDrawingDetails);
        //    }

        //    return result;
        //}


        [HttpPost]
        public async Task<IActionResult> Post(ProjectDetailsDto projectDetailsDto)
        {
            var result = _mapper.Map<ProjectDetails>(projectDetailsDto);
            _context.ProjectDetails.Add(result);
            await _context.SaveChangesAsync();
            return Created($"/ProjectDetaills/{result.Id}",result);
        }
        [HttpPut]
        public async Task<IActionResult>Put (ProjectDetailsDto projectDetailsDto)
        {
            var updateProject = _mapper.Map<ProjectDetails>(projectDetailsDto);
            _context.ProjectDetails.Update(updateProject);
            await _context.SaveChangesAsync();
            return Ok(updateProject);

        }
        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("{id:int}")]
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
