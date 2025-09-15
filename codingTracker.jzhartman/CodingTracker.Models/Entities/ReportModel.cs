using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Models.Entities;
public class ReportModel
{
    public List<CodingSessionDataRecord> SessionList { get; set; }
    public double TotalTime { get; set; }
    public string TotalTimeText { get; set; }
    public double AverageTime { get; set; }
    public string AverageTimeText { get; set; }
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
        AverageTimeText = ConvertTimeFromSecondsToText(AverageTime);
        TotalTimeText = ConvertTimeFromSecondsToText(TotalTime);
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

    private string ConvertTimeFromSecondsToText(double input)
    {
        int miliseconds = TimeSpan.FromSeconds(input).Milliseconds;
        int seconds = TimeSpan.FromSeconds(input).Seconds;

        if ((double)miliseconds/1000 >= 0.5) seconds ++;

        int minutes = TimeSpan.FromSeconds(input).Minutes;
        int hours = TimeSpan.FromSeconds(input).Hours + TimeSpan.FromSeconds(input).Days * 24;

        return $"{hours} hours, {minutes} minutes, {seconds} seconds";
    }

}
