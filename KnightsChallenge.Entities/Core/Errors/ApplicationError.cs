namespace KnightsChallenge.Entities.Core.Errors;

public class ApplicationError (int statusCode, string message, string code) : Exception
{
  public int StatusCode { get; set; } = statusCode;

  public string Message { get; set; } = message;

  public string Code { get; set; } = code;
}