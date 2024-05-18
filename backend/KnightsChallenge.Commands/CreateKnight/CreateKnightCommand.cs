using KnightsChallenge.Entities.Core;
using MediatR;

namespace KnightsChallenge.Commands.CreateKnight;

public class CreateKnightCommand(CreateKnightCommandPayload payload) : Command<object, CreateKnightCommandPayload>(null, payload), IRequest;