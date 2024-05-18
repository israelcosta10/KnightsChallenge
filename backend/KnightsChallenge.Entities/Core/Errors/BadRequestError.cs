namespace KnightsChallenge.Entities.Core.Errors;

public class BadRequestError () : ApplicationError(400, "Bad request", "BAD_REQUEST");