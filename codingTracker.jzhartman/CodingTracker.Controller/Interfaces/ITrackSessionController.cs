
namespace CodingTracker.Controller.Interfaces
{
    public interface ITrackSessionController
    {
        DateTime GetEndTime(DateTime startTime);
        DateTime GetStartTime();
        void GetTimesWithStopwatch();
        void Run();
    }
}