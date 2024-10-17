using System.Net;

namespace WarehouseTraineeTask.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Что-то пошло не так: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex); 
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Произошла ошибка в системе. Пожалуйста, попробуйте позже.",
                Detailed = exception.Message 
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}