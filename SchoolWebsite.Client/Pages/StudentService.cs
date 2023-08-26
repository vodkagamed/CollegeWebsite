namespace SchoolWebsite.Client.Pages;

public class StudentService
{
    private readonly HttpClient _httpClient;
    public StudentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> CreateStudent(Student createdStudent)
    {
        return await _httpClient.PostAsJsonAsync("/api/Student",createdStudent);
    }
    public async Task<HttpResponseMessage> GetStudents()
    {
        return await _httpClient.GetAsync("/api/Student");
    }
    public async Task<HttpResponseMessage> GetStudentById(int studentId)
    {
        return await _httpClient.GetAsync($"/api/Student/{studentId}");
    }
    public async Task<HttpResponseMessage> EditStudent(int studentId, Student editedStudent)
    {
        return await _httpClient.PutAsJsonAsync($"api/Student/{studentId}", editedStudent);   
    }

    public async Task<HttpResponseMessage> DeleteStudent(int studentId)
    {
       return await _httpClient.DeleteAsync($"/api/Student/{studentId}");
    }
}
