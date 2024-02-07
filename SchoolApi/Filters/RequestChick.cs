using Microsoft.AspNetCore.Mvc.Filters;

namespace CollegeApi.Filters
{

    public class RequestChick : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            context.
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
           
    }
}
