using hrmana.HRManagement.Application.DTOs;

namespace hrmana.HrManagement.API.Middleware
{
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleWare> _logger;
        public ExceptionHandlingMiddleWare(RequestDelegate next, ILogger<ExceptionHandlingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext http)
        {
            try
            {
                await _next(http);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(http,ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext http,Exception ex)
        {
            var (status, message) = ex switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound,ex.Message),
                ArgumentException => (StatusCodes.Status400BadRequest,ex.Message),
                InvalidOperationException =>(StatusCodes.Status409Conflict,ex.Message),
                _ => (StatusCodes.Status500InternalServerError,"System Error Cuzz")
            };
            if (status >= 500)
            {
                _logger.LogError(ex, "Error At {Method} {Path}", http.Request.Method, http.Request.Path);
            }
            else
            {
                _logger.LogWarning("{Method} {Path} -> {Status}: {Message}", http.Request.Method, http.Request.Path, status, ex.Message);
            }
            http.Response.StatusCode = status;
            await http.Response.WriteAsJsonAsync(BaseResponse<object>.Fail(message));

        }
    }
}
