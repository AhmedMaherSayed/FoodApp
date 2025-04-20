using FoodApp.Domain.Data.Enums;
using FoodApp.Presentation.ViewModel;

namespace FoodApp.Presentation.MiddleWare
{
    public class GlobalErrorHandlingMiddleware(ILogger logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await context.Response
                    .WriteAsJsonAsync(ResponseViewModel<bool>.Failure(ErrorCode.BadRequest, $"{ex.InnerException?.Message ?? ex.Message}"));

                logger.LogError(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
