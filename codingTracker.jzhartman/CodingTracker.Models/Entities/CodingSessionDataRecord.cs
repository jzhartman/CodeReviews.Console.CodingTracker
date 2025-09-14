namespace CodingTracker.Models.Entities;
public class CodingSessionDataRecord
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int Duration { get; set; }
}
