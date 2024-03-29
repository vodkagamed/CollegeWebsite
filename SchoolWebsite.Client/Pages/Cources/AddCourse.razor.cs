﻿using Microsoft.AspNetCore.Components;
using SchoolWebsite.Client.Pages.Colleges;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Cources
{
    public partial class AddCourse
    {
        [Inject]
        public CourseService courseService { get; set; } [Inject]
        public CollegeService CollegeService { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public Course Course { get; set; } = new();
        private HttpResponseMessage response = new();
        private List<College> Colleges = new();
        private Guid selectedCollegeId;

        protected override async Task OnInitializedAsync()
        {
            Colleges = await GetColleges();
        }
        public async Task<List<College>> GetColleges()
        {
            var response = await CollegeService.Get();

            bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, Course.Name);
            if (areAny)
            {
                return Colleges = await response.Content.ReadFromJsonAsync<List<College>>();
            }
            return new List<College>();
        }
        public virtual async Task OnFormValidAsync()
        {
            if (Course.Id !=Guid.Empty)
            {
                Course.CollegeId = selectedCollegeId; // Set the selected college ID
                response = await courseService.Edit(Course.Id, Course);
                await validation.PerformHttpRequest(HttpMethod.Put, response, Course.Name);
            }
            else
            {
                response = await courseService.Create(selectedCollegeId,Course); // Use the selected college ID
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, Course.Name);
                if (isCreated)
                    Course = new();
            }
        }
    }
}
