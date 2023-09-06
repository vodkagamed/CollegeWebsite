using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared;

namespace SchoolWebsite.Client.Pages.Students;
public partial class StudentData
{
    [Inject] private NavigationManager? nav { get; set; }
    [Inject] public StudentService? studentService { get; set; }
    [Inject] public CourseService? courseService { get; set; }
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
        var Studresponse = await studentService.GetStudentById(int.Parse(Id));
        bool areAnySt = await validation.PerformHttpRequest(HttpMethod.Get, Studresponse, null);
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

    public async Task<List<Course>> GetAvailableCourses()
    {
        var allcourResponse = await courseService.GetCourses(student.CollegeId);
        var allCourses =
            await allcourResponse.Content.ReadFromJsonAsync<List<Course>>();

        var enrolledCourseIds = student.Courses.Select(c => c.Id).ToList();

        // Exclude the enrolled courses from the list of all courses
        var availableCourses = allCourses.Where(c => !enrolledCourseIds.Contains(c.Id)).ToList();

        return availableCourses;
    }



}

