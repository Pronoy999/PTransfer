using Microsoft.AspNetCore.Mvc;
using PTransfer.Core;
using PTransfer.Models;
using PTransfer.RequestModels;
using PTransfer.Utilities;
using System.Net;
using System.Threading.Tasks;

namespace PTransfer.Controllers {
    [ApiController]
    [Route("/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase {
        [HttpPost]
        public IActionResult Login([FromBody] Authentication authentication) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Response response;
            Users users = AuthHelper.LoginUser(authentication);
            if (users != null) {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, users);
                return new ObjectResult(response);
            }
            response = new Response(Constants.ERROR_MSG,Constants.INVALID_CREDENTIALS_MSG,null);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return new ObjectResult(response);
        }
    }
}