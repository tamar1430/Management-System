using BlApi;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    internal const int startMilestoneId = 1;
    private static int nextMilestoneId = startMilestoneId;
    internal static int NextMilestoneId { get => nextMilestoneId++; }

    public BO.Milestone Read(int id)
    {
        DO.Task doMilestone = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"milestone with id:{id} isn't exists");
        if (!doMilestone.IsMilestone)
            throw new BO.BlDoesNotExistException($"milestone with id:{id} isn't exists");

        var listDependencyTasks = _dal!.Dependency.ReadAll(d => d.DependentTask == id).Select(d=>d.PreviousTask);

        List<BO.TaskInList> listTasks = new();

        foreach (var DependencyTask in listDependencyTasks.ToList())
        {
            DO.Task task = _dal!.Task.Read(DependencyTask)!;
            BO.TaskInList taskInList = new()
            {
                Id = task!.Id,
                Description = task.Description,
                Alias = task.Alias,
                Status = BO.Tools.status(task)
            };
            listTasks.Add(taskInList);
        }

        int numberOfTasksCompleted = listTasks.Count(t => BO.Tools.status(_dal!.Task!.Read(t!.Id)!) == BO.Status.Done);

        double CompletionPercentage = numberOfTasksCompleted>0 ?
            (numberOfTasksCompleted / listTasks.Count) * 100
            :0;
        BO.Status status = BO.Tools.status(doMilestone);


        BO.Milestone milestone = new()
        {
            Id = id,
            Description = doMilestone.Description,
            Alias = doMilestone.Alias,
            CreatedAtDate = doMilestone.CreatedAtDate,
            Status = status,
            ForecastDate = doMilestone.ForesastDate,
            DeadlineDate = doMilestone!.DeadLineDate is not null?((DateTime)doMilestone!.DeadLineDate!):null,
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

    private int CreateMilestone(List<int> tasks)
    {

        DO.Task doMilestone = new()
        {
            Id = 0,
            Alias = $"M{NextMilestoneId}",
            IsMilestone = true
        };
        int milestoneId = _dal!.Task!.Create(doMilestone);

        foreach (var taskId in tasks)
        {
            _dal.Dependency.Create(new DO.Dependency(0, milestoneId, taskId));
        }

        return milestoneId;
    }

    private void UpdateStartDateAndFinishDate(DateTime startDate, DateTime finishDate)
    {
        _dal!.SpecialOperations!.SetStartProjectDate(startDate);
        _dal!.SpecialOperations!.SetFinishProjectDate(finishDate);
    }

    private bool EqualDependencies(List<int> list1, List<int> list2)
    {
        // Check if the lists have the same length
        if (list1.Count != list2.Count)
        {
            return false;
        }

        // Create a dictionary to store the count of each element in list1
        Dictionary<int, int> elementsCount = new Dictionary<int, int>();

        // Count the occurrences of each element in list1
        foreach (int num in list1)
        {
            if (elementsCount.ContainsKey(num))
            {
                elementsCount[num]++;
            }
            else
            {
                elementsCount[num] = 1;
            }
        }

        // Check the elements count in list2
        foreach (int num in list2)
        {
            // If an element is missing or the count is 0, the lists are not equal
            if (!elementsCount.ContainsKey(num) || elementsCount[num] == 0)
            {
                return false;
            }

            // Decrement the count of the element in the dictionary
            elementsCount[num]--;
        }

        // If all elements are matched and their count is 0, the lists are equal
        return elementsCount.Values.All(count => count == 0);
    }

    /// <summary>
    /// function thatCalculation all the milestones
    /// </summary>
    /// <param name="dependencies">all the prevois depenencies</param>
    /// <returns></returns>
    private List<DO.Dependency> CalculationMilestones(List<DO.Dependency> dependencies)
    {
        //delete all the previos depenency
        _dal.Dependency.ReadAll().ToList().ForEach(d => _dal.Dependency.Delete(d!.Id));
        //start milestone
        DO.Task doMilestoneStart = new()
        {
            Id = 0,
            Alias = "start",
            IsMilestone = true
        };
        int idStart = _dal!.Task!.Create(doMilestoneStart);

        List<int> tasksId = _dal.Task.ReadAll().ToList().Where(t => !t!.IsMilestone).Select(m => m!.Id).ToList();

        var groups = dependencies.GroupBy(d => d.DependentTask)
            .Select(g => new { key = g.Key, tasks = g.Select(s => s.PreviousTask).ToList() });

        foreach (var taskId in tasksId)
        {
            if (!groups.Select(g => g.key).ToList().Contains(taskId))
                _dal.Dependency.Create(new DO.Dependency(0, taskId, idStart));
        }

        foreach (var group in groups)
        {
            group.tasks.Sort();
            group.tasks.Distinct().ToList();
        }

        foreach (var groupD in groups)
        {
            int idMilesone;

            BO.Milestone? milestone = (from milestone1 in _dal.Task.ReadAll(t => t.IsMilestone && t.Alias!="start").ToList()
                                       where EqualDependencies(Read(milestone1.Id).Dependencies.Select(d => d.Id).ToList(), groupD.tasks) == true
                                       select Read(milestone1.Id)).FirstOrDefault();

            idMilesone = milestone is null ? CreateMilestone(groupD.tasks) :
                milestone.Id;

            _dal.Dependency.Create(new DO.Dependency(0, groupD.key, idMilesone));
        }

        List<int> prevTasks = dependencies.GroupBy(d => d.PreviousTask).Select(g => g.Key).ToList();
        
        //end milestone
        DO.Task doMilestoneEnd = new()
        {
            Id = 0,
            Alias = "end",
            IsMilestone = true
        };
        int idEnd = _dal!.Task!.Create(doMilestoneEnd);
        foreach (var taskId in tasksId)
        {
            if (!prevTasks.Contains(taskId))
            {
                _dal.Dependency.Create(new DO.Dependency(0, idEnd, taskId));
            }
        }

        return _dal.Dependency.ReadAll().ToList()!;
    }

    private void CalculationTimeTasksAndMilestones()
    {
        List<int> lastTasks = Read(_dal!.Task!.Read(m => m.Alias == "end")!.Id).Dependencies.ToList().Select(t => t.Id).ToList();
        foreach (var task in lastTasks)
        {
            DO.Task prevTask = _dal.Task.Read(task)!;
            DO.Task newTask = prevTask with
            { DeadLineDate = _dal.SpecialOperations.GetFinishProjectDate() - prevTask.RequiredEffortTime };
            _dal.Task.Update(newTask);
            UpdateDeadLineDates(_dal.Task.Read(newTask!.Id)!);

        }

        void UpdateDeadLineDates(DO.Task task)
        {
            List<int>? prevTasks = Read(_dal.Dependency.Read(d => d!.DependentTask == task.Id)!.PreviousTask)?.Dependencies.Select(t => t.Id).ToList();
            if (prevTasks is not null)
            {
                foreach (var taskId in prevTasks)
                {
                    DO.Task taskD = _dal.Task.Read(taskId)!;
                    DateTime deadLine = (DateTime)(task.DeadLineDate! - task.RequiredEffortTime!)!;
                    _dal.Task.Update(taskD
                      with
                    {
                        DeadLineDate = taskD.DeadLineDate is null ? deadLine
                        : DateTime.Compare((DateTime)taskD.DeadLineDate, deadLine) > 0 ?
                        deadLine : taskD.DeadLineDate//,
                        //ScheduledDate = task.DeadLineDate - task.RequiredEffortTime - taskD.RequiredEffortTime
                    });

                    UpdateDeadLineDates(_dal.Task.Read(taskD!.Id)!);
                }
            }
        }
        
        List<int> firstTasks = _dal.Dependency.ReadAll(d=>d.PreviousTask == _dal!.Task!.Read(m => m.Alias == "start")!.Id).Select(d=>d!.DependentTask).ToList();
        foreach (var task in firstTasks)
        {
            DO.Task prevTask = _dal.Task.Read(task)!;
            DO.Task newTask = prevTask with
            { ScheduledDate = _dal.SpecialOperations.GetStartProjectDate() };
            _dal.Task.Update(newTask);
            UpdateScheduledDates(_dal.Task.Read(prevTask!.Id)!);

        }

        void UpdateScheduledDates(DO.Task task)
        {
            var milestones = _dal.Task.ReadAll(t => t.IsMilestone).Where(m => Read(m!.Id).Dependencies.Select(t => t.Id).Contains(task.Id)).Select(m => m!.Id).ToList();
            if (milestones.Any())
            {
                List<DO.Task> tasks = _dal.Task.ReadAll(t => !t.IsMilestone).ToList()!;
                var depTasks = tasks.Where(t => _dal.Dependency.Read(d => milestones.Contains(d.PreviousTask) && d.DependentTask == t.Id) is not null).ToList();

                foreach (var taskD in depTasks)
                {
                    DateTime scheduledDate = taskD.ScheduledDate is null ?
                        (DateTime)(task.ScheduledDate!+task.RequiredEffortTime)! :
                        DateTime.Compare((DateTime)task.ScheduledDate!, (DateTime)taskD.ScheduledDate!) > 0
                        ? (DateTime)task.ScheduledDate!
                        : (DateTime)taskD.ScheduledDate!;
                    _dal.Task.Update(taskD with
                    //{ ScheduledDate = task.DeadLineDate - task.RequiredEffortTime });
                    {
                        ScheduledDate = scheduledDate,
                        ForesastDate = scheduledDate + taskD.RequiredEffortTime
                    });

                    UpdateScheduledDates(_dal.Task.Read(taskD!.Id)!);
                }

            }
        }

        List<BO.Milestone> milestones = _dal.Task.ReadAll(t => t.IsMilestone).Select(m => Read(m!.Id)).ToList()!;
        foreach (var milestone in milestones)
        {
            DO.Task doMilestone = _dal.Task.Read(milestone.Id)!;
            DO.Task updateMilestone = doMilestone with
            {
                ScheduledDate = milestone.Dependencies is not null?
                milestone.Dependencies.Select(t => _dal.Task.Read(t.Id)!.ScheduledDate).Min()
                :_dal.SpecialOperations.GetStartProjectDate(),
                DeadLineDate = milestone.Dependencies is not null ?
                milestone.Dependencies.Select(t => _dal.Task.Read(t.Id)!.DeadLineDate).Max()
                :_dal.SpecialOperations.GetFinishProjectDate(),
            };
            _dal.Task.Update(updateMilestone);
            
        }
    }

    private void UpdateMilestonesNames()
    {
        List<BO.Milestone> milestones = _dal.Task.ReadAll(t => t.IsMilestone && t.Alias!="start" && t.Alias!="end").Select(m => Read(m!.Id)).ToList()!;
        foreach (var milestone in milestones)
        {
            DO.Task doMilestone = _dal.Task.Read(milestone.Id)!;
            string alias = $"{doMilestone.Alias}: ";
            List<string> tasksAlias = milestone.Dependencies.Select(t => _dal.Task.Read(t.Id)!.Alias).ToList();
            foreach (var taskAlias in tasksAlias)
            {
                alias += $"{taskAlias}, ";
            }
            _dal.Task.Update(doMilestone with
            {
                ScheduledDate = milestone.Dependencies.Select(t => _dal.Task.Read(t.Id)!.ScheduledDate).Min(),
                Alias = alias

            });
        }
    }

    public void CreatingProjectSchedule(DateTime startDate, DateTime finishDate)
    {
        UpdateStartDateAndFinishDate(startDate, finishDate);
        CalculationMilestones(_dal!.Dependency!.ReadAll()!.ToList()!);
        CalculationTimeTasksAndMilestones();
        UpdateMilestonesNames();
    }

}
