using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core.Errors;
using KnightsChallenge.Queries.Models;
using MediatR;
using MongoDB.Driver;

namespace KnightsChallenge.Queries.GetKnight;

public class GetKnightQueryHandler (IMongoCollection<Knight> knightCollection, IMongoCollection<Hero> heroCollection)
  : IRequestHandler<GetKnightQuery, KnightView>
{
  public async Task<KnightView> Handle (GetKnightQuery request, CancellationToken cancellationToken)
  {
    var knight = (await knightCollection.FindAsync(x => x.Id == request.AggregateId)).FirstOrDefault();

    if (knight is null)
    {
      var hero = (await heroCollection.FindAsync(x => x.Id == request.AggregateId)).FirstOrDefault();

      if (hero is null)
      {
        throw new NotFoundError();
      }

      return KnightView.FromHero(hero);
    }

    return KnightView.FromKnight(knight);
  }
}