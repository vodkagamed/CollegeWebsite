using CollegeApi.ActionFilters;

namespace SchoolApi.Controllers;

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
            IEnumerable<Course> Ensubjects = await CourRepo.GetAllAsync();
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


    [HttpGet("{id}")]
    [ServiceFilter(typeof(RequestCheck<Course>))]
    public async Task<ActionResult<Course>> GetCourseById(Guid id)
    {
        Course enSubject = await CourRepo.GetByIdAsync(id);
        if (enSubject == null)
            return NotFound();

        //Course DeSubjects = (Course)_protector.Decrypt(enSubject);
        return Ok(enSubject);
    }

    // GET: api/Course/College/5
    // POST: api/Subject
    [HttpPost]
    public async Task<ActionResult<Course>> Post([FromBody] Course course)
    {
        if (course == null)
            return BadRequest();

        //byte[] encryptedSubject = _protector.Encrypt(subject);
        await CourRepo.AddAsync(course);

        return CreatedAtAction(nameof(Post),new {id= course.Id } , course);
    }

    // PUT: api/Subject/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromBody] Course subject)
    {
        //var encryptExistSubject = (Course)_protector.Encrypt(subject);

        var updatedSubject = await CourRepo.UpdateAsync(subject);
        if (updatedSubject != null)
            return Ok(updatedSubject);
        return NotFound();
    }

    // DELETE: api/Subject/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        Course endeletedSubject = await CourRepo.DeleteAsync(id);

        if (endeletedSubject != null)
            return Ok(endeletedSubject);

        return NotFound();
    }
}

