
namespace BlImplementation;

using BL;
using BlApi;
internal class IBl : BlApi.Bl
{
    public IEngineer Engineer => new EngineerImplementation();

    public IMilestone Milestone => new MilestoneImplementation();

    public ITask Task => new TaskImplementation();
    
    public ISpecialOperations SpecialOperations => new SpecialOperationsImplementation();
}
