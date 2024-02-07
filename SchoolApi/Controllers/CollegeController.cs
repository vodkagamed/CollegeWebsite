﻿using AutoMapper;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController : ControllerBase
    {
        //private readonly DataProtector _protector;
        private readonly CollegesRepo collegesRepo;

        private List<string> customEncludes = new();

        public CollegeController
            (DataProtector protector, CollegesRepo collegesRepo, IMapper mapper)
        {
            this.collegesRepo = collegesRepo;
        
            customEncludes.Append("Teachers");
            customEncludes.Append("Students");
            customEncludes.Append("Cources");
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> Get()
        {
                List<College> colleges =
                    (
                    (await collegesRepo.GetAllWithIncludesAsync(customEncludes))
                    .ToList()
                    );
            return colleges;
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        public async Task<ActionResult<College>> Get(Guid id)
        {
            College college = await collegesRepo.GetByIdWithIncludesAsync(id,customEncludes);

            return Ok(college);
        }

        // POST: api/College
        [HttpPost]
        public async Task<ActionResult<College>> Post([FromBody] College college)
        {
            if (college == null)
                return BadRequest();
            //College encryptedCollege = (College)_protector.Encrypt(college);
            await collegesRepo.AddAsync(college);

            return CreatedAtAction(nameof(Get), college.Id, college);
        }

        // PUT: api/College/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] College college)
        {
            //var encryptExistCollege = (College)_protector.Encrypt(college);

            var updatedCollege = await collegesRepo.UpdateAsync(college);
            if (updatedCollege != null)
                return Ok(updatedCollege);
            return NotFound();
        }

        // DELETE: api/College/5
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
