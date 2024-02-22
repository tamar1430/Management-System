namespace BlApi;

/// <summary>
/// ISpecialOperations
/// </summary>
public interface ISpecialOperations
{
    public DateTime? GetStartProjectDate();
    public DateTime? GetFinishProjectDate();
    public void SetStartProjectDate(DateTime value);
    public void SetFinishProjectDate(DateTime value);
    public void Reset();
    public void Init();
}