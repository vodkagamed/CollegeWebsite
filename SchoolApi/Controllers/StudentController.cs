using Microsoft.AspNetCore.Mvc;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly DataProtector _protector;
        private readonly StudentsRepo studentsRepo;

        public StudentController(DataProtector protector, StudentsRepo studentsRepo)
        {
            _protector = protector;
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
                    List<Student> DecryptedStudents = _protector.Decrypt(Enstudents).ToList();
                    return Ok(DecryptedStudents);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Student/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            Student enStudent = await studentsRepo.GetStudent(id);
            if (enStudent == null)
                return NotFound();

            Student DeStudents = _protector.Decrypt(enStudent);
            return Ok(DeStudents);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            if (student == null)
                return BadRequest();

            Student encryptedStudent = _protector.Encrypt(student);
            await studentsRepo.AddStudent(encryptedStudent);

            return CreatedAtAction(nameof(Get), student.Id, encryptedStudent);
        }

        // PUT: api/Student/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Student student)
        {
            var encryptExistStudent = _protector.Encrypt(student);

            var updatedStudent = await studentsRepo.UpdateStudent(encryptExistStudent, id);
            if (updatedStudent != null)
                return Ok(_protector.Decrypt(updatedStudent));
            return NotFound();
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Student endeletedStudent = await studentsRepo.DeleteStudent(id);

            if (endeletedStudent != null)
                return Ok(_protector.Decrypt(endeletedStudent));
            
            return NotFound();
        }
    }
}
