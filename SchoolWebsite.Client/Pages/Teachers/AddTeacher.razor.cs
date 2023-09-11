using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;
using System.Runtime.CompilerServices;

namespace SchoolWebsite.Client.Pages.Teachers
{
    public partial class AddTeacher
    {
        [Inject] public TeacherService teacherService { get; set; }
        [Inject] public CourseService CourseService { get; set; }
        [Inject] public CollegeService CollegeService { get; set; }
        [Inject] public ValidationMessages validation { get; set; }
        [Parameter]
        public Teacher Teacher { get; set; } = new();
        public HttpResponseMessage response = new();
        public List<Course> avilabeCourses = new();
        private List<Course> avilabeColleges = new();

        protected override async Task OnInitializedAsync() => await GetAllColleges();
        public virtual async Task OnFormValidAsync()
        {
            if (Teacher.Id > 0)
            {
                response = await teacherService.Edit(Teacher.Id, Teacher);

                await validation.PerformHttpRequest(HttpMethod.Put, response, Teacher.Name);
            }
            else
            {
                response = await teacherService.Create(Teacher.CollegeId, Teacher);
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Teacher.Name);
                if (isCreated)
                    Teacher = new();
            }
        }

        async Task GetCollegeCourses()
        {
            var response = await CourseService.Get();
            var allCourses = await response.Content.ReadFromJsonAsync<List<Course>>();
            var allCollegeCourses = allCourses.Where(C => C.CollegeId == Teacher.CollegeId);
            avilabeCourses = allCollegeCourses.ToList();
        }
        async Task GetAllColleges()
        {
            var response = await CollegeService.Get();
            var allColleges = await response.Content.ReadFromJsonAsync<List<Course>>();
            avilabeColleges = allColleges.ToList();
        }

        private async Task CatchCollegeId(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out int collegeId))
            {
                Teacher.CollegeId = collegeId;
                await GetCollegeCourses();
            }
        }

    }
}
