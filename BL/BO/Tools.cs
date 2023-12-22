
namespace BO;

public class Tools
{
    public static Status status (DO.Task doTask)
    {
        return doTask.ScheduledDate is null ? BO.Status.Unscheduled :
                   doTask.StartDate is null ? BO.Status.Scheduled :
                   doTask.CompleteDate is null ?
                   (doTask.DeadLineDate < DateTime.Now ? BO.Status.OnTrack : BO.Status.InJeopardy) :
                   BO.Status.Done;
    }

}
