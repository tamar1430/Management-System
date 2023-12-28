
namespace Dal;
using DalApi;

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

    public ISpecialOperations SpecialOperations => new SpecialOperationsImplementation();
}
