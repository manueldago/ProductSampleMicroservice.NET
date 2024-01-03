using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ProductRegistrationSystemAPI.sharedKernel
{
    public class RequestTimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(RequestDelegate next, ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = Stopwatch.StartNew();

            await _next(context);

            sw.Stop();

            LogRequestTime(context, sw.ElapsedMilliseconds);
        }

        private void LogRequestTime(HttpContext context, long elapsedMilliseconds)
        {
            try
            {
                var logMessage = $"Request {context.Request.Method} {context.Request.Path} took {elapsedMilliseconds} ms";
                _logger.LogInformation(logMessage);

                string pathDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                bool existDirectory = Directory.Exists(pathDirectory);
                if (!existDirectory)
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                var filePath = Path.Combine(pathDirectory, $"RequestTimeLog.txt");

                File.AppendAllText(filePath, $"{DateTime.Now} - {logMessage}\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }            
        }
    }
}
