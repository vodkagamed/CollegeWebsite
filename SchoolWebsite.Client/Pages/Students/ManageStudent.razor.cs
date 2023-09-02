using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Pages.Students;
public partial class ManageStudent
{
    [Inject]
    public StudentService studentService { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    [Parameter]
    public Student Student { get; set; } = new();
    public HttpResponseMessage response = new();
    public virtual async Task OnFormValidAsync()
    {
        if (Student.Id > 0)
        {
            response = await studentService.EditStudent(Student.Id, Student);
            
            await validation.PerformHttpRequest(HttpMethod.Put, response,Student.Name);
        }
        else
        {
            response = await studentService.CreateStudent(Student);
            bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Student.Name);
            if (isCreated)
                Student = new();
        }
    }
}
