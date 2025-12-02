namespace CodingTracker.Models.Entities;
public enum GoalType
{
    TotalTime,
    AverageTime,
    DaysPerPeriod,
    Unknown
}

public enum GoalStatus
{
    InProgress,
    Complete,
    Failed,
    Abandoned
}