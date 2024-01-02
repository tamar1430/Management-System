using DalApi;

namespace BlApi;
public interface Bl
{
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITask Task { get; }
    ISpecialOperations SpecialOperations { get; }
}

