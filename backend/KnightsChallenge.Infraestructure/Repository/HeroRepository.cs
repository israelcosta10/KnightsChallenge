using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MongoDB.Driver;

namespace KnightsChallenge.Infraestructure.Repository;

public class HeroRepository (IUnitOfWork unitOfWork, IMongoCollection<Hero> collection) : IHeroRepository
{
  public async Task<Hero?> FindByIdAsync (string id)
  {
    return (await collection.FindAsync(knight => knight.Id == id)).FirstOrDefault();
  }

  public void Save (Hero aggregate)
  {
    unitOfWork.AddOperation(() => collection.InsertOne(aggregate), aggregate);
  }

  public void Update (Hero aggregate)
  {
    unitOfWork.AddOperation(() => collection.ReplaceOne(k => k.Id == aggregate.Id, aggregate), aggregate);
  }

  public void Delete (Hero aggregate)
  {
    unitOfWork.AddOperation(() => collection.DeleteOne(k => k.Id == aggregate.Id), aggregate);
  }
}