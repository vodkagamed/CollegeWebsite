using Microsoft.AspNetCore.Components;
namespace SchoolWebsite.Client.Pages;

public partial class Students
{
    [Inject]
    public StudentService StudentService { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    private List<Student> students = new();
    private HttpResponseMessage response = new();
    [Inject] public DataProtector protector { get; set; }
    protected override async Task OnInitializedAsync()
    {
        response = await StudentService.GetStudents();

        bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, null);
        if (areAny)
        {
            students = await response.Content.ReadFromJsonAsync<List<Student>>();
        }
    }


}

