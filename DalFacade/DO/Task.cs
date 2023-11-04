﻿

namespace DO;

public record Task
(
    ///static int I = 1;
    int Id,
    string Desceiption,
    string Alias,
    bool IsMilestone,
    EngineerExperience CopmlexityLevel,
    DateTime  CreatedAtDate,
    DateTime? StartDate = null,
    DateTime? ScheduledDate = null,
    DateTime? ForesastDate = null,
    DateTime? DeadLineDate = null,
    DateTime? CompleteDate = null,
    string? Deliverable = null,
    string? Remarks = null,
    int? Engineerld = null
)
{
    public Task():this(0,"","",false, EngineerExperience.Novice,DateTime.Now) { }
}
