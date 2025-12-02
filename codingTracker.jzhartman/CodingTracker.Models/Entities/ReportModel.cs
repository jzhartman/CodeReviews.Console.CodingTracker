namespace CodingTracker.Models.Entities;
public class ReportModel
{
    public List<CodingSessionDataRecord> SessionList { get; set; }
    public double TotalTime { get; set; }
    public double AverageTime { get; set; }
    public CodingSessionDataRecord FirstEntry {  get; set; }
    public CodingSessionDataRecord LastEntry { get; set; }
    public int SessionCount { get; set; }
    
    public ReportModel(List<CodingSessionDataRecord> sessionList)
    {
        SessionList = sessionList;
        CalculateTotalTime();
        CalculateAverageTime();
        GetFirstAndLastEntry();
        GetSessionCount();
    }

    private void CalculateTotalTime()
    {
        foreach (var session in SessionList)
        {
            TotalTime += session.Duration;
        }
    }

    private void CalculateAverageTime()
    {
        AverageTime = TotalTime / SessionList.Count;
    }
    private void GetFirstAndLastEntry()
    {
        var orderedList = SessionList.OrderBy(s=>s.StartTime).ToList();

        FirstEntry = orderedList.First();
        LastEntry = orderedList.Last();
    }
    private void GetSessionCount()
    {
        SessionCount = SessionList.Count;
    }
}
