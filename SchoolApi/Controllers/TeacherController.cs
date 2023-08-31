using Microsoft.AspNetCore.Mvc;
using SchoolApi.Repos;
using SchoolWebsite.shared;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly DataProtector _protector;
        private readonly TeachersRepo teachersRepo;

        public TeacherController(DataProtector protector, TeachersRepo teachersRepo)
        {
            _protector = protector;
            this.teachersRepo = teachersRepo;
        }

        // GET: api/Teacher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            try
            {
                List<Teacher> Enteachers = (await teachersRepo.GetTeachers()).ToList();
                if (Enteachers.Any())
                {
                    IEnumerable<Teacher> DecryptedTeachers = (IEnumerable<Teacher>)_protector.Decrypt(Enteachers);
                    return Ok(DecryptedTeachers);
                }
                else
                    return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Teacher/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            Teacher enTeacher = await teachersRepo.GetTeacher(id);
            if (enTeacher == null)
                return NotFound();

            Teacher DeTeachers = (Teacher)_protector.Decrypt(enTeacher);
            return Ok(DeTeachers);
        }

        // POST: api/Teacher
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post([FromBody] Teacher teacher)
        {
            if (teacher == null)
                return BadRequest();

            Teacher encryptedTeacher = (Teacher)_protector.Encrypt(teacher);
            await teachersRepo.AddTeacher(encryptedTeacher);

            return CreatedAtAction(nameof(Get), teacher.Id, encryptedTeacher);
        }

        // PUT: api/Teacher/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Teacher teacher)
        {
            var encryptExistTeacher = (Teacher)_protector.Encrypt(teacher);

            var updatedTeacher = await teachersRepo.UpdateTeacher(encryptExistTeacher, id);
            if (updatedTeacher != null)
                return Ok(_protector.Decrypt(updatedTeacher));
            return NotFound();
        }

        // DELETE: api/Teacher/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Teacher endeletedTeacher = (Teacher)await teachersRepo.DeleteTeacher(id);

            if (endeletedTeacher != null)
                return Ok(_protector.Decrypt(endeletedTeacher));

            return NotFound();
        }
    }
}
