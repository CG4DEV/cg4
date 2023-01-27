using System.Collections.Generic;

namespace ProjectName.Domain
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public IDictionary<string, object>? Extensions { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}