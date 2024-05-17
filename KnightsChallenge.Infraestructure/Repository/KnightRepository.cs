﻿using KnightsChallenge.Entities;
using KnightsChallenge.Entities.Core;
using KnightsChallenge.Infraestructure.Repository.Contracts;
using MongoDB.Driver;

namespace KnightsChallenge.Infraestructure.Repository;

public class KnightRepository (IMongoClient mongoClient, IUnitOfWork unitOfWork) : IKnightRepository
{
  private readonly IMongoCollection<Knight> collection =
    mongoClient.GetDatabase("MONGO_DB_CONNECTION_DATABASE").GetCollection<Knight>("knights");
  
  public async Task<Knight?> FindByNicknameAsync (string nickname)
  {
    return (await collection.FindAsync(knight => knight.Nickname == nickname)).FirstOrDefault();
  }
  
  public async Task<Knight?> FindByIdAsync (string id)
  {
    return (await collection.FindAsync(knight => knight.Id == id)).FirstOrDefault();
  }

  public void Save (Knight aggregate)
  {
    unitOfWork.AddOperation(() => collection.InsertOne(aggregate), aggregate);
  }
  
  public void Update (Knight aggregate)
  {
    unitOfWork.AddOperation(() => collection.ReplaceOne(k => k.Id == aggregate.Id, aggregate), aggregate);
  }

  public void Delete (Knight aggregate)
  {
    unitOfWork.AddOperation(() => collection.DeleteOne(k => k.Id == aggregate.Id), aggregate);
  }
}