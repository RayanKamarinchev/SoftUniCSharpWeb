using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskBoardApp01.Filters
{
    public class MyActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var service = context.HttpContext.RequestServices.GetService<IService>();
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
