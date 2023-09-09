using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Cources
{
    public partial class CourseInfo
    {
        [Inject] private NavigationManager? nav { get; set; }
        [Inject] public CourseService? courseService { get; set; }
        public DataProtector protector { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public string? Id { get; set; }
        public Dictionary<string, string> CourseRecords { get; private set; }

        private Course course = new();
        bool valid;

        protected override async Task OnInitializedAsync()
        {
            var response = await courseService.GetCourseById(int.Parse(Id));
            bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, null);
            if (areAny)
            {
                course = await response.Content.ReadFromJsonAsync<Course>();
                valid = true;
                try
                {
                    CourseRecords = new Dictionary<string, string>
                          {
                            { "Id", course.Id.ToString() },
                            { "Name", course.Name },
                            { "College", course.College.Name}
                          };

                }
                catch (NullReferenceException)
                {
                    CourseRecords = new();
                }
            }
        }
        public async Task DeleteCourse(int courseId)
        {
            var response = await courseService.DeleteCourse(courseId);
            Course deletedCourse = await response.Content.ReadFromJsonAsync<Course>();
            bool isDeleted = await validation.PerformHttpRequest
                (HttpMethod.Delete, response, deletedCourse.Name);
            if (isDeleted)
             await InvokeAsync(StateHasChanged);
        }
        public void EditCourse(int courseID) =>
            nav.NavigateTo($"/EditCourseInfo/{courseID}", forceLoad: true);


    }
}
