using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Services.Comunication
{
    public class Response
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public string ModelStateErrorKey { get; protected set; }

        public Response(bool success, string message, string modelStateErrorKey)
        {
            Success = success;
            Message = message;
            ModelStateErrorKey = modelStateErrorKey;
        }
        public Response()
        {
            Success = true;
        }
        public Response(string message, string modelStateErrorKey)
        {
            Success = false;
            Message = message;
            ModelStateErrorKey = modelStateErrorKey;
        }
    }
}
