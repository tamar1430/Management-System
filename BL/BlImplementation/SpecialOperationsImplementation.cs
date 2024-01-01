namespace BL;
using BlApi;
using BO;

internal class SpecialOperationsImplementation : ISpecialOperations
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public DateTime? GetFinishProjectDate()
    {
        return _dal.SpecialOperations.GetFinishProjectDate();
    }

    public DateTime? GetStartProjectDate()
    {
        return _dal.SpecialOperations.GetStartProjectDate();
    }

    public void SetFinishProjectDate(DateTime value)
    {
        _dal.SpecialOperations.SetFinishProjectDate(value);
    }

    public void SetStartProjectDate(DateTime value)
    {
        _dal.SpecialOperations.SetStartProjectDate(value);
    }

    public void Reset()
    {
        _dal.SpecialOperations.Reset();
    }

}
