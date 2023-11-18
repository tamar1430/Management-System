using System.Linq;
using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// create new dependency
    /// </summary>
    /// <param name="dependency"></param>
    /// <returns>id of the new dependency</returns>
    public int Create(Dependency dependency)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency newDependency = dependency with { Id = newId };
        DataSource.Dependencys.Add(newDependency);
        return newId;
    }

    /// <summary>
    /// delete dependency
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Dependency? dependency = DataSource.Dependencys.Where(dependency => dependency.Id == id).FirstOrDefault();
        if (dependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        else
            DataSource.Dependencys.Remove(dependency);
    }

    /// <summary>
    /// read dependency
    /// </summary>
    /// <param name="id"></param>
    /// <returns>dependency with the if that recived</returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Where(dependency => dependency.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// read all the dependencys
    /// </summary>
    /// <returns>dependencys list</returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    /// <summary>
    /// update dependency
    /// </summary>
    /// <param name="newDependency"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Dependency newDependency)
    {
        Dependency? previousDependency = DataSource.Dependencys.Where(dependency => dependency.Id == newDependency.Id).FirstOrDefault();
        if (previousDependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={newDependency.Id} does Not exist");
        DataSource.Dependencys.Remove(previousDependency);
        DataSource.Dependencys.Add(newDependency);
    }
}
