using KnightsChallenge.Entities.Core;
using MediatR;

namespace KnightsChallenge.Commands.UpdateKnight;

public class UpdateKnightCommand (string knightId, UpdateKnightCommandPayload payload)
  : Command<string, UpdateKnightCommandPayload>(knightId, payload), IRequest;
