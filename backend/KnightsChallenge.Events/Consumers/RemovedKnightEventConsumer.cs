using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Entities.Events;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MassTransit;

namespace KnightsChallenge.Events.Consumers;

public class RemovedKnightEventConsumer (IHeroRepository heroRepository, IUnitOfWork unitOfWork)
  : IConsumer<RemovedKnightEvent>
{
  public async Task Consume (ConsumeContext<RemovedKnightEvent> context)
  {
    heroRepository.Save(Hero.Build(context.Message.Name, context.Message.Nickname, context.Message.Birth,
      context.Message.Weapons.Select(w => Weapon.Build(w.Name, w.Mod, w.Attr, w.Equipped)).ToList(),
      Attributes.Build(context.Message.Attributes.Strength, context.Message.Attributes.Dexterity,
        context.Message.Attributes.Constitution, context.Message.Attributes.Intelligence,
        context.Message.Attributes.Wisdom, context.Message.Attributes.Charisma), context.Message.KeyAttribute));

    await unitOfWork.SaveChangesAsync(context.CancellationToken);
  }
}