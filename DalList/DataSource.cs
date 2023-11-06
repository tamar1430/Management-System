

namespace Dal;
using DO;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTaskId = 1000;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
    internal static List<Task> Tasks { get; } = new();
    internal static List<Dependency> Dependencys { get; } = new();
    internal static List<Engineer> Engineers { get; } = new();
}
