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
    [Produces("application/json")]
    [Route("/upload-request/parts")]
    public class RequestPartsController : ControllerBase {
        [HttpPost]
        public IActionResult CreateRequestParts([FromBody] UploadRequestPartsCreate uploadRequestPartsCreate) {
            Response response;
            if (!ModelState.IsValid) {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return BadRequest(ModelState);
            }
            string token = HttpContext.Request.Headers[Constants.JW_TOKEN_KEY];
            if (token == null) {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                response = new Response(Constants.FORBIDDEN_MSG, Constants.ERROR_MSG, null);
                return new ObjectResult(response);
            }
            dynamic tokenData = JObject.Parse(JwTHelper.ValidateJwT(token));
            int partsId = UploadRequestHelper.UploadRequestParts(uploadRequestPartsCreate, (int)tokenData.UserId);
            response = new Response(Constants.SUCCESS_MSG, Constants.SUCCESS_MSG, partsId);
            return new ObjectResult(response);
        }
    }
}
