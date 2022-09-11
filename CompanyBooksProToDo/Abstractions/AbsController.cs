using CompanyBooksProToDo.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CompanyBooksProToDo.Abstractions
{
    public abstract class AbsController : Controller
    {
        [NonAction]
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ApiHelper.Context = HttpContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ApiHelper.Context = HttpContext;
        }


    }
}
