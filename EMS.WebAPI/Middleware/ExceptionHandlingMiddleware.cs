using EMS.WebAPI.Exceptions;
using System.Net;

namespace EMS.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Internal Server Error. Please try again later.";
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Exception: {exception.Message} {Environment.NewLine}{exception.StackTrace}{Environment.NewLine}";

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;
                default:
                    LogExceptionToFile(logMessage);
                    break;
            }

            LogExceptionToFile(logMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(message);
        }

        public void LogExceptionToFile(string logMessage)
        {
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "exceptions.txt");

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); 
                File.AppendAllText(logFilePath, logMessage); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log exception to file: {ex.Message}");
            }
        }
    }

}
