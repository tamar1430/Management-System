

namespace Dal;
using DalApi;
using DO;

/// <summary>
/// class DataSource include config and defining the lists
/// </summary>
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


