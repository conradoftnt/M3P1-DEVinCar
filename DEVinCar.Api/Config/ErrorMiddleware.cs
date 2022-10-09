using System.Net;
using DEVinCar.Domain.Exceptions;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Api.Config;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await TratamentoExcecao(context, ex);
        }
    }

    private Task TratamentoExcecao(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        string message;

        if (ex is BadRequestException)
        {
            status = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (ex is NoContentException)
        {
            status = HttpStatusCode.NoContent;
            message = ex.Message;
        }
        else if (ex is NotFoundException)
        {
            status = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = "An internal server error has occurred, please contact IT";
        }

        var response = new ErrorDTO(message);

        context.Response.StatusCode = (int) status;

        return context.Response.WriteAsJsonAsync(response);
    }
}