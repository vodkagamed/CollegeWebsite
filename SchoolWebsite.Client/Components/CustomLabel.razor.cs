using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Components
{
    public partial class CustomLabel
    {

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }
        [Parameter]
        public string LabelTitle { get; set; }
    }
}
