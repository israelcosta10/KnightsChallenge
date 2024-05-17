using KnightsChallenge.Commands.Errors;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Entities.Core.Errors;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MediatR;

namespace KnightsChallenge.Commands.UpdateKnight;

public class UpdateKnightCommandHandler (IKnightRepository knightRepository, IUnitOfWork unitOfWork)
  : IRequestHandler<UpdateKnightCommand>
{
  public async Task Handle (UpdateKnightCommand request, CancellationToken cancellationToken)
  {
    var knight = await knightRepository.FindByIdAsync(request.AggregateId);

    if (knight is null)
      throw new NotFoundError();

    if (knight.Nickname == request.Payload.Nickname)
      return;

    var existsWithRequestedNickname = await knightRepository.FindByNicknameAsync(request.Payload.Nickname);

    if (existsWithRequestedNickname is not null)
      throw new NicknameAlreadyInUseError();

    knight.UpdateNickname(request.Payload.Nickname);
    knightRepository.Update(knight);

    await unitOfWork.SaveChangesAsync(cancellationToken);
  }
}