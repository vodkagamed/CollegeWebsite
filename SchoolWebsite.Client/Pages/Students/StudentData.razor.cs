using Microsoft.AspNetCore.Components;
namespace SchoolWebsite.Client.Pages.Students;
public partial class StudentData
{
    [Inject] private NavigationManager? nav { get; set; }
    [Inject] public StudentService? studentService { get; set; }
    public DataProtector protector { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    [Parameter]
    public string? Id { get; set; }
    private Student student = new();
    bool valid;

    protected override async Task OnInitializedAsync()
    {
        var response = await studentService.GetStudentById(int.Parse(Id));
        bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, null);
        if (areAny)
        {
            student = await response.Content.ReadFromJsonAsync<Student>();
            valid = true;
        }
    }
    public async Task DeleteStudent(int studentId)
    {
        var response = await studentService.DeleteStudent(studentId);
        Student deletedStudent = await response.Content.ReadFromJsonAsync<Student>();
        bool isDeleted = await validation.PerformHttpRequest
            (HttpMethod.Delete, response, deletedStudent.Name);
        if (isDeleted)
            InvokeAsync(StateHasChanged);
    }
    public void EditStudent(int studentID) =>
        nav.NavigateTo($"/EditStudentInfo/{studentID}", forceLoad: true);



}

