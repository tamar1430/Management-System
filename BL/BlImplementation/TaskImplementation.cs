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

        foreach (var prevTask in boTask.Dependencies)
        {
            _dal.Dependency.Create(new DO.Dependency(0, boTask.Id, prevTask.Id));
        }

        DO.Task doTask = new(boTask.Id, boTask.Description,
            boTask.Alias, false, (DO.EngineerExperience)boTask!.CopmlexityLevel,
            boTask.CreatedAtDate,
            boTask.ScheduledStartDate, boTask.StartDate,
            boTask.ForecastDate - boTask.BaselineStartDate, boTask.DeadLine,
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
            throw new BlDeletionImpossible("can't delete this task");
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
                    CreatedAtDate = doTask.CreatedAtDate
Status = null,
                    Dependencies = doTask.IsMilestone ? new List<BO.Task>() :
    Milestone
     BaselineStartDate
   StartDate
    ScheduledStartDate
   ForecastDate
    DeadLine
     CompleteDate
    Deliverable
   Remarks
     Engineer
 CopmlexityLevel
                });
    }

    public BO.Task Read(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task engineer)
    {
        throw new NotImplementedException();
    }
}
