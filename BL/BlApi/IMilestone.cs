using DO;

namespace BlApi;

public interface IMilestone
{
    public BO.Milestone Read(int id);
    public BO.Milestone Update(int id, string? alias = null, string? description = null, string? remarks = null);
    public void CreatingProjectSchedule(DateTime startDate, DateTime finishDate);
}
