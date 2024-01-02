namespace BL;
using BlApi;
using BO;

/// <summary>
/// SpecialOperationsImplementation
/// </summary>
internal class SpecialOperationsImplementation : ISpecialOperations
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Get Finish Project Date
    /// </summary>
    /// <returns></returns>
    public DateTime? GetFinishProjectDate()
    {
        return _dal.SpecialOperations.GetFinishProjectDate();
    }

    /// <summary>
    /// Get Start Project Date
    /// </summary>
    /// <returns></returns>
    public DateTime? GetStartProjectDate()
    {
        return _dal.SpecialOperations.GetStartProjectDate();
    }

    /// <summary>
    /// Set Finish Project Date
    /// </summary>
    /// <param name="value"></param>
    public void SetFinishProjectDate(DateTime value)
    {
        _dal.SpecialOperations.SetFinishProjectDate(value);
    }

    /// <summary>
    /// Set Start Project Date
    /// </summary>
    /// <param name="value"></param>
    public void SetStartProjectDate(DateTime value)
    {
        _dal.SpecialOperations.SetStartProjectDate(value);
    }

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset()
    {
        _dal.SpecialOperations.Reset();
    }

}
