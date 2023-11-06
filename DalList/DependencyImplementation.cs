

using DalApi;
using DO;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency dependency)
    {
        int newId = DataSource.Config.NextDependencyId;
        Dependency newDependency = dependency with { Id = newId };
        DataSource.Dependencys.Add(newDependency);
        return newId;
    }

    public void Delete(int id)
    {
        Dependency? dependency = DataSource.Dependencys.Find(dependency => dependency.Id == id);
        if (dependency == null)
            throw new Exception($"Dependency with ID={id} does Not exist");
        else
            DataSource.Dependencys.Remove(dependency);
    }

    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.Find(dependency => dependency.Id == id);
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

    public void Update(Dependency newDependency)
    {
        Dependency? previousDependency = DataSource.Dependencys.Find(dependency => dependency.Id == newDependency.Id);
        if (previousDependency == null)
            throw new Exception($"Dependency with ID={newDependency.Id} does Not exist");
        DataSource.Dependencys.Remove(previousDependency);
        DataSource.Dependencys.Add(newDependency);
    }
}
