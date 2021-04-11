using Microsoft.AspNetCore.Mvc;
using PTransfer.Core;
using PTransfer.Models;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PTransfer.Controllers {
    [ApiController]
    [Route("/users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase {
        [HttpPost]
        public IActionResult RegisterUsers([FromBody] Users Users, string Password) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            bool isUserCreated = UsersHelper.RegisterUser(Users, Password);
            Response response;
            if (isUserCreated) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                response = new Response("UserAdded", "Success", Users);
                return new ObjectResult(response);
            }
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new Response("User Already Registered.", "Error", null);
            return new ObjectResult(response);
        }
        [HttpGet("{id?}")]
        public IActionResult GetUsers(int id) {
            Response response;
            if (id > 0) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                Users users = UsersHelper.GetUsers(id, string.Empty);
                response = new Response("Success", "Success", users);
                return new ObjectResult(response);
            }
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new Response("Invalid Id", "Error", null);
            return new ObjectResult(response);
        }
    }
}
