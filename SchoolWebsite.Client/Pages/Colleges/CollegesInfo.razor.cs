using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared;

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
            await GetColleges();
        }
        public async Task GetColleges()
        {
            var response = await CollegeService.GetColleges();

            bool areAny = await validation.PerformHttpRequest(HttpMethod.Get, response, null);
            if (areAny)
            {
                Colleges = await response.Content.ReadFromJsonAsync<List<College>>();
            }
        }

    }
}
