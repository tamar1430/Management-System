
namespace DO;

/// <summary>
/// record class for tasks
/// </summary>
/// <param name="Id"></param>
/// <param name="Description"></param>
/// <param name="Alias"></param>
/// <param name="IsMilestone"></param>
/// <param name="CopmlexityLevel"></param>
/// <param name="CreatedAtDate"></param>
/// <param name="ScheduledDate"></param>
/// <param name="StartDate"></param>
/// <param name="ForesastDate"></param>
/// <param name="DeadLineDate"></param>
/// <param name="CompleteDate"></param>
/// <param name="Deliverable"></param>
/// <param name="Remarks"></param>
/// <param name="Engineerld"></param>
public record Task
(
    int Id,
    string Description,
    string Alias,
    bool IsMilestone,
    EngineerExperience CopmlexityLevel,
    DateTime  CreatedAtDate,
    DateTime? ScheduledDate = null,
    DateTime? StartDate = null,
    TimeSpan? ForesastDate = null,
    DateTime? DeadLineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverable = null,
    string? Remarks = null,
    int? Engineerld = null
)
{
    public Task():this(0,"","",false, EngineerExperience.Novice,DateTime.Now) { }//defualt constractor
}
