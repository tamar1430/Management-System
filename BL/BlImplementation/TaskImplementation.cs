
namespace BlImplementation;

/// <summary>
/// TaskImplementation
/// </summary>
internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// create task
    /// </summary>
    /// <param name="boTask"></param>
    /// <exception cref="BO.BlInorrectData"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public void Create(BO.Task boTask)
    {
        if (boTask.Id <= 0) throw new BO.BlInorrectData("Task's id isn't correct");
        if (boTask.Alias.Length <= 0) throw new BO.BlInorrectData("Task's Alias isn't correct");

        DO.Task doTask = new(boTask.Id, boTask.Description,
            boTask.Alias, false,
            boTask.CreatedAtDate, (DO.EngineerExperience?)boTask!.CopmlexityLevel,
            boTask.ScheduledStartDate, boTask.StartDate,
            boTask.RequiredEffortTime, boTask.ForecastDate, boTask.DeadLine,
            boTask.CompleteDate, boTask.Deliverable,
            boTask.Remarks, boTask!.Engineer?.Id
            );

        try
        {
            int idTask = _dal.Task.Create(doTask);
            foreach (var prevTask in boTask.Dependencies)
            {
                _dal.Dependency.Create(new DO.Dependency(0, idTask, prevTask.Id));
            }

        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={doTask.Id} already exists", ex);
        }
    }

    /// <summary>
    /// delete task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDeletionImpossible"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        if (_dal.Dependency.ReadAll().Where(d => d!.PreviousTask == id).First() != null)
        {
            throw new BO.BlDeletionImpossible("can't delete this task");
        }

        try
        {
            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} doesn't exists", ex);
        }
    }

    /// <summary>
    /// read all tasks
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task
                .ReadAll(filter != null ? (Func<DO.Task, bool>)filter : null).Where(t => !t!.IsMilestone)
                select Read(doTask.Id));
    }

    /// <summary>
    /// read task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask is null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} doesn't exists");
        }

        var listDependencyTasks = _dal!.Dependency.ReadAll(d => d.DependentTask == id);

        List<BO.TaskInList> listTasks = new();

        foreach (var DependencyTask in listDependencyTasks.ToList())
        {
            var task = _dal!.Task.Read(DependencyTask!.PreviousTask);
            listTasks.Add(new BO.TaskInList()
            {
                Id = task!.Id,
                Description = task.Description,
                Alias = task.Alias,
                Status = BO.Tools.status(task)
            });
        }

        int? milestonId = _dal.Dependency.Read(d => d.DependentTask == doTask.Id) is not null ?
            _dal.Dependency.Read(d => d.DependentTask == doTask.Id)!.PreviousTask : null;

        BO.MilestoneInTask? milestone = milestonId is not null ?
            new()
            {
                Id = (int)milestonId,
                Alias = _dal.Task.Read((int)milestonId)!.Alias,
            }
        : null;



        BO.Task boTask = new()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = BO.Tools.status(doTask),
            Milestone = milestone,
            Dependencies = listTasks,
            StartDate = doTask.StartDate,
            ScheduledStartDate = doTask.ScheduledDate,
            ForecastDate = doTask.ForesastDate,
            RequiredEffortTime = doTask.RequiredEffortTime,
            DeadLine = doTask.DeadLineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverable = doTask.Deliverable,
            Remarks = doTask.Remarks,
            Engineer = doTask.EngineerId is not null ? new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal!.Engineer!.Read((int)doTask!.EngineerId)!.Name } : null,
            CopmlexityLevel = (BO.EngineerExperience?)doTask.CopmlexityLevel
        };
        return boTask;
    }

    /// <summary>
    /// update task
    /// </summary>
    /// <param name="boTask"></param>
    /// <exception cref="BO.BlInorrectData"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0) throw new BO.BlInorrectData("Task's id isn't correct");
        if (boTask.Alias is null || boTask.Alias.Length <= 0) throw new BO.BlInorrectData("Task's Alias isn't correct");

        if (boTask.Dependencies is not null) foreach (var prevTask in boTask.Dependencies)
            {
                if (CheckDependency(prevTask, boTask))
                    _dal.Dependency.Create(new DO.Dependency(0, boTask.Id, prevTask.Id));
            }

        DO.Task? prevDoTask = _dal!.Task.Read(boTask.Id);

        if (prevDoTask is null) throw new BO.BlDoesNotExistException($"task with id:{boTask.Id} isn't exists");

        DO.Task newDoTask = new(boTask.Id, boTask.Description,
            boTask.Alias, false,
            boTask.CreatedAtDate, (DO.EngineerExperience?)boTask!.CopmlexityLevel,
            boTask.ScheduledStartDate, boTask.StartDate,
            boTask.RequiredEffortTime, boTask.ForecastDate, boTask.DeadLine,
            boTask.CompleteDate, boTask.Deliverable,
            boTask.Remarks, boTask!.Engineer?.Id
            );
        _dal.Task.Update(newDoTask);
    }

    /// <summary>
    /// help function for Check Dependency
    /// </summary>
    /// <param name="prevTask"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    private bool CheckDependency(BO.TaskInList prevTask, BO.Task task)
    {
        return InnerCheck(prevTask.Id, task.Id);

        bool InnerCheck(int currentTaskId, int prevTaskId)
        {
            List<DO.Dependency?>? dependencies = _dal.Dependency.ReadAll(d => d.DependentTask == currentTaskId)?.ToList();
            if (dependencies is null) { return true; }
            foreach (var dependency in dependencies)
            {
                if (dependency!.PreviousTask == prevTaskId)
                    return false;
                else
                    if (!InnerCheck(dependency!.PreviousTask, prevTaskId))
                    return false;
            }
            return true;
        }

    }

}

