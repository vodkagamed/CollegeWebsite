using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Pages.Colleges
{
    public partial class AddCollege
    {
        [Inject]
        public CollegeService CollService { get; set; }
        [Inject]
        public ValidationMessages validation { get; set; }
        [Parameter]
        public College College { get; set; } = new();
        public HttpResponseMessage response = new();
        public virtual async Task OnFormValidAsync()
        {
            if (College.Id > 0)
            {
                response = await CollService.EditCollege(College.Id, College);

                await validation.PerformHttpRequest(HttpMethod.Put, response, College.Name);
            }
            else
            {
                response = await CollService.CreateCollege(College);
                bool isCreated = await validation.PerformHttpRequest(HttpMethod.Post, response, College.Name);
                if (isCreated)
                    College = new();
            }
        }
    }
}
