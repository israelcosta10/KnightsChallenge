using KnightsChallenge.Entities;
using KnightsChallenge.Queries.Models;
using MediatR;
using MongoDB.Driver;

namespace KnightsChallenge.Queries.GetKnights;

public class GetKnightsQueryHandler (
  IMongoCollection<Knight> knightsCollection,
  IMongoCollection<Hero> heroesCollection) : IRequestHandler<GetKnightsQuery, List<KnightView>>
{
  public async Task<List<KnightView>> Handle (GetKnightsQuery request, CancellationToken cancellationToken)
  {
    if (request.Parameters.Filter?.ToLower() == "heroes")
      return heroesCollection.Find(x => true).ToList().Select(KnightView.FromHero).ToList();

    return knightsCollection.Find(x => true).ToList().Select(KnightView.FromKnight).ToList();
  }
}