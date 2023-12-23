using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : BlApi.ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Create(BO.Task boTask)
    {
        if (boTask.Id <= 0) throw new BO.BlInorrectData("Task's id isn't correct");
        if (boTask.Alias.Length <= 0) throw new BO.BlInorrectData("Task's Alias isn't correct");

        //foreach (var prevTask in boTask.Dependencies)
        //{
        //    _dal.Dependency.Create(new DO.Dependency(0, boTask.Id, prevTask.Id));
        //}

        DO.Task doTask = new(boTask.Id, boTask.Description,
            boTask.Alias, false, (DO.EngineerExperience)boTask!.CopmlexityLevel,
            boTask.CreatedAtDate,
            boTask.ScheduledStartDate, boTask.StartDate,
            boTask.ForecastDate - boTask.ScheduledStartDate, boTask.DeadLine,
            boTask.CompleteDate, boTask.Deliverable,
            boTask.Remarks, boTask!.Engineer!.Id
            );

        try
        {
            int idTask = _dal.Task.Create(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={doTask.Id} already exists", ex);
        }
    }

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

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll(filter != null ? (Func<DO.Task, bool>)filter : null)
                select new BO.Task
                {
                    Id = doTask.Id,
                    Description = doTask.Description,
                    Alias = doTask.Alias,
                    CreatedAtDate = doTask.CreatedAtDate,
                    Status = doTask.ScheduledDate is null ? BO.Status.Unscheduled :
                    doTask.StartDate is null ? BO.Status.Scheduled :
                    doTask.CompleteDate is null ? BO.Status.OnTrack :
                    BO.Status.InJeopardy,
                    Milestone = null,//לעשות
                                     //BaselineStartDate =null ,//לעשות
                    StartDate = doTask.StartDate,
                    ScheduledStartDate = doTask.ScheduledDate,
                    ForecastDate = doTask.ScheduledDate + doTask.ForesastDate,
                    DeadLine = doTask.DeadLineDate,
                    CompleteDate = doTask.CompleteDate,
                    Deliverable = doTask.Deliverable,
                    Remarks = doTask.Remarks,
                    Engineer = doTask.EngineerId is not null ? new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal!.Engineer!.Read((int)doTask!.EngineerId)!.Name } : null,
                    CopmlexityLevel = (BO.EngineerExperience)doTask.CopmlexityLevel
                });
    }

    public BO.Task Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask is null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} doesn't exists");
        }

        BO.Task boTask = new()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            CreatedAtDate = doTask.CreatedAtDate,
            Status = doTask.ScheduledDate is null ? BO.Status.Unscheduled :
                doTask.StartDate is null ? BO.Status.Scheduled :
                doTask.CompleteDate is null ? BO.Status.OnTrack :
                BO.Status.InJeopardy,
            Milestone = null,//לעשות
                             //BaselineStartDate =null ,//לעשות
            StartDate = doTask.StartDate,
            ScheduledStartDate = doTask.ScheduledDate,
            ForecastDate = doTask.ScheduledDate + doTask.ForesastDate,
            DeadLine = doTask.DeadLineDate,
            CompleteDate = doTask.CompleteDate,
            Deliverable = doTask.Deliverable,
            Remarks = doTask.Remarks,
            Engineer = doTask.EngineerId is not null ? new BO.EngineerInTask() { Id = (int)doTask.EngineerId, Name = _dal!.Engineer!.Read((int)doTask!.EngineerId)!.Name } : null,
            CopmlexityLevel = (BO.EngineerExperience)doTask.CopmlexityLevel
        };
        return boTask;
    }

    public void Update(BO.Task boTask)
    {
        if (boTask.Id <= 0) throw new BO.BlInorrectData("Task's id isn't correct");
        if (boTask.Alias.Length <= 0) throw new BO.BlInorrectData("Task's Alias isn't correct");

        //foreach (var prevTask in boTask.Dependencies)
        //{
        //    _dal.Dependency.Create(new DO.Dependency(0, boTask.Id, prevTask.Id));
        //}

        DO.Task? prevDoTask = _dal!.Task.Read(boTask.Id);

        if (prevDoTask is null) throw new BO.BlDoesNotExistException($"task with id:{boTask.Id} isn't exists");

        _dal.Task.Delete(boTask.Id);

        DO.Task newDoTask = new(boTask.Id, boTask.Description,
            boTask.Alias, false, (DO.EngineerExperience)boTask!.CopmlexityLevel,
            boTask.CreatedAtDate,
            boTask.ScheduledStartDate, boTask.StartDate,
            boTask.ForecastDate - boTask.ScheduledStartDate, boTask.DeadLine,
            boTask.CompleteDate, boTask.Deliverable,
            boTask.Remarks, boTask!.Engineer!.Id
            );
        _dal.Task.Create(newDoTask);
    }
}

