using Microsoft.AspNetCore.Http;
using System.Net;

namespace NEWZEAL_LAND_WORK_API.Middleware
{
    public class ExceptionHandlerMiddleWare : Exception
    {
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;
        private readonly RequestDelegate requestDelegate;

        public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> logger, RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {

                var errorId = Guid.NewGuid();

                logger.LogError(ex, $"{errorId}  : {ex.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
               

                var error = new
                {
                    id = errorId,
                    ErrorMessage = "Something went wrong! we are looking into resolving this."

                };
                await httpContext.Response.WriteAsJsonAsync(error);
                 
            }
        }
        //public async Task InvokeAsync(HttpContext context)
        //{
        //    try
        //    {
        //        await requestDelegate(context);
        //    }
        //    catch (ExceptionHandlerMiddleWare ex)
        //    {
        //        logger.LogError(ex, ex.Message);
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        await context.Response.WriteAsync("An error occurred: " + ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "An unexpected error occurred.");
        //        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //        await context.Response.WriteAsync("An unexpected error occurred.");
        //    }
        //}
    }

}

