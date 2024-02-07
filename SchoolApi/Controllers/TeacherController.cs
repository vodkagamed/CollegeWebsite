namespace SchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    //private readonly DataProtector _protector;
    private readonly TeachersRepo teachersRepo;

    public TeacherController(DataProtector protector, TeachersRepo teachersRepo)
    {
       // _protector = protector;
        this.teachersRepo = teachersRepo;
    }

    // GET: api/Teacher
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Teacher>>> Get()
    {
        try
        {
            List<Teacher> Enteachers = (await teachersRepo.GetAllAsync()).ToList();
            if (Enteachers.Any())
            {
                //IEnumerable<Teacher> DecryptedTeachers = (IEnumerable<Teacher>)_protector.Decrypt(Enteachers);
                return Ok(Enteachers);
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
    [HttpGet("{id}")]
    public async Task<ActionResult<Teacher>> Get(Guid id)
    {
        Teacher enTeacher = await teachersRepo.GetByIdAsync(id);
        if (enTeacher == null)
            return NotFound();

        //Teacher DeTeacher = (Teacher)_protector.Decrypt(enTeacher);
        return Ok(enTeacher);
    }

    // POST: api/Teacher
    [HttpPost]
    public async Task<ActionResult<Teacher>> Post([FromBody] Teacher teacher)
    {
        if (teacher == null)
            return BadRequest("please add valid data");

        //Teacher encryptedTeacher = (Teacher)_protector.Encrypt(teacher);
        await teachersRepo.AddAsync(teacher);

        return CreatedAtAction(nameof(Get), teacher.Id, teacher);
    }

    // PUT: api/Teacher/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put([FromBody] Teacher teacher)
    {
        //var encryptExistTeacher = (Teacher)_protector.Encrypt(teacher);

        var updatedTeacher = await teachersRepo.UpdateAsync(teacher);
        if (updatedTeacher != null)
            return Ok(updatedTeacher);
            //return Ok(_protector.Decrypt(updatedTeacher));
        return NotFound();
    }

    // DELETE: api/Teacher/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        Teacher endeletedTeacher = (Teacher)await teachersRepo.DeleteAsync(id);

        if (endeletedTeacher != null)
            return Ok(endeletedTeacher);

        return NotFound();
    }
}
