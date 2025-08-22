using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Entities
{
    public class CodingSession
    {
        public long Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }

        public CodingSession(long id, string startTime, string endTime, string duration)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            //GetDuration();
        }

        private void GetDuration()
        {
            var duration = DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime));
            Duration = duration.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
