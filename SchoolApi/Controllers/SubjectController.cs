using Microsoft.AspNetCore.Mvc;
using SchoolApi.Repos;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly DataProtector _protector;
        private readonly SubjectsRepo subjectsRepo;

        public SubjectController(DataProtector protector, SubjectsRepo subjectsRepo)
        {
            _protector = protector;
            this.subjectsRepo = subjectsRepo;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> Get()
        {
            try
            {
                IEnumerable<Subject> Ensubjects = (await subjectsRepo.GetSubjects());
                if (Ensubjects.Any())
                {
                   var DecryptedSubjects = _protector.Decrypt(Ensubjects);
                    return Ok(DecryptedSubjects);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Subject/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Subject>> Get(int id)
        {
            Subject enSubject = await subjectsRepo.GetSubject(id);
            if (enSubject == null)
                return NotFound();

            Subject DeSubjects = (Subject)_protector.Decrypt(enSubject);
            return Ok(DeSubjects);
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<ActionResult<Subject>> Post([FromBody] Subject subject)
        {
            if (subject == null)
                return BadRequest();

            Subject encryptedSubject = (Subject)_protector.Encrypt(subject);
            await subjectsRepo.AddSubject(encryptedSubject);

            return CreatedAtAction(nameof(Get), subject.Id, encryptedSubject);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Subject subject)
        {
            var encryptExistSubject = (Subject)_protector.Encrypt(subject);

            var updatedSubject = await subjectsRepo.UpdateSubject(encryptExistSubject, id);
            if (updatedSubject != null)
                return Ok(_protector.Decrypt(updatedSubject));
            return NotFound();
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Subject endeletedSubject = await subjectsRepo.DeleteSubject(id);

            if (endeletedSubject != null)
                return Ok(_protector.Decrypt(endeletedSubject));

            return NotFound();
        }
    }

}

