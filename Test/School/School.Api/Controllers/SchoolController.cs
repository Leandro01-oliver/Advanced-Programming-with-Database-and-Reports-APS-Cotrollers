using Microsoft.AspNetCore.Mvc;
using School.Core.Entities;
using School.Core.Repositories;

namespace School.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetAllAsync()
        {
            var schools = await schoolRepository.GetAllAsync();
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetByIdAsync(long id)
        {
            var school = await schoolRepository.GetByIdAsync(id);
            if (schools == null) return NotFound();
            return ok(schools);
        }

        [HttpPost]
        public async Task<ActionResult<School>> AddAsync(School school)
        {
            if (ModelState.IsValid)
            {
                await schoolRepository.AddAsync(school);
                await schoolRepository.SaveAsync();
                return CreatedAtAction(nameof(GetByIdAsync),
                new { Id = school.Id },
                school);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(long id, School school)
        {
            if (ModelState.IsValid && id == school.Id)
            {
                if (!(await schoolRepository.Exists(id)))
                {
                    return NotFound();
                    schoolRepository.Update (school);
                    await schoolRepository.SaveAsync();
                }
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            if (!(await schoolRepository.Exists(id)))
                return NotFound();
            await schoolRepository.DeleteAsync(id);
            await schoolRepository.SaveAsync();
            return NoContent();
        }
    }
}
