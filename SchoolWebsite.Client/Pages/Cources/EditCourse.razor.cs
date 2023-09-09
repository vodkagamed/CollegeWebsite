using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Cources
{
    public partial class EditCourse
    {
        [Inject]
        public CourseService courseService { get; set; }
        [Inject]
        public DataProtector protector { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public string Id { get; set; }
        private Course editedCourse = new();
        protected async override Task OnInitializedAsync()
        {
            var response = await courseService.GetCourseById(int.Parse(Id));
            editedCourse = await response.Content.ReadFromJsonAsync<Course>();

            await validation.PerformHttpRequest(HttpMethod.Get, response, editedCourse.Name);
        }
    }
}
