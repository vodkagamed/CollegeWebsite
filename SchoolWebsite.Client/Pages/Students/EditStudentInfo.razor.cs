using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using System.Reflection.Metadata;

namespace SchoolWebsite.Client.Pages.Students
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
        private Student StudentToEdit = new();
        protected async override Task OnInitializedAsync()
        {
            var response = await studentService.GetById(Guid.Parse(Id));
            StudentToEdit = await response.Content.ReadFromJsonAsync<Student>();

            await validation.PerformHttpRequest(HttpMethod.Get, response, StudentToEdit.Name);
        }


    }
}
