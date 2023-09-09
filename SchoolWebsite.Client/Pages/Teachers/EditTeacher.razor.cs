using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Teachers
{
    public partial class EditTeacher
    {
        [Inject]
        public TeacherService teacherService { get; set; }
        [Inject]
        public DataProtector protector { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public string Id { get; set; }
        private Teacher editedTeacher = new();
        protected async override Task OnInitializedAsync()
        {
            var response = await teacherService.GetTeacherById(int.Parse(Id));
            editedTeacher = await response.Content.ReadFromJsonAsync<Teacher>();

            await validation.PerformHttpRequest(HttpMethod.Get, response, editedTeacher.Name);
        }
    }
}
