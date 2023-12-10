namespace BO;

public class Task
{
    public int Id { get; init; }
    public string Description { get; init; }
    public string Alias { get; init; }
    public DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; }
    //public MilestoneInTask? Milestone { get; set; }
    public List<TaskInList> Dependencies { get; set; }
    public DateTime? BaselineStartDate { get; init; }
    public DateTime? StartDate { get; set; }
    public DateTime? ScheduledStartDate { get; init; }
    public DateTime? ForecastDate { get; init; }
    public DateTime? DeadLine {  get; init; }
    public DateTime? CompleteDate { get; set; }
    public string? Deliverable {  get; set; }
    public string? Remarks { get; set; }
    public Engineer? Engineer { get; set; }
    public EngineerExperience? CopmlexityLevel {  get; set; }
}
