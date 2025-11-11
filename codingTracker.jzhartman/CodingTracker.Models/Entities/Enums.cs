using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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