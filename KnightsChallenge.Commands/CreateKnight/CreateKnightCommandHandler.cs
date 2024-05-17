using KnightsChallenge.Commands.Errors;
using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MediatR;

namespace KnightsChallenge.Commands.CreateKnight;

public class CreateKnightCommandHandler (IKnightRepository knightRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateKnightCommand>
{
  public async Task Handle (CreateKnightCommand request, CancellationToken cancellationToken)
  {
    var existsWithRequestedNickname = await knightRepository.FindByNicknameAsync(request.Payload.Nickname);

    if (existsWithRequestedNickname is not null)
      throw new NicknameAlreadyInUseError();
    
    knightRepository.Save(Knight.Build(request.Payload.Name, request.Payload.Nickname, request.Payload.Birthday,
      request.Payload.Weapons.Select(w => Weapon.Build(w.Name, w.Mod, w.Attr, w.Equipped)).ToList(),
      Attributes.Build(request.Payload.Attributes.Strength, request.Payload.Attributes.Dexterity,
        request.Payload.Attributes.Constitution, request.Payload.Attributes.Intelligence,
        request.Payload.Attributes.Wisdom, request.Payload.Attributes.Charisma), request.Payload.KeyAttribute));

    await unitOfWork.SaveChangesAsync(cancellationToken);
  }
}