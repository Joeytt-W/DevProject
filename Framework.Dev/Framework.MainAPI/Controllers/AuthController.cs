using Framework.MainEntity.ViewModel;
using Framework.Service.IService;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Framework.MainAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginModel model,int expireTime = 30)
        {
            var employee = await _authService.QueryAsync(model.EmployeeNo);

            if (employee == null)
                return BadRequest();

            Dictionary<string, string> claimDic = new Dictionary<string, string>();

            claimDic.Add("Username", model.Name);
            claimDic.Add("EmployeeNo", model.EmployeeNo);

            var token = JWTTools.Encode(claimDic, expireTime);

            return Ok(token);
        }
    }
}