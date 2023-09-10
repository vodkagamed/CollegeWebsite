using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using System.Runtime.CompilerServices;

namespace SchoolWebsite.Client.Pages.Teachers
{
    public partial class AddTeacher
    {
        [Inject]
        public TeacherService teacherService { get; set; }
        [Inject]
        public CourseService CourseService { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public Teacher Teacher { get; set; } = new();
        public HttpResponseMessage response = new();
        public List<Course> avilabeCourses = new();
        public virtual async Task OnFormValidAsync()
        {
            if (Teacher.Id > 0)
            {
                response = await teacherService.Edit(Teacher.Id, Teacher);

                await validation.PerformHttpRequest(HttpMethod.Put, response, Teacher.Name);
            }
            else
            {
                response = await teacherService.Create( Teacher.CollegeId, Teacher);
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Teacher.Name);
                if (isCreated)
                    Teacher = new();
            }
            //await GetCollegeCourses();
        }
        //async Task GetCollegeCourses()
        //{
        //    var response = await CourseService.GetCourses(Teacher.CollegeId);
        //    avilabeCourses = await response.Content.ReadFromJsonAsync<List<Course>>();
        //}
    }
}
