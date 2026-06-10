using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UsuarioApi.Exceptions.Handlers;

public class GlobalExcptionHandler : IExceptionHandler
{

    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if(exception is not MyCustomException)
        {
            return ValueTask.FromResult(false);
        }

        ProblemDetails problemDetails = new ProblemDetails
        {
            Title = "Houve um problema com a sua requisição",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.WriteAsJsonAsync(problemDetails);
        return ValueTask.FromResult(true);
    }
}