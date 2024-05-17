using KnightsChallenge.Entities.Core;
using KnightsChallenge.Entities.Core.Errors;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MassTransit;
using MediatR;

namespace KnightsChallenge.Commands.RemoveKnight;

public class RemoveKnightCommandHandler (IKnightRepository knightRepository, IUnitOfWork unitOfWork, IBus bus) : IRequestHandler<RemoveKnightCommand>
{
  public async Task Handle (RemoveKnightCommand request, CancellationToken cancellationToken)
  {
    var knight = await knightRepository.FindByIdAsync(request.AggregateId);

    if (knight is null)
      throw new NotFoundError();

    knight.Delete();
    knightRepository.Delete(knight);

    await unitOfWork.SaveChangesAsync(cancellationToken);
  }
}