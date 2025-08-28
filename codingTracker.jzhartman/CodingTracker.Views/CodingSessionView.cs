using CodingTracker.Models.Entities;

namespace CodingTracker.Views
{
    public static class CodingSessionView
    {
        public static void RenderCodingSessions(List<CodingSession> sessions)
        {
            int count = 1;
            Console.WriteLine("A list of coding sessions: ");

            foreach (var session in sessions)
            {
                Console.WriteLine($"{count}:\t{session.StartTime} to {session.EndTime} for a duration of {session.Duration}");
                count++;
            }
        }
    }
}
