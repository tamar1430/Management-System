

namespace Dal;
using DalApi;

/// <summary>
/// class DalXml
/// </summary>
sealed public class DalXml : IDal
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
