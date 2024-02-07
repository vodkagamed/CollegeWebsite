using AutoMapper;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController : ControllerBase
    {
        //private readonly DataProtector _protector;
        private readonly CollegesRepo collegesRepo;

        public CollegeController
            (DataProtector protector, CollegesRepo collegesRepo, IMapper mapper)
        {
            //_protector = protector;
            this.collegesRepo = collegesRepo;
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> Get()
        {
            try
            {
                List<College> Encolleges = (await collegesRepo.GetColleges()).ToList();
                if (Encolleges.Any())
                    return Ok(Encolleges);

                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/College/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<College>> Get(Guid id)
        {
            College college = await collegesRepo.GetCollege(id);
            if (college == null)
                return NotFound();

            return Ok(college);
        }

        // POST: api/College
        [HttpPost]
        public async Task<ActionResult<College>> Post([FromBody] College college)
        {
            if (college == null)
                return BadRequest();
            //College encryptedCollege = (College)_protector.Encrypt(college);
            await collegesRepo.AddCollege(college);

            return CreatedAtAction(nameof(Get), college.Id, college);
        }

        // PUT: api/College/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] College college)
        {
            //var encryptExistCollege = (College)_protector.Encrypt(college);

            var updatedCollege = await collegesRepo.UpdateCollege(college, id);
            if (updatedCollege != null)
                return Ok(updatedCollege);
            return NotFound();
        }

        // DELETE: api/College/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            College endeletedCollege = (College)await collegesRepo.DeleteCollege(id);

            if (endeletedCollege != null)
                return Ok(endeletedCollege);

            return NotFound();
        }
    }
}
