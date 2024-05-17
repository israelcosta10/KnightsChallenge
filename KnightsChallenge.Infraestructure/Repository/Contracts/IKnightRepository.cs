using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;

namespace KnightsChallenge.Infraestructure.Repository.Contracts;

public interface IKnightRepository : IRepository<Knight>
{
  Task<Knight?> FindByNicknameAsync (string nickname);
}