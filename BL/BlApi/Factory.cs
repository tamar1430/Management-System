
namespace BlApi;

public static class Factory
{
    public static Bl Get() => new BlImplementation.IBl();
}
