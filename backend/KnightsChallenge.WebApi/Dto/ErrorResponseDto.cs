using KnightsChallenge.Entities.Core.Errors;

namespace KnightsChallenge.WebApi.Dto;

public class ErrorResponseDto
{
  public required int StatusCode { get; set; }

  public required string Message { get; set; }

  public required string Code { get; set; }

  public static ErrorResponseDto FromApplicationError (ApplicationError error)
  {
    return new ErrorResponseDto
    {
      StatusCode = error.StatusCode,

      Code = error.Code,

      Message = error.Message
    };
  }
}