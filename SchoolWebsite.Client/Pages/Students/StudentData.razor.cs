using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Students;
public partial class StudentData
{
    [Inject] private NavigationManager nav { get; set; }
    [Inject] public StudentService studentService { get; set; }
    [Inject] public CourseService courseService { get; set; }
    public DataProtector protector { get; set; }
    [Inject]
    public ValidationMessages validation { get; set; }
    [Parameter]
    public string? Id { get; set; }
    private Student student = new();
    bool valid;

    private List<Course> AvailableCourses = new();
    private Dictionary<string, string> StudData = new();
    protected override async Task OnInitializedAsync()
    {
        var Studresponse = await studentService.GetById(int.Parse(Id));
        bool areAnySt = await validation.PerformHttpRequest(HttpMethod.Get, Studresponse, "Student Data");
        if (areAnySt)
        {
            student = await Studresponse.Content.ReadFromJsonAsync<Student>();
            valid = true;

            // Ensure student data is loaded before getting available courses
            AvailableCourses = await GetAvailableCourses();
        }
        try
        {
            StudData = new Dictionary<string, string>
            {
                { "Id", student.Id.ToString() },
                { "Name", student.Name },
                { "Age", student.Age },
                { "Phone", student.Phone},
                { "College", student.College.Name}
            };

        }
        catch (NullReferenceException)
        {

            StudData = new();
        }
        StateHasChanged();
    }
    public async Task DeleteStudent(int studentId)
    {
        Student deletedStudent = new();
        var response = await studentService.Delete(studentId);
        if (response.IsSuccessStatusCode)
            deletedStudent = await response.Content.ReadFromJsonAsync<Student>();

        await validation.PerformHttpRequest
             (HttpMethod.Delete, response, deletedStudent.Name);
    }
    public void EditStudent(int studentID) =>
        nav.NavigateTo($"/EditStudentInfo/{studentID}", forceLoad: true);

    public async Task<List<Course>> GetAvailableCourses()
    {
        List<Course> allCourses = new();
        var allcourResponse = await courseService.Get();
        if (allcourResponse.IsSuccessStatusCode)
            allCourses = await allcourResponse.Content.ReadFromJsonAsync<List<Course>>();
        allCourses = allCourses.Where(c => c.CollegeId == student.CollegeId).ToList();
        var enrolledCourseIds = student.Courses.Select(c => c.Id).ToList();

        var availableCourses = allCourses.Where(c => !enrolledCourseIds.Contains(c.Id)).ToList();

        return availableCourses;
    }

    public async Task EnrollCourse(object CourseCallBack)
    {
        Course course = (Course)CourseCallBack;
        var response = await studentService.EnrollCourse(student.Id, course.Id);
        Course EnrolledCourse = await response.Content.ReadFromJsonAsync<Course>();
        bool isEnrolled = await validation.PerformHttpRequest
            (HttpMethod.Post, response, EnrolledCourse.Name);
        if (isEnrolled)
            AvailableCourses = await GetAvailableCourses();
    }
    public async Task CancelCourse(object obj)
    {
        Course course = (Course)obj;
        var response = await studentService.CancelCourse(student.Id, course.Id);
        Course CanceledCourse = await response.Content.ReadFromJsonAsync<Course>();
        bool isCanceled = await validation.PerformHttpRequest
            (HttpMethod.Delete, response, CanceledCourse.Name);
        if (isCanceled)
        {
            AvailableCourses = await GetAvailableCourses();
            StateHasChanged();
        }
    }



}

