namespace BO;

internal class Task
{
    public int Id { get; init; }
    public string Description { get; init; }
    public string Alias { get; init; }
    public DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    public bool IsMilestone { get; init; }
    EngineerExperience CopmlexityLevel,
    DateTime CreatedAtDate,
    DateTime? ScheduledDate = null,
    DateTime? StartDate = null,
    TimeSpan? ForesastDate = null,
    DateTime? DeadLineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverable = null,
    string? Remarks = null,
    int? Engineerld = null
}
