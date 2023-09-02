using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Controllers.DTOs.Encoming;
using SchoolApi.Controllers.DTOs.Outcoming;
using SchoolApi.Repos;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollegeController : ControllerBase
    {
        //private readonly DataProtector _protector;
        private readonly CollegesRepo collegesRepo;
        private readonly IMapper mapper;

        public CollegeController
            (DataProtector protector, CollegesRepo collegesRepo,IMapper mapper)
        {
            //_protector = protector;
            this.collegesRepo = collegesRepo;
            this.mapper = mapper;
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> Get()
        {
            try
            {
                List<College> Encolleges = (await collegesRepo.GetColleges()).ToList();
                if (Encolleges.Any())
                {
                    //var DecryptedColleges = _protector.Decrypt(Encolleges);
                    var collegesRs = Encolleges.Select(c => mapper.Map<CollegeResponse>(c));
                    return Ok(Encolleges);
                }
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest() ;
            }
        }

        // GET: api/College/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<College>> Get(int id)
        {
            College college = await collegesRepo.GetCollege(id);
            if (college == null)
                return NotFound();
            var collegeRs = mapper.Map<CollegeResponse>(college);
            //College DeColleges = (College)_protector.Decrypt(enCollege);
            return Ok(collegeRs);
        }

        // POST: api/College
        [HttpPost]
        public async Task<ActionResult<College>> Post([FromBody] CollegeCreateDTO collegeDTO)
        {
            if (collegeDTO == null)
                return BadRequest();
            var college = mapper.Map<College>(collegeDTO);
            //College encryptedCollege = (College)_protector.Encrypt(college);
            await collegesRepo.AddCollege(college);

            return CreatedAtAction(nameof(Get), college.Id, college);
        }

        // PUT: api/College/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] College college)
        {
            //var encryptExistCollege = (College)_protector.Encrypt(college);

            var updatedCollege = await collegesRepo.UpdateCollege(college, id);
            if (updatedCollege != null)
                return Ok(updatedCollege);
            return NotFound();
        }

        // DELETE: api/College/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            College endeletedCollege = (College)await collegesRepo.DeleteCollege(id);

            if (endeletedCollege != null)
                return Ok(endeletedCollege);

            return NotFound();
        }
    }
}
