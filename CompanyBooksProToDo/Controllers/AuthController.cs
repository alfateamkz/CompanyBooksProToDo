using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using CompanyBooksProToDo.Models.TODO;
using CompanyBooksProToDo.Abstractions;

namespace CompanyBooksProToDo.Controllers
{
    public class AuthController : AbsController
    {


        [Route("HasUser")]
        public async Task<IActionResult> HasUser(string username,string password)
        {
            return Json(Helpers.ApiHelper.HasUser(username, password));
        }

        [HttpPost,Route("Auth")]
        public async Task<IActionResult> Auth(string username, string password)
        {
            var user = Helpers.ApiHelper.GetUser(username, password);
            await MakeAuth(user);
            return RedirectToAction("Index", "Home");
        }
        private async Task<ClaimsPrincipal> MakeAuth(Employee user)
        {
            await SignOut();

            // создаем один claim
            var claims = new List<Claim>
            {
                 new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id_client.ToString()),
                 //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserRole.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var principal = new ClaimsPrincipal(id);

            //Хуярим куки для хранения данных юзера и получения через ApiHelper.
            HttpContext.Response.Cookies.Append("Id_client", user.Id_client.ToString());
            HttpContext.Response.Cookies.Append("Client_name", user.Client_name);
            HttpContext.Response.Cookies.Append("Client_number", user.Client_number);

            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return principal;
        }


        public async Task<IActionResult> Logout()
        {

            await SignOut();
            return RedirectToAction("Login", "Home");
        }

        private async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (HttpContext.Request.Cookies.ContainsKey("Id_client"))
                HttpContext.Response.Cookies.Delete("Id_client");
            if (HttpContext.Request.Cookies.ContainsKey("Client_name"))
                HttpContext.Response.Cookies.Delete("Client_name");
            if (HttpContext.Request.Cookies.ContainsKey("Client_number"))
                HttpContext.Response.Cookies.Delete("Client_number");
        }
    }
}
