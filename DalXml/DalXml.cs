

namespace Dal;
using DalApi;
using System.Diagnostics;

/// <summary>
/// class DalXml
/// </summary>
sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IDependency Dependency => new DependencyImplementation();
}
