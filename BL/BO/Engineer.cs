using System.Reflection;
using System.Text;

namespace BO;

/// <summary>
/// Engineer class
/// </summary>
public class Engineer
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public TaskInEngineer? Task { get; set; }

    public override string ToString() => this.ToStringProperty();
}
