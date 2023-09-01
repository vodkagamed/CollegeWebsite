using Microsoft.AspNetCore.Components;

namespace SchoolWebsite.Client.Components
{
    public partial class Label
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }
    }
}
