using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using SchoolWebsite.Shared;

namespace SchoolWebsite.Client.Pages.Teachers;
public partial class TeacherData
{
    [Inject] private NavigationManager? nav { get; set; }
    [Inject] public TeacherService? teacherService { get; set; }
    public DataProtector protector { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    [Parameter]
    public string? Id { get; set; }
    private Teacher teacher = new();
    private Dictionary<string,string> TeacherRecords = new();
    bool valid;

    protected override async Task OnInitializedAsync()
    {
        var response = await teacherService.GetTeacherById(int.Parse(Id));
        bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, null);
        if (areAny)
        {
            teacher = await response.Content.ReadFromJsonAsync<Teacher>();
            valid = true;
        }
        try
        {
            TeacherRecords = new Dictionary<string, string>
            {
                { "Id", teacher.Id.ToString() },
                { "Name", teacher.Name },
                { "College", teacher.College.Name},
                { "Course", teacher.Course.Name}
            };

        }
        catch (NullReferenceException)
        {
            TeacherRecords = new();
        }

        var tasks = new List<Task>();

        Parallel.ForEach(Enumerable.Range(1, 10), i =>
        {
            tasks.Add(Task.Run(async () =>
            {
                await Log.Information($"Log entry from Thread {i}");
            }));
        });
    }
    public async Task DeleteTeacher(int teacherId)
    {
        var response = await teacherService.DeleteTeacher(teacherId);
        Teacher deletedTeacher = await response.Content.ReadFromJsonAsync<Teacher>();
        bool isDeleted = await validation.PerformHttpRequest
            (HttpMethod.Delete, response, deletedTeacher.Name);
        if (isDeleted)
            InvokeAsync(StateHasChanged);
    }
    public void EditTeacher(int teacherID) =>
        nav.NavigateTo($"/EditTeacherInfo/{teacherID}", forceLoad: true);



}

