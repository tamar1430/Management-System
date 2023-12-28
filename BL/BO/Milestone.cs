namespace BO;

public class Milestone
{
    public int  Id { get; init; }
    public string Description { get; init; }
    public string Alias {  get; init; }
    public DateTime CreatedAtDate {  get; init; }   
    public Status Status { get; set; }
    public DateTime ? ForecastDate { get; init; }
    public DateTime? DeadlineDate { get; init; }
    public DateTime? CompleteDate { get; set;}
    public double ? CompletionPercentage { get; set; }
    public string ? Remarks {  get; set; }
    public List<TaskInList> Dependencies { get; set; }

    public override string ToString() => this.ToStringProperty();
}
