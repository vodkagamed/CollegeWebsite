using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using SchoolWebsite.shared.Models.Create;
namespace SchoolWebsite.Client.Pages.Students;
public partial class AddStudent
{
    [Inject] public StudentService studentService { get; set; }
    [Inject] public ValidationMessages validation { get; set; }
    [Inject] public CollegeService CollegeService { get; set; }
    [Parameter]
    public Student Student { get; set; } = new();
    public HttpResponseMessage response = new();
    private List<Course> avilabeColleges = new();
    protected override async Task OnInitializedAsync() => await GetAllColleges();
    public virtual async Task OnFormValidAsync(object createdStudent)
    {
        if (Student.Id != Guid.Empty)
        {
            response = await studentService.Edit(Student.Id, Student);

            await validation.PerformHttpRequest(HttpMethod.Put, response, Student.Name);
        }
        else
        {
            response = await studentService.Create(Student.CollegeId, Student);
            bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Student.Name);
            if (isCreated)
                Student = new();
        }
    }
    async Task GetAllColleges()
    {
        var response = await CollegeService.Get();
        var allColleges = await response.Content.ReadFromJsonAsync<List<Course>>();
        avilabeColleges = allColleges.ToList();
    }

}
