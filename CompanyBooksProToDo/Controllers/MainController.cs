using CompanyBooksProToDo.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alfateam.Controllers
{
    public class MainController : Controller
    {

        [NonAction]
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ApiHelper.Context = HttpContext;
        }


        [HttpGet,Route("SetLocale")]
        public async Task SetLocale(enumLanguagesEnum lang)
        {
            if (HttpContext.Request.Cookies.ContainsKey("Language"))
            {
                HttpContext.Response.Cookies.Delete("Language");
            }
            HttpContext.Response.Cookies.Append("Language", ((int)lang).ToString());
        }
    }
}
