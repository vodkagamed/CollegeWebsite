using Microsoft.AspNetCore.Mvc;
using SchoolApi.Repos;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        //private readonly DataProtector _protector;
        private readonly CoursesRepo subjectsRepo;

        public CourseController(DataProtector protector, CoursesRepo subjectsRepo)
        {
            //_protector = protector;
            this.subjectsRepo = subjectsRepo;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            try
            {
                IEnumerable<Course> Ensubjects = (await subjectsRepo.GetCourses());
                if (Ensubjects.Any())
                {
                   //var DecryptedSubjects = _protector.Decrypt(Ensubjects);
                    return Ok(Ensubjects);
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
        public async Task<ActionResult<Course>> Get(int id)
        {
            Course enSubject = await subjectsRepo.GetCourse(id);
            if (enSubject == null)
                return NotFound();

            //Course DeSubjects = (Course)_protector.Decrypt(enSubject);
            return Ok(enSubject);
        }

        // POST: api/Subject
        [HttpPost("{collegeId:int}")]
        public async Task<ActionResult<Course>> Post(int collegeId,[FromBody] Course course)
        {
            if (course == null)
                return BadRequest();

            //byte[] encryptedSubject = _protector.Encrypt(subject);
            await subjectsRepo.AddCourse(collegeId, course);

            return CreatedAtAction(nameof(Get), course.Id, course);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Course subject)
        {
            //var encryptExistSubject = (Course)_protector.Encrypt(subject);

            var updatedSubject = await subjectsRepo.UpdateCourse(subject, id);
            if (updatedSubject != null)
                return Ok(updatedSubject);
            return NotFound();
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Course endeletedSubject = await subjectsRepo.DeleteCourse(id);

            if (endeletedSubject != null)
                return Ok(endeletedSubject);

            return NotFound();
        }
    }

}

