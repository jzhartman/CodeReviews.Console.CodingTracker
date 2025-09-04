using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Entities
{
    public class CodingSession
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; }

        public CodingSession(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            CalculateDuration();
        }

        private void CalculateDuration()
        {
          Duration = (long)EndTime.Subtract(StartTime).TotalSeconds;
        }
    }
}
