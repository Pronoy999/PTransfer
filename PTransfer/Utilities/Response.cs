using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PTransfer.Utilities {
    public class Response {
        public string ResponseMessage;
        public string ResponseType;
        public object ResponseValue;

        public Response(string responseMessage, string responseType, object responseValue) {
            ResponseMessage = responseMessage;
            ResponseType = responseType;           
            ResponseValue = responseValue;
        }
    }
}
