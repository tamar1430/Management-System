using BlApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{ 
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public BO.Milestone Read(int id)
    {
      DO.Task doMilestone = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"milestone with id:{id} isn't exists");

        var listIdTasks = _dal!.Dependency.ReadAll(d => d.DependentTask == id);

        List<BO.TaskInList> listTasks = new();

        foreach (var idTask in listIdTasks.ToList())
        {
            var task = _dal!.Task.Read(idTask!.Id);
            listTasks.Add(new BO.TaskInList() { Id = task!.Id, Description = task.Description, Alias = task.Alias,
                Status = task.ScheduledDate is null ? BO.Status.Unscheduled :
                task.StartDate is null ? BO.Status.Scheduled :
                task.CompleteDate is null ? BO.Status.OnTrack :
                BO.Status.InJeopardy,
            }); 
        }
        
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
            CompletionPercentage = 0,//לחשבן
            Remarks = doMilestone.Remarks,
            Dependencies = listTasks
        };
        

        return milestone;

    }

    public void Update(BO.Milestone milestone)
    {
        throw new NotImplementedException();
    }
}
