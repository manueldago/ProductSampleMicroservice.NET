using MediatR;
using System.Net;

namespace sharedKernel
{
    public class ResponseAPI : IRequest
    {
        public ExceptionAPI? ResponseException { get; set; }
        public object? ResponseData { get; set; }
        public HttpStatusCode? ResponseStatusCode { get; set; }
        
    }

    public class ExceptionAPI
    {
        public string? Message { get; set; }
        public string? Source { get; set; }
    }

}
