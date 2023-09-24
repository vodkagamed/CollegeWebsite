namespace SchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    //private readonly DataProtector _protector;
    private readonly StudentsRepo studentsRepo;

    public StudentController(DataProtector protector, StudentsRepo studentsRepo)
    {
        //_protector = protector;
        this.studentsRepo = studentsRepo;
    }

    // GET: api/Student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> Get()
    {
        try
        {
            List<Student> Enstudents = (await studentsRepo.GetStudents()).ToList();
            if (Enstudents.Any())
            {
                //var DecryptedStudents = _protector.Decrypt(Enstudents).ToList();
                return Ok(Enstudents);
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
    public async Task<ActionResult<Student>> Get(int id)
    {
        Student enStudent = await studentsRepo.GetStudent(id);
        if (enStudent == null)
            return NotFound();

        //object DeStudents = _protector.Decrypt(enStudent);
        return Ok(enStudent);
    }

    [HttpPost("{collegeId:int}")]
    public async Task<ActionResult<Student>> Post(int collegeId,[FromBody] Student student)
    {
        if (student == null)
            return BadRequest();

        //Student encryptedStudent =(Student) _protector.Encrypt(student);
        await studentsRepo.AddStudent(collegeId, student);

        return CreatedAtAction(nameof(Get), student.Id, student);
    }

    [HttpPost]
    [Route("{studentId}/Course/Enroll")]
    public async Task<ActionResult<Course>> Enroll(int studentId, [FromBody] int courseId)
    {
        Course EnrolledStudent = await studentsRepo.EnrollCourse(studentId, courseId);
        if (EnrolledStudent is not null)
            return Ok(EnrolledStudent);
        return BadRequest();
    }
    [HttpPost]
    [Route("{studentId}/Course/Cancel")]
    public async Task<ActionResult<Course>> CancelCourse(int studentId,[FromBody] int courseId)
    {

        Course CanceledStudent = await studentsRepo.CancelCourse(studentId, courseId);
        if (CanceledStudent is not null)
            return Ok(CanceledStudent);
        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Student student)
    {
        //var encryptExistStudent =(Student) _protector.Encrypt(student);

        var updatedStudent = await studentsRepo.UpdateStudent(student, id);
        if (updatedStudent != null)
            return Ok(updatedStudent);
        return NotFound();
    }

    // DELETE: api/Student/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Student endeletedStudent = await studentsRepo.DeleteStudent(id);

        if (endeletedStudent != null)
            return Ok(endeletedStudent);
        
        return NotFound();
    }
}
