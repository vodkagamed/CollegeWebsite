using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Pages.Cources
{
    public partial class AddCourse
    {
        [Inject]
        public CourseService courseService { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public Course Course { get; set; } = new();
        public HttpResponseMessage response = new();
        public virtual async Task OnFormValidAsync()
        {
            if (Course.Id > 0)
            {
                response = await courseService.EditCourse(Course.Id, Course);

                await validation.PerformHttpRequest(HttpMethod.Put, response, Course.Name);
            }
            else
            {
                response = await courseService.CreateCourse(Course, Course.CollegeId);
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Course.Name);
                if (isCreated)
                    Course = new();
            }
        }
    }
}
