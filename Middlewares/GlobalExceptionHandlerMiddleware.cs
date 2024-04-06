using FiyiStore.Areas.BasicCore.Interfaces;
using System.Net;
using System.Text.Json;
using FiyiStore.Areas.BasicCore.Entities;

namespace FiyiStore.Middlewares;
public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, 
        IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        using IServiceScope serviceScope = _serviceProvider.CreateScope();

        IFailureRepository failureRepository = serviceScope.ServiceProvider.GetRequiredService<IFailureRepository>();

        DateTime Now = DateTime.Now;
        Failure Failure = new()
        {
            Message = exception.Message,
            EmergencyLevel = 1,
            StackTrace = exception.StackTrace ?? "",
            Source = exception.Source ?? "",
            Comment = "",
            Active = true,
            UserCreationId = context.Session.GetInt32("UserId") ?? 1,
            UserLastModificationId = context.Session.GetInt32("UserId") ?? 1,
            DateTimeCreation = Now,
            DateTimeLastModification = Now,
        };
        //TODO convertirlo en async
        failureRepository.Add(Failure);

        ExceptionResponse exceptionResponse = new()
        {
            Status = context.Response.StatusCode,
            Message = exception.Message,
            NewPathOrQuestion = $@"¿Quiere empezar del principio?
 Ha habido una falla temporal"
        };

        var jsonContent = JsonSerializer.Serialize(exceptionResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return context.Response.WriteAsync(jsonContent);
    }
}