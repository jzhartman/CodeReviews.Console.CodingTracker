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
        public long Duration { get; private set; }
        public string DurationText { get; private set;}

        public CodingSession(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            CalculateDuration();
            GenerateDurationText();
        }

        private void CalculateDuration()
        {
          Duration = (long)EndTime.Subtract(StartTime).TotalSeconds;
        }

        private void GenerateDurationText()
        {
            DurationText = TimeSpan.FromSeconds(Duration).ToString();
        }
    }
}
