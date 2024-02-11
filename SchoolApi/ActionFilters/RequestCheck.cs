using Microsoft.AspNetCore.Mvc.Filters;
namespace CollegeApi.ActionFilters;


public class RequestCheck<T> : ActionFilterAttribute where T : class
{
    IRepository<T> _repository;
    public RequestCheck(IRepository<T> repository)
    {
        _repository = repository;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var entityId = context.ActionArguments["id"] as Guid?;

        if (entityId is not null && entityId != Guid.Empty)
        {
            var result = await _repository.GetByIdAsync(entityId);
            if (result is null)
            {
                context.Result = new NotFoundObjectResult("Error not found");
            }
        }
        else
        {
            context.Result = new BadRequestObjectResult("Id is null or empty");
            return;
        }
        await base.OnActionExecutionAsync(context, next);
    }

}
