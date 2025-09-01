namespace CodingTracker.Controller.Interfaces
{
    public interface ITrackSessionController
    {
        void GetStartAndEndTimes();
        void GetTimesWithStopwatch();
        void Run();
    }
}