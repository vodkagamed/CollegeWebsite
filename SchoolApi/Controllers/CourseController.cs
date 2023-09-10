using Microsoft.AspNetCore.Mvc;
using SchoolApi.Repos;
using SchoolWebsite.shared;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        //private readonly DataProtector _protector;
        private readonly CoursesRepo CourRepo;

        public CourseController(DataProtector protector, CoursesRepo subjectsRepo)
        {
            //_protector = protector;
            this.CourRepo = subjectsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            try
            {
                IEnumerable<Course> Ensubjects = await CourRepo.GetCourses();
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


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            Course enSubject = await CourRepo.GetCourse(id);
            if (enSubject == null)
                return NotFound();

            //Course DeSubjects = (Course)_protector.Decrypt(enSubject);
            return Ok(enSubject);
        }

        // GET: api/Course/College/5
        // POST: api/Subject
        [HttpPost("{collegeId:int}")]
        public async Task<ActionResult<Course>> Post(int collegeId,[FromBody] Course course)
        {
            if (course == null)
                return BadRequest();

            //byte[] encryptedSubject = _protector.Encrypt(subject);
            await CourRepo.AddCourse(collegeId, course);

            return CreatedAtAction(nameof(Post),new {id= course.Id } , course);
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Course subject)
        {
            //var encryptExistSubject = (Course)_protector.Encrypt(subject);

            var updatedSubject = await CourRepo.UpdateCourse(subject, id);
            if (updatedSubject != null)
                return Ok(updatedSubject);
            return NotFound();
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Course endeletedSubject = await CourRepo.DeleteCourse(id);

            if (endeletedSubject != null)
                return Ok(endeletedSubject);

            return NotFound();
        }
    }

}

