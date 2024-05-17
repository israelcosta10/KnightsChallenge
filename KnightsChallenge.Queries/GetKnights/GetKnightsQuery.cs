using KnightsChallenge.Entities.Core;
using KnightsChallenge.Queries.Models;
using MediatR;

namespace KnightsChallenge.Queries.GetKnights;

public class GetKnightsQuery (GetKnightsQueryParams parameters)
  : Query<object, GetKnightsQueryParams>(null, parameters), IRequest<List<KnightView>>;