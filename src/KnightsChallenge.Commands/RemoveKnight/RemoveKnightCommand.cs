using KnightsChallenge.Entities.Core;
using MediatR;

namespace KnightsChallenge.Commands.RemoveKnight;

public class RemoveKnightCommand (string aggregateId) : Command<string, object>(aggregateId, null), IRequest;
