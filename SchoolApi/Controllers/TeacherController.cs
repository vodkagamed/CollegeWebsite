﻿namespace SchoolApi.Controllers;

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
            List<Teacher> Enteachers = (await teachersRepo.GetTeachers()).ToList();
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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Teacher>> Get(int id)
    {
        Teacher enTeacher = await teachersRepo.GetTeacher(id);
        if (enTeacher == null)
            return NotFound();

        //Teacher DeTeacher = (Teacher)_protector.Decrypt(enTeacher);
        return Ok(enTeacher);
    }

    // POST: api/Teacher
    [HttpPost("{collegeId:int}")]
    public async Task<ActionResult<Teacher>> Post(int collegeId,[FromBody] Teacher teacher)
    {
        if (teacher == null||collegeId<=0)
            return BadRequest("please add valid data");

        //Teacher encryptedTeacher = (Teacher)_protector.Encrypt(teacher);
        await teachersRepo.AddTeacher(collegeId,teacher);

        return CreatedAtAction(nameof(Get), teacher.Id, teacher);
    }

    // PUT: api/Teacher/5
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Teacher teacher)
    {
        //var encryptExistTeacher = (Teacher)_protector.Encrypt(teacher);

        var updatedTeacher = await teachersRepo.UpdateTeacher(teacher, id);
        if (updatedTeacher != null)
            return Ok(updatedTeacher);
            //return Ok(_protector.Decrypt(updatedTeacher));
        return NotFound();
    }

    // DELETE: api/Teacher/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Teacher endeletedTeacher = (Teacher)await teachersRepo.DeleteTeacher(id);

        if (endeletedTeacher != null)
            return Ok(endeletedTeacher);

        return NotFound();
    }
}
