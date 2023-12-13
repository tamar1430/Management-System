using BlApi;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void AddEngineer(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }

    public void DeleteEngineer(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Engineer EngineerDetailsRequest(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer> GetEngineersList(Func<BO.Engineer, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void UpdateEngineerData(BO.Engineer engineer)
    {
        throw new NotImplementedException();
    }
}
