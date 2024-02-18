namespace BlApi;

/// <summary>
/// IEngineer
/// </summary>
public interface IEngineer
{
    public void Create(BO.Engineer engineer);
    public void Delete(int id);
    public BO.Engineer Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<DO.Engineer, bool>? filter = null);
    public void Update(BO.Engineer engineer);
}
