using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PTransfer.Core;
using PTransfer.RequestModels;
using PTransfer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PTransfer.Controllers {
    [ApiController]
    [Route("/upload-request")]
    [Produces("application/json")]
    public class RequestController : ControllerBase {
        [HttpPost]
        public IActionResult CreateRequest([FromBody] UploadRequestCreate uploadRequestCreate) {
            Response response;
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            string token = this.HttpContext.Request.Headers[Constants.JW_TOKEN_KEY];
            var decodedToken = JwTHelper.ValidateJwT(token);
            dynamic tokenData = JObject.Parse(decodedToken);
            if (string.IsNullOrEmpty(decodedToken) || (int)tokenData.UserId <= 0) {
                response = new Response(Constants.FORBIDDEN_MSG, Constants.ERROR_MSG, null);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ObjectResult(response);
            }
            int requestId = UploadRequestHelper.CreateRequest(uploadRequestCreate, (int)tokenData.UserId);
            response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, requestId);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return new ObjectResult(response);
        }
        [HttpPut]
        public IActionResult UpdateRequest([FromBody] UploadRequestUpdate uploadRequestUpdate) {
            Response response;
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            string token = HttpContext.Request.Headers[Constants.JW_TOKEN_KEY];
            if (string.IsNullOrEmpty(token)) {
                response = new Response(Constants.FORBIDDEN_MSG, Constants.ERROR_MSG, null);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new ObjectResult(response);
            }
            var decodedToken = JwTHelper.ValidateJwT(token);
            dynamic tokenData = (!string.IsNullOrEmpty(decodedToken) ? JObject.Parse(decodedToken) : null);            
            int requestId = UploadRequestHelper.UpdateRequest(uploadRequestUpdate, (int)tokenData.UserId);
            FileUploader.MergeFileAndUpload(uploadRequestUpdate);
            response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, requestId);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            return new ObjectResult(response);
        }
    }
}
