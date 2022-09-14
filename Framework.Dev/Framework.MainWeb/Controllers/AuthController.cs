using Framework.MainEntity.ViewModel;
using Framework.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Framework.MainWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        // GET: Auth
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var employee = await _authService.QueryAsync(model.EmployeeNo);

            if (employee == null)
                return RedirectToActionPermanent("Index", "Error", new { error = "员工不存在" });

            return RedirectToActionPermanent("Index", "Company");
        }
    }
}