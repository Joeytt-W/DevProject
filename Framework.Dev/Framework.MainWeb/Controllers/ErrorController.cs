using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Framework.MainWeb.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult Index(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
                ViewBag.Error = error;
            else
                ViewBag.Error = "InterNal Wrong";
            return View();
        }
    }
}