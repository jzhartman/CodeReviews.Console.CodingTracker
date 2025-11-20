using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Entities;
public class GoalDTO
{
    public int Id { get; set; }
    public GoalType Type { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GoalStatus Status { get; set; }
    public long GoalValue { get; set; }
    public long CurrentValue { get; set; }
    public double Progress { get; set; }
}
