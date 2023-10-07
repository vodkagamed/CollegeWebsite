using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Components
{
    public partial class CustomInfo
    {
        [Inject] private NavigationManager Nav { get; set; }
        [Inject] public CourseService CourseService { get; set; }
        public DataProtector Protector { get; set; }
        [Inject]
        public ValidationMessages Validation { get; set; }
        [Parameter]
        public string Id { get; set; }
        public Dictionary<string, string> CourseRecords { get; private set; }

        private Course course = new();

        protected override async Task OnInitializedAsync()
        {
            var response = await CourseService.GetById(int.Parse(Id));
            bool areAny = await Validation.PerformHttpRequest(HttpMethod.Get, response, "Course Data");
            if (areAny)
            {
                course = await response.Content.ReadFromJsonAsync<Course>();
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
            var response = await CourseService.Delete(courseId);
            Course deletedCourse = await response.Content.ReadFromJsonAsync<Course>();
            bool isDeleted = await Validation.PerformHttpRequest
                (HttpMethod.Delete, response, deletedCourse.Name);
            if (isDeleted)
                await InvokeAsync(StateHasChanged);
        }
        public void EditCourse(int courseID) =>
            Nav.NavigateTo($"/EditCourseInfo/{courseID}", forceLoad: true);


    }

}
