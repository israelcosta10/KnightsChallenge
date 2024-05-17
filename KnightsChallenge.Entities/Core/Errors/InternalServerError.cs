namespace KnightsChallenge.Entities.Core.Errors;

public class InternalServerError (string message) : ApplicationError(500, message, "INTERNAL_SERVER");