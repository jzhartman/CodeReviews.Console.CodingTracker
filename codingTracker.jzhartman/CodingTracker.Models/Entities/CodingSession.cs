namespace CodingTracker.Models.Entities
{
    public class CodingSession
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }

        private void CalculateDurationInSeconds()
        {
            Duration = (int)EndTime.Subtract(StartTime).TotalSeconds;
        }

    }
}
