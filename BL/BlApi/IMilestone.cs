namespace BlApi;
public interface IMilestone
{
    public BO.Engineer MilestoneDetailsRequest(int id);
    public void UpdateMilestoneData(BO.Milestone milestone);
}
