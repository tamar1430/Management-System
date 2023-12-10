namespace BlApi;

/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    public IEnumerable<BO.Engineer> getEngineersList(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer EngineerDetailsRequest(int id);
    public void addEngineer(BO.Engineer engineer);
    public void deleteEngineer(int id);
    public void updateEngineerData(BO.Engineer engineer);
}
