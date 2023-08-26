using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace SchoolWebsite.Client.Pages
{
    public partial class EditStudentInfo
    {
        [Inject]
        public StudentService studentService { get; set; }
        [Inject]
        public DataProtector protector { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public string Id { get; set; }
        private Student editedStudent = new();
        protected async override Task OnInitializedAsync()
        {
            var response = await studentService.GetStudentById(int.Parse(Id));
            editedStudent = await response.Content.ReadFromJsonAsync<Student>();

            await validation.PerformHttpRequest(HttpMethod.Get, response, editedStudent.Name);
        }


    }
}
