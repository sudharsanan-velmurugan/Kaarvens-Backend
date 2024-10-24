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
        public FinanceDetailsController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _db.FinanceDetails.ToListAsync();

            return Ok(result);
        }
        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var taskDetail = await _db.FinanceDetails.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(taskDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Post(FinanceDetails financeDetails)
        {
            _db.FinanceDetails.Add(financeDetails);
            await _db.SaveChangesAsync();
            return Created($"/TaskDetails/{financeDetails.Id}", financeDetails);
        }

        [HttpPut]
        public async Task<IActionResult> Put(FinanceDetails financeDetails)
        {
            var updatedFD = await _db.FinanceDetails.FindAsync(financeDetails.Id);

            if (updatedFD == null)
            {
                return NotFound();
            }
            updatedFD.Id = financeDetails.Id;
            updatedFD.ProjectName = financeDetails.ProjectName;
            updatedFD.Status = financeDetails.Status;
            updatedFD.Comments = financeDetails.Comments;


            await _db.SaveChangesAsync();
            return Ok(updatedFD);

        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var financeDetail = await _db.FinanceDetails.FirstOrDefaultAsync(x => x.Id == id);
            if (financeDetail == null)
            {
                return NotFound();
            }
            _db.FinanceDetails.Remove(financeDetail);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}


