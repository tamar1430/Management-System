﻿
namespace DalApi;
using DO;

public interface IDependency
{
    int Create(Dependency dependency); //Creates new entity object in DAL
    Dependency? Read(int id); //Reads entity object by its ID
    List<Dependency> ReadAll(); //stage 1 only, Reads all entity objects
    void Update(Dependency newDependency); //Updates entity object
    void Delete(int id); //Deletes an object by its Id
}
