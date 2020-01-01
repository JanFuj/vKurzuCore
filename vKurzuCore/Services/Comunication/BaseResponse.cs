using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vKurzuCore.Services.Comunication
{
    public abstract class BaseResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public string ModelStateErrorKey { get; protected set; }

        public BaseResponse(bool success, string message, string modelStateErrorKey)
        {
            Success = success;
            Message = message;
            ModelStateErrorKey = modelStateErrorKey;
        }
    }
}
