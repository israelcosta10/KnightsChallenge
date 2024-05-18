namespace KnightsChallenge.Entities.Core.Errors;

public class NotFoundError () : ApplicationError(404, "Resource not found", "RESOURCE_NOT_FOUND");