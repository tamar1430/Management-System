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
    /// Reads dependency that meet a certain condition
    /// </summary>
    /// <param name="filter">Pointer to a boolean function, delegate of type Func</param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencys.Where(filter).FirstOrDefault();
    }

    /// <summary>
    /// Read all dependencys or dependencys that meet a certain condition
    /// </summary>
    /// <returns>dependencys list</returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter == null)
            return DataSource.Dependencys.Select(dependency => dependency);
        else
            return DataSource.Dependencys.Where(filter);
    }

    /// <summary>
    /// update dependency
    /// </summary>
    /// <param name="newDependency"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Dependency dependency)
    {
        Dependency? previousDependency = DataSource.Dependencys.Where(dependency1 => dependency1.Id == dependency.Id).FirstOrDefault();
        if (previousDependency == null)
            throw new DalDoesNotExistException($"Dependency with ID={dependency.Id} does Not exist");
        DataSource.Dependencys.Remove(previousDependency);
        DataSource.Dependencys.Add(dependency);
    }
}
