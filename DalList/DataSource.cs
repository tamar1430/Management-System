

namespace Dal;

using DalApi;
using DO;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 1;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependencyId = 1;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
    internal static List<Task> Tasks { get; } = new();
    internal static List<Engineer> Engineers { get; } = new();
    internal static List<Dependency> Dependencys { get; } = new();
}

public class RandomIdEngineer
{
    public static int randomIdEngineer()
    {
        Random random = new Random();
        int randomIndex = random.Next(DataSource.Engineers.Count);
        Engineer randomEngineer = DataSource.Engineers[randomIndex];
        return randomEngineer.Id;
    }
}
