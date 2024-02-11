using AutoMapper;
using CollegeApi.ActionFilters;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController : ControllerBase
    {
        private readonly CollegeRepo collegesRepo;

        private List<string> Includes = new();

        public CollegeController
            (DataProtector protector, CollegeRepo collegesRepo)
        {
            this.collegesRepo = collegesRepo;
            Includes.Add($"{nameof(College.Courses)}");
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> Get()
        {
            List<College> colleges =
                (await collegesRepo.GetAllWithIncludesAsync(Includes.ToArray())).ToList();
            return colleges;
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        [ServiceFilter(typeof(RequestCheck<College>))]
        public async Task<ActionResult<College>> Get(Guid id)
        {
            College college = await collegesRepo.GetByIdWithIncludesAsync(id,Includes.ToArray());

            return Ok(college);
        }

        [HttpPost]
        public async Task<ActionResult<College>> Post([FromBody] College college)
        {
            if (college == null)
                return BadRequest();
            await collegesRepo.AddAsync(college);

            return CreatedAtAction(nameof(Get), college.Id, college);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] College college)
        {
            var updatedCollege = await collegesRepo.UpdateAsync(college);
            if (updatedCollege != null)
                return Ok(updatedCollege);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            College endeletedCollege = (College)await collegesRepo.DeleteAsync(id);

            if (endeletedCollege != null)
                return Ok(endeletedCollege);

            return NotFound();
        }
    }
}
