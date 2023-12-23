using BlApi;
using BO;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    internal const int startMilestoneId = 1;
    private static int nextMilestoneId = startMilestoneId;
    internal static int NextnextMilestoneIdId { get => nextMilestoneId++; }

    internal int CreateMilestone(List<int> tasks)
    {

        DO.Task doMilestone = new()
        {
            Id = 0,
            Alias = $"M{nextMilestoneId}",
            IsMilestone = true
        };
        int milestoneId = _dal!.Task!.Create(doMilestone);

        foreach (var taskId in tasks)
        {
            _dal.Dependency.Create(new DO.Dependency(0, taskId, milestoneId));
        }

        return milestoneId;
    }

    internal void updateStartDateAndFinishDate(DateTime startDate, DateTime finishdat)
    {

    }

    internal bool EqualDependencies(List<int> l1, List<int> l2)
    {
        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
                return false;
        }
        return true;
    }

    public BO.Milestone Read(int id)
    {
        DO.Task doMilestone = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"milestone with id:{id} isn't exists");

        var listDependencyTasks = _dal!.Dependency.ReadAll(d => d.DependentTask == id);

        List<BO.TaskInList> listTasks = new();

        foreach (var DependencyTask in listDependencyTasks.ToList())
        {
            var task = _dal!.Task.Read(DependencyTask!.Id);
            listTasks.Add(new BO.TaskInList()
            {
                Id = task!.Id,
                Description = task.Description,
                Alias = task.Alias,
                Status = BO.Tools.status(task)
            });
        }

        int numberOfTasksCompleted = listTasks.Count(t => BO.Tools.status(_dal!.Task!.Read(t!.Id)!) == BO.Status.Done);

        double? CompletionPercentage = (numberOfTasksCompleted / listTasks.Count) * 100;


        BO.Milestone milestone = new()
        {
            Id = id,
            Description = doMilestone.Description,
            Alias = doMilestone.Alias,
            CreatedAtDate = doMilestone.CreatedAtDate,
            Status = doMilestone.ScheduledDate is null ? BO.Status.Unscheduled :
                doMilestone.StartDate is null ? BO.Status.Scheduled :
                doMilestone.CompleteDate is null ? BO.Status.OnTrack :
                BO.Status.InJeopardy,
            ForecastDate = doMilestone.ScheduledDate + doMilestone.ForesastDate,
            DeadlineDate = (DateTime)doMilestone!.DeadLineDate!,
            CompleteDate = doMilestone.CompleteDate,
            CompletionPercentage = CompletionPercentage,
            Remarks = doMilestone.Remarks,
            Dependencies = listTasks
        };


        return milestone;

    }

    public BO.Milestone Update(int id, string? alias = null, string? description = null, string? remarks = null)
    {
        DO.Task doMilestone = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"milestone with id:{id} isn't exists");

        DO.Task newDoMliestone = doMilestone with
        {
            Description = description ?? doMilestone.Description,
            Alias = alias ?? doMilestone.Alias,
            Remarks = remarks ?? doMilestone.Remarks
        };

        _dal.Task.Update(newDoMliestone);

        return Read(id);

    }

    public List<DO.Dependency> CalculationMilestones(List<DO.Dependency> dependencies)
    {
        DO.Task doMilestone = new()
        {
            Id = 0,
            Alias = "start",
            IsMilestone = true
        };
        int idStart = _dal!.Task!.Create(doMilestone);


        var groups = dependencies.GroupBy(d => d.DependentTask)
            .Select(g => new { key = g.Key, tasks = g.Select(s => s.PreviousTask).ToList() });


        foreach (var group in groups)
        {
            group.tasks.Sort();
            group.tasks.Distinct().ToList();
        }

        foreach (var groupD in groups)
        {
            int idMilesone;

            if (groupD.tasks.Count == 0)
                idMilesone = idStart;

            else
            {
                BO.Milestone? milestone = (from milestone1 in _dal.Task.ReadAll(t => t.IsMilestone).ToList()
                                           where EqualDependencies(Read(milestone1.Id).Dependencies.Select(d => d.Id).ToList(), groupD.tasks) == true
                                           select Read(milestone1.Id)).FirstOrDefault();

                idMilesone = milestone is null ? CreateMilestone(groupD.tasks) :
                    milestone.Id;

            }

            _dal.Dependency.Create(new DO.Dependency(0, groupD.key, idMilesone));
        }

        return _dal.Dependency.ReadAll().ToList()!;  
    }

}