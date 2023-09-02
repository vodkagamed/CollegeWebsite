using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Pages.Teachers
{
    public partial class AddTeacher
    {
        [Inject]
        public TeacherService teacherService { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public Teacher Teacher { get; set; } = new();
        public HttpResponseMessage response = new();
        public virtual async Task OnFormValidAsync()
        {
            if (Teacher.Id > 0)
            {
                response = await teacherService.EditTeacher(Teacher.Id, Teacher);

                await validation.PerformHttpRequest(HttpMethod.Put, response, Teacher.Name);
            }
            else
            {
                response = await teacherService.CreateTeacher(Teacher, Teacher.CollegeId);
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Teacher.Name);
                if (isCreated)
                    Teacher = new();
            }
        }
    }
}
