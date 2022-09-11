using CompanyBooksProToDo.Helpers;
using CompanyBooksProToDo.Models;
using CompanyBooksProToDo.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyBooksProToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var vm = new IndexVM
            {
                Missions = ApiHelper.GetMissions()
            };
            return View(vm);
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult CreateItem()
        {
            return View();
        }
        public IActionResult UpdateItem()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
