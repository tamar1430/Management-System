﻿namespace BO;

/// <summary>
/// MilestoneInList class
/// </summary>
public class MilestoneInList
{
    public int Id { get; init; }
    public string Description {  get; init; }
    public string Alias { get; init; }
    public DateTime StartDate { get; init; }
    public Status? Status { get; set; }
    public double? CompletionPercentage { get; set; }

    public override string ToString() => this.ToStringProperty();
}
