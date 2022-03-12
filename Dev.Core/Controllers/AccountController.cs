using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.CoreWeb.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginAccount, string loginPassword)
        {
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
