
namespace DO;

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
    public Task():this(0,"","",false, EngineerExperience.Novice,DateTime.Now) { }
}
