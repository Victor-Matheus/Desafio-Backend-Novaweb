using System.Net;
using Controllers.Contracts;

namespace Controllers
{
    public class ControllerResponse : IControllerResponse
    {
        public ControllerResponse(){}
        public ControllerResponse(HttpStatusCode httpStatusCode, bool success, string message, object data)
        {
            // Ensuring not entry bugs
            System.Diagnostics.Debug.Assert(System.Enum.IsDefined(typeof(HttpStatusCode), httpStatusCode));
            System.Diagnostics.Debug.Assert(success == true || success == false);
            System.Diagnostics.Debug.Assert(message.Length > 0);

            HttpStatusCode = httpStatusCode;
            Success = success;
            Message = message;
            Data = data;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}