using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Core.Models
{
    public class CodingSession
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }

        public CodingSession(int id, string startTime, string endTime, string duration)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            GetDuration();
        }

        private void GetDuration()
        {
            var duration = DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime));
            Duration = duration.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
