namespace BlApi;

/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    public IEnumerable<BO.Engineer> GetEngineersList(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer EngineerDetailsRequest(int id);
    public void AddEngineer(BO.Engineer engineer);
    public void DeleteEngineer(int id);
    public void UpdateEngineerData(BO.Engineer engineer);
}
