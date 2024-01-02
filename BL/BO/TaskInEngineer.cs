namespace BO;

/// <summary>
/// TaskInEngineer class
/// </summary>
public class TaskInEngineer
{
    public int Id {  get; init; }
    public string Alias { get; init; }

    public override string ToString() => this.ToStringProperty();
}
