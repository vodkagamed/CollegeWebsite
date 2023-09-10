using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Colleges
{
    public partial class CollegesInfo
    {
        [Inject]
        public CollegeService CollegeService { get; set; }

        [Inject]
        public ValidationMessages validation { get; set; }
        public List<College> Colleges { get; set; } = new();
        protected override async Task OnInitializedAsync()
        {
            Colleges = await GetColleges();
        }
        public async Task<List<College>> GetColleges()
        {
            var response = await CollegeService.Get();

            bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, "Colleges");
            if (areAny)
            {
               return Colleges = await response.Content.ReadFromJsonAsync<List<College>>();
            }
            return new List<College>();
        }

    }
}
