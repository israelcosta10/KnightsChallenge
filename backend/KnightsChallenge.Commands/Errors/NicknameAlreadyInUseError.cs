using KnightsChallenge.Entities.Core.Errors;

namespace KnightsChallenge.Commands.Errors;

public class NicknameAlreadyInUseError () : ApplicationError(400, "Nickname already in use", "NICKNAME_ALREADY_IN_USE");