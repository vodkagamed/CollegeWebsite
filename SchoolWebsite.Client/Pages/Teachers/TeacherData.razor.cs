using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using SchoolWebsite.Shared;

namespace SchoolWebsite.Client.Pages.Teachers;
public partial class TeacherData
{
    [Inject] private NavigationManager nav { get; set; }
    [Inject] public TeacherService teacherService { get; set; }
    public DataProtector protector { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    [Parameter]
    public string Id { get; set; }
    private Teacher teacher = new();
    private Dictionary<string,string> TeacherRecords = new();
    bool valid;

    protected override async Task OnInitializedAsync()
    {
        var response = await teacherService.GetById(int.Parse(Id));
        bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, "Teacher data");
        if (areAny)
        {
            teacher = await response.Content.ReadFromJsonAsync<Teacher>() ?? new();
            valid = true;
        }
        try
        {
            TeacherRecords = new Dictionary<string, string>
            {
                { "Id", teacher.Id.ToString() },
                { "Name", teacher.Name },
                { "College", teacher.College.Name },
                { "Course", teacher.Course.Name}
            };

        }
        catch (NullReferenceException)
        {
            TeacherRecords = new();
        }
    }
    public async Task DeleteTeacher(int teacherId)
    {
        var response = await teacherService.Delete(teacherId);
        Teacher deletedTeacher = await response.Content.ReadFromJsonAsync<Teacher>() ?? new();
        bool isDeleted = await validation.PerformHttpRequest
            (HttpMethod.Delete, response, deletedTeacher.Name);
    }
    public void EditTeacher(int teacherID) =>
        nav.NavigateTo($"/EditTeacherInfo/{teacherID}", forceLoad: true);
}

