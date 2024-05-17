using KnightsChallenge.Entities.Core.Errors;
using KnightsChallenge.WebApi.Dto;
using Microsoft.AspNetCore.WebUtilities;
using ILogger = Serilog.ILogger;

namespace KnightsChallenge.WebApi.Middlewares;

public class GlobalExceptionMiddleware (RequestDelegate next, ILogger logger)
{
  public async Task InvokeAsync (HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception e)
    {
      logger.Error(e, $"An error ocurred processing the request: {e.Message}");
      await HandleExceptionAsync(context, e);
    }
  }

  private async Task HandleExceptionAsync (HttpContext context, Exception e)
  {
    context.Response.ContentType = "application/json";
    
    ApplicationError err = new InternalServerError(e.Message);

    if (e is ApplicationError)
    {
      err = (e as ApplicationError)!;
    }

    context.Response.StatusCode = err.StatusCode;

    var responseErr = new ErrorResponseDto {
      StatusCode = err.StatusCode,

      Message = err.Message,

      Code = err.Code
    };

    string query = context.Request.QueryString.Value;
    var parsedQuery = QueryHelpers.ParseQuery(query);

    if (parsedQuery.ContainsKey("RedirectUrl"))
    {
      string url = parsedQuery["RedirectUrl"].ToString();

      if (!string.IsNullOrEmpty(url))
      {
        context.Response.Redirect(url);
      }
    }

    await context.Response.WriteAsJsonAsync(responseErr);
  }
}