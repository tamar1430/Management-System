
namespace BlApi;

/// <summary>
/// Factory
/// </summary>
public static class Factory
{
    public static Bl Get() => new BlImplementation.IBl();
}
