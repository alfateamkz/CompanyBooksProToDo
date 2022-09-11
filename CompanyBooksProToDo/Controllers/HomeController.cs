using CompanyBooksProToDo.Abstractions;
using CompanyBooksProToDo.Helpers;
using CompanyBooksProToDo.Models;
using CompanyBooksProToDo.Models.TODO;
using CompanyBooksProToDo.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyBooksProToDo.Controllers
{
    [Authorize]
    public class HomeController : AbsController
    {   
        public IActionResult Index()
        {
            var vm = new IndexVM
            {
                Missions = ApiHelper.GetMissions()
            };
            return View(vm);
        }

        [AllowAnonymous,Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        #region Create item
        public IActionResult CreateItem()
        {
            var vm = new CreateItemVM
            {
                Products = ApiHelper.GetProductsByParameters()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult CreateItemPOST(Mission mission)
        {
            ApiHelper.CreateMission(mission);
            return RedirectToAction("Index","Home");
        }
        #endregion

        #region Update item
        public IActionResult UpdateItem(long id)
        {
            var vm = new CreateItemVM
            {
                Mission = ApiHelper.GetMissionById(id),
                Products = ApiHelper.GetProductsByParameters()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult UpdateItemPOST(Mission mission)
        {
            ApiHelper.UpdateMission(mission);
            return RedirectToAction("Index", "Home");
        }
        #endregion



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
