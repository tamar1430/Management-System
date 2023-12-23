using DO;

namespace BlApi;

public interface IMilestone
{
    public BO.Milestone Read(int id);
    public BO.Milestone Update(int id, string? alias = null, string? description = null, string? remarks = null);
    public List<DO.Dependency> CalculationMilestones(List<Dependency> dependencies);
}
