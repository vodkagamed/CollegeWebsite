using Microsoft.AspNetCore.Components;
using SchoolWebsite.shared.Models;

namespace SchoolWebsite.Client.Pages.Colleges
{
    public partial class AddCollege
    {
        [Inject]
        public CollegeService CollService { get; set; }
        [Inject]
        public ValidationMessages Validation { get; set; }
        [Parameter]
        public College College { get; set; } = new();
        public HttpResponseMessage response = new();
        public virtual async Task OnFormValidAsync()
        {
            if (College.Id > 0)
            {
                response = await CollService.Edit(College.Id, College);

                await Validation.PerformHttpRequest(HttpMethod.Put, response, College.Name);
            }
            else
            {
                response = await CollService.Create(College);
                bool isCreated = await Validation.PerformHttpRequest(HttpMethod.Post, response, College.Name);
                if (isCreated)
                    College = new();
            }
        }
    }
}
