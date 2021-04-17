using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTransfer.Core;
using PTransfer.Models;
using PTransfer.RequestModels;
using PTransfer.Utilities;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PTransfer.Controllers {
    [ApiController]
    [Route("/users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase {
        [HttpPost]
        public IActionResult RegisterUsers([FromBody] Users Users) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            Users responseUser = UsersHelper.RegisterUser(Users, Users.Password);
            Response response;
            if (responseUser.UserId > -1) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                response = new Response("UserAdded", Constants.SUCCESS_MSG, Users);
                return new ObjectResult(response);
            }
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new Response("User Already Registered.", Constants.ERROR_MSG, null);
            return new ObjectResult(response);
        }
        [HttpGet("{id?}")]
        public IActionResult GetUsers(int id) {
            Response response;
            string token = this.HttpContext.Request.Headers[Constants.JW_TOKEN_KEY];
            string decodedToken = JwTHelper.ValidateJwT(token);
            dynamic tokenData = JObject.Parse(decodedToken);
            int userId = tokenData.UserId;
            if (string.IsNullOrEmpty(decodedToken)) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                response = new Response(Constants.FORBIDDEN_MSG, Constants.ERROR_MSG, null);
                return new ObjectResult(response);
            }
            if (id > 0 && userId == id) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                Users users = UsersHelper.GetUsers(id, string.Empty);
                response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, users);
                return new ObjectResult(response);
            }
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new Response("Invalid Id", Constants.ERROR_MSG, null);
            return new ObjectResult(response);
        }
        [HttpPut]
        public IActionResult UpdateUsers([FromBody] UsersForUpdate users) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Response response;
            string decodedToken = JwTHelper.ValidateJwT(this.HttpContext.Request.Headers[Constants.JW_TOKEN_KEY]);
            dynamic tokenData = JObject.Parse(decodedToken);
            int userId = tokenData.UserId;
            if (string.IsNullOrEmpty(decodedToken) || userId != users.UserId) {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                response = new Response("Invalid JwT", Constants.ERROR_MSG, null);
                return new ObjectResult(response);
            }
            int isUpdated = UsersHelper.UpdateUserDetails(users);
            if (isUpdated > 0) {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, 1);
                return new ObjectResult(response);
            }
            response = new Response(Constants.INTERNAL_SERVER_ERROR_MSG, Constants.ERROR_MSG, null);
            return new ObjectResult(response);
        }
    }
}
