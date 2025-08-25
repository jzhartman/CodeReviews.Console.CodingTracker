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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }

        //public CodingSession(long id, DateTime startTime, DateTime endTime, int duration)
        //{
        //    Id = id;
        //    StartTime = startTime;
        //    EndTime = endTime;
        //    Duration = duration;
        //    //GetDuration();
        //}

        private void GetDuration()
        {
            //var duration = DateTime.Parse(EndTime).Subtract(DateTime.Parse(StartTime));
            //Duration = duration.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //var durationText = TimeSpan.FromMilliseconds(ms).ToString("format");

    }
}
