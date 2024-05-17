using KnightsChallenge.Entities.Core;
using KnightsChallenge.Queries.Models;
using MediatR;

namespace KnightsChallenge.Queries.GetKnight;

public class GetKnightQuery (string aggregateId) : Query<string, object>(aggregateId, null), IRequest<KnightView>;