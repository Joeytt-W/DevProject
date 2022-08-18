using Dev.Entity.ViewModel;
using Dev.Services.IServices;
using Dev.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.CoreApi.Controllers
{
    public class AccountController:Controller
    {
        private readonly ITestService _test;

        public AccountController(ITestService test)
        {
            this._test = test;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stus = await _test.GetTestStudentInfo();
            return View(stus);
        }
    }
}
