
using DalApi;

namespace Dal;

internal class SpecialOperationsImplementation : ISpecialOperations
{

    public DateTime? GetStartProjectDate()
    {
        return DataSource.Config.StartProjectDate;
    }

    public DateTime? GetFinishProjectDate()
    {
        return DataSource.Config.FinishProjectDate;
    }

    public void SetStartProjectDate(DateTime value)
    {
        DataSource.Config.StartProjectDate = value;
    }

    public void SetFinishProjectDate(DateTime value)
    {
        DataSource.Config.StartProjectDate = value;
    }

    public void Reset()
    {
        DataSource.Engineers.Clear();
        DataSource.Dependencys.Clear();
        DataSource.Tasks.Clear();
    } 
}
