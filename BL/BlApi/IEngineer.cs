namespace BlApi;

/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    public void Create(BO.Engineer engineer);
    public void Delete(int id);
    public BO.Engineer Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public void UpdateEngineerData(BO.Engineer engineer);
}
