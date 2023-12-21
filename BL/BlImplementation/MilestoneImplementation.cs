using BlApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{ 
    private DalApi.IDal _dal = DalApi.Factory.Get;


    public BO.Engineer MilestoneDetailsRequest(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateMilestoneData(BO.Milestone milestone)
    {
        throw new NotImplementedException();
    }
}
